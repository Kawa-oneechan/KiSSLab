using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Kawa.Mix;
using Kawa.SExpressions;
using Timer = System.Windows.Forms.Timer;

namespace KiSSLab
{
	public partial class Scene
	{
		public Mix Mix { get; private set; }
		public Bitmap Bitmap { get; private set; }
		public Viewer Viewer { get; private set; }

		public int Set
		{
			get { return set; }
			set
			{
				if (value < 0)
					set = 0;
				else if (value >= Sets)
					set = Sets - 1;
				else
					set = value;
			}
		}

		public List<Part> Parts { get; set; }
		public List<Cel> Cels { get; set; }

		public int ScreenWidth { get; private set; }
		public int ScreenHeight { get; private set; }

		public object Background
		{
			get { return backgrounds[set]; }
			set { backgrounds[set] = value; }
		}
		public int MaxFix { get; private set; }

		public int Palette { get; set; }
		public int Palettes { get; private set; }
		public int Sets { get; private set; }

		public int Zoom { get; set; }

		private int set;
		private Bitmap palette;
		private float[][] matrix;
		private object[] backgrounds;
		private string defaultOn = "0123456789";

		private ImageAttributes attrs;
		private Graphics gfx;

		public Scene(Viewer viewer, Mix mix, string configFile)
		{
			Mix = mix;
			Viewer = viewer;

			Parts = new List<Part>();
			Cels = new List<Cel>();

			Events = new Dictionary<string, List<object>>();
			Timers = new Dictionary<int, Timer>();
			scriptVariables = new Dictionary<Symbol, object>();
			scriptVariables.Add("true", 1);
			scriptVariables.Add("false", 0);

			attrs = new ImageAttributes();

			ScreenWidth = 480;
			ScreenHeight = 400;
			MaxFix = 100;
			backgrounds = new object[10];

			matrix = new[] {
				new float[] {1, 0, 0, 0, 0},
				new float[] {0, 1, 0, 0, 0},
				new float[] {0, 0, 1, 0, 0},
				new float[] {0, 0, 0, 1, 0},
				new float[] {0, 0, 0, 0, 1}
			};

			try
			{
				palette = Tools.GrabClonedBitmap("palettes.png", Mix);
				Palettes = palette.Height;
			}
			catch (Exception)
			{
				palette = null;
				Palettes = 1;
			}

			HashCodes = new Dictionary<int, string>();
			var sex = new SExpression(Mix.GetString(configFile));
			var data = ((List<object>)sex.Data)[0] as List<object>;

			foreach (var item in data)
			{
				if (item is List<object> && ((List<object>)item)[0] is Symbol)
				{
					var form = ((List<object>)item)[0] as Symbol;
					var rest =  ((List<object>)item).Skip(1).ToList();
					if (form == "screen" && rest.Count == 2)
					{
						ScreenWidth = (int)rest[0];
						ScreenHeight = (int)rest[1];
					}
					else if (form == "default-on" && rest.Count == 1 && rest[0] is string)
					{
						defaultOn = rest[0].ToString();
					}
					else if (form == "max-fix" && rest.Count == 1 && rest[0] is int)
					{
						MaxFix = (int)rest[0];
					}
					else if (form == "background")
					{
						if (!(rest[0] is List<object>))
						{
							rest = new List<object>() { rest };
						}
						var bgs = 0;
						foreach (var bgi in rest.OfType<List<object>>())
						{
							object bg = null;
							if (bgi.Count == 3 && bgi[0] is int && bgi[1] is int && bgi[2] is int)
							{
								bg = Color.FromArgb((int)bgi[0], (int)bgi[1], (int)bgi[2]);
							}
							else if (bgi.Count >= 7 && bgi[0] is Symbol && bgi[0].ToString() == "gradient")
							{
								var angle = 90f;
								if (bgi.Count > 7 && bgi[7] is int)
									angle = (float)(int)bgi[7];
								bg = new LinearGradientBrush(new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.FromArgb((int)bgi[1], (int)bgi[2], (int)bgi[3]), Color.FromArgb((int)bgi[4], (int)bgi[5], (int)bgi[6]), angle);
							}
							else if (bgi.Count == 1 && bgi[0] is string)
							{
								bg = Tools.GrabClonedBitmap(bgi[0].ToString() + ".png", Mix);
							}
							else if (bgi.Count == 2 && bgi[0] is string && bgi[1] is Symbol && bgi[1].ToString() == "tiled")
							{
								bg = new TextureBrush(Tools.GrabClonedBitmap(bgi[0].ToString() + ".png", Mix));
							}
							backgrounds[bgs] = bg;
							if (bgs == 0)
							{
								for (var i = 1; i < 10; i++)
									backgrounds[i] = backgrounds[0];
							}
							bgs++;
						}
						
					}
					#region Cel parser
					else if (form == "cels")
					{
						foreach (var celItem in rest.Cast<List<object>>())
						{
							form = celItem[0] as Symbol;
							if (form == "group")
							{
								//handle groups
							}
							else
							{
								ParseCelForm(celItem);
							}
						}
					}
					#endregion
					else if (form == "events")
						LoadEvents((List<object>)item);
				}
			}

			//Determine max set count
			foreach (var cel in Cels)
			{
				for (var i = 0; i < 10; i++)
				{
					if (cel.OnSets[i] && i > Sets)
						Sets = i;
				}
			}
			Sets++;

			Bitmap = new Bitmap(ScreenWidth, ScreenHeight);
		}

		public Cel ParseCelForm(List<object> celItem)
		{
			Bitmap image = null;
			var file = "";
			var id = "";
			var partof = "";
			var alpha = 255;
			var positions = new List<object>() { 0, 0 };
			var offset = new List<object>() { 0, 0 };
			var on = defaultOn; //"0123456789";
			var fix = 0;
			var newPos = false;
			var newFix = false;
			var mapped = true;
			var ghosted = false;

			for (var i = 0; i < celItem.Count; i++)
			{
				var form = celItem[i] as Symbol;
				switch (form)
				{
					case null:
						break;
					case "file":
						{
							i++;
							file = celItem[i] as string;
							if (id == "") id = file;
							if (partof == "") partof = id;
						}
						break;
					case "id":
						{
							i++;
							if (partof == id) partof = "";
							id = celItem[i] as string;
							if (partof == "") partof = id;
						}
						break;
					case "partof":
						{
							i++;
							partof = celItem[i] as string;
						}
						break;
					case "alpha":
						{
							i++;
							alpha = (int)celItem[i];
						}
						break;
					case "pos":
						{
							i++;
							positions = celItem[i] as List<object>;
							newPos = true;
						}
						break;
					case "offset":
						{
							i++;
							offset = celItem[i] as List<object>;
						}
						break;
					case "fix":
						{
							i++;
							fix = (int)celItem[i];
							newFix = true;
						}
						break;
					case "locked":
						{
							fix = MaxFix;
							newFix = true;
						}
						break;
					case "on":
						{
							i++;
							on = celItem[i] as string;
						}
						break;
					case "unmapped":
						{
							mapped = false;
						}
						break;
					case "ghost":
						{
							ghosted = true;
						}
						break;
					case "group":
						break;
				}
			}

			//try to preload the image
			try
			{
				image = Tools.GrabClonedBitmap(file + ".png", Mix);
			}
			catch (System.IO.FileNotFoundException ex)
			{
				DarkUI.Forms.DarkMessageBox.ShowWarning(ex.Message, Application.ProductName);
				return null;
			}

			//find the partof object
			var part = Parts.FirstOrDefault(o => o.ID == partof);
			if (part == null)
			{
				part = new Part(this)
				{
					ID = partof,
					Cels = new List<Cel>(),
				};
				Parts.Add(part);
			}

			if (newPos)
			{
				if (positions[0] is int)
				{
					//pos (12 34) --> pos ((12 34))
					positions = new List<object>() { positions };
				}

				while (positions.Count < 10)
					positions.Add(positions[0]);

				for (var i = 0; i < 10; i++)
				{
					if (positions[i] is Symbol && positions[i].ToString() == "*")
					{
						if (i == 0)
							positions[i] = new List<object>() { 0, 0 };
						else
							positions[i] = positions[0];
					}
					var pos = positions[i] as List<object>;
					part.Positions[i] = new Point((int)pos[0], (int)pos[1]);
					if (pos.Count > 2 && pos[2].ToString() == ">")
					{
						pos.RemoveAt(2);
						for (var j = i + 1; j < 10; j++)
							positions[j] = positions[i];
					}
				}

				for (var i = 0; i < 10; i++)
				{
					part.InitialPositions[i] = part.Positions[i];
				}
			}
			if (newFix)
			{
				part.Fix = part.InitialFix = fix;
			}

			var c = new Cel(this)
			{
				Image = image,
				ImageFilename = file + ".png",
				Visible = mapped,
				ID = id,
				Opacity = alpha,
				Ghost = ghosted,
			};
			foreach (var s in on)
			{
				if (!char.IsDigit(s))
					continue;
				var i = (int)(s - '0');
				c.OnSets[i] = true;
			}
			c.Offset = new Point((int)offset[0], (int)offset[1]);
			c.Part = part;
			part.Cels.Add(c);
			part.UpdateBounds();

			Cels.Add(c);
			return c;
		}

		public Part GetPartFromPoint(Point point, out Cel backCel)
		{
			Part ret = null;
			backCel = null;
			for (var i = Cels.Count; i > 0; i--)
			{
				var cel = Cels[i - 1];
				var part = cel.Part;

				if (cel.Ghost) continue;
				if (!cel.OnSet) continue;
				if (!cel.Visible) continue;

				var x = point.X - part.Position.X - cel.Offset.X;
				var y = point.Y - part.Position.Y - cel.Offset.Y;
				if (x <= 0 || y <= 0 || x >= cel.Image.Width || y >= cel.Image.Height)
					continue;
				var color = cel.Image.GetPixel(x, y);
				if (color.A != 0)
				{
					ret = part;
					backCel = cel;
				}
			}
			return ret;
		}

		public void DrawToBitmap()
		{
			if (palette != null)
			{
				var colorMap = new ColorMap[palette.Width];
				for (var i = 0; i < palette.Width; i++)
				{
					var sourceColor = palette.GetPixel(i, 0);
					var targetColor = palette.GetPixel(i, Palette);
					colorMap[i] = new ColorMap() { OldColor = sourceColor, NewColor = targetColor };
				}
				attrs.SetRemapTable(colorMap);
			}

			if (gfx == null)
			{
				gfx = Graphics.FromImage(Bitmap);
				gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
			}

			if (Background is Color)
				gfx.Clear((Color)Background);
			else if (Background is Brush)
				gfx.FillRectangle((Brush)Background, 0, 0, ScreenWidth, ScreenHeight);
			else if (Background is Bitmap)
				gfx.DrawImage((Bitmap)Background, 0, 0, ScreenWidth, ScreenHeight);
			else
				gfx.Clear(Color.CornflowerBlue);

			foreach (var cel in Cels.Reverse<Cel>())
			{
				var part = cel.Part;
				if (!cel.OnSet) continue;
				if (!cel.Visible) continue;
				
				if (cel.Opacity == 0)
					continue;
				matrix[3][3] = cel.Opacity / 256.0f;
				attrs.SetColorMatrix(new ColorMatrix(matrix));

				try
				{
					gfx.DrawImage(cel.Image, new Rectangle(cel.Part.Position.X + cel.Offset.X, cel.Part.Position.Y + cel.Offset.Y, cel.Image.Width, cel.Image.Height), 0, 0, cel.Image.Width, cel.Image.Height, GraphicsUnit.Pixel, attrs);
				}
				catch (Exception)
				{ }
			}
		}
	}

	public class Cel
	{
		private Scene scene;
		public string ImageFilename { get; set; }
		public Bitmap Image { get; set; }
		public Point Offset { get; set; }
		[ScriptProperty]
		public bool Visible { get; set; }
		public bool[] OnSets { get; set; }
		public bool OnSet { get { return OnSets[scene.Set]; } set { OnSets[scene.Set] = value; } }
		[ScriptProperty]
		public Part Part { get; set; }
		[ScriptProperty]
		public string ID { get; set; }
		[ScriptProperty]
		public int Opacity { get; set; }
		[ScriptProperty]
		public bool Ghost { get; set; }

		public Cel(Scene scene)
		{
			this.scene = scene;
			OnSets = new bool[10];
			for (var i = 0; i < 10; i++)
				OnSets[i] = false;
		}

		public override string ToString()
		{
			return string.Format("{0}", ID);
		}
	}

	public class Part
	{
		private Scene scene;
		[ScriptProperty]
		public List<Cel> Cels { get; set; }
		public Point[] Positions { get; set; }
		public Point Position { get { return Positions[scene.Set]; } set { Positions[scene.Set] = value; } }
		public Point[] InitialPositions { get; set; }
		public Point InitialPosition { get { return InitialPositions[scene.Set]; } set { InitialPositions[scene.Set] = value; } }
		[ScriptProperty]
		public int Fix { get; set; }
		[ScriptProperty]
		public int InitialFix { get; set; }
		[ScriptProperty]
		public bool Locked { get { return Fix >= scene.MaxFix; } set { Fix = value ? scene.MaxFix : 0; } }
		[ScriptProperty]
		public string ID { get; set; }
		public Part LastCollidedWith { get; set; }
		[ScriptProperty]
		public bool Visible
		{
			get
			{
				return Cels.Any(c => c.Visible);
			}
			set
			{
				Cels.ForEach(c => c.Visible = value);
			}
		}

		public Part(Scene scene)
		{
			this.scene = scene;
			InitialPositions = new Point[10];
			Positions = new Point[10];
		}

		public override string ToString()
		{
			return string.Format("{0}", ID);
		}

		public Size Bounds { get; private set; }
		public void UpdateBounds()
		{
			var w = 0;
			var h = 0;
			foreach (var cel in Cels)
			{
				if (cel.Image.Width + cel.Offset.X > w)
					w = cel.Image.Width + cel.Offset.X;
				if (cel.Image.Height + cel.Offset.Y > h)
					h = cel.Image.Height + cel.Offset.Y;
			}
			Bounds = new Size(w, h);
		}
	}
}
