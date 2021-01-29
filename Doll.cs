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
		public List<Cell> Cells { get; set; }

		public int ScreenWidth { get; private set; }
		public int ScreenHeight { get; private set; }

		public object Background
		{
			get { return backgrounds[set]; }
			set { backgrounds[set] = value; }
		}

		public int Palette { get; set; }
		public int Palettes { get; private set; }
		public int Sets { get; private set; }

		private int set;
		private Bitmap palette;
		private float[][] matrix;
		private object[] backgrounds;

		private ImageAttributes attrs;
		private Graphics gfx;

		public Scene(Viewer viewer, string configFile)
		{
			Viewer = viewer;

			Parts = new List<Part>();
			Cells = new List<Cell>();

			Events = new Dictionary<string, List<object>>();
			Timers = new Dictionary<int, Timer>();
			scriptVariables = new Dictionary<Symbol, object>();
			scriptVariables.Add("true", 1);
			scriptVariables.Add("false", 0);

			attrs = new ImageAttributes();

			ScreenWidth = 480;
			ScreenHeight = 400;
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
				palette = Tools.GrabClonedBitmap("palettes.png");
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

			var defaultOn = "0123456789";

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
								bg = Tools.GrabClonedBitmap(bgi[0].ToString() + ".png");
							}
							else if (bgi.Count == 2 && bgi[0] is string && bgi[1] is Symbol && bgi[1].ToString() == "tiled")
							{
								bg = new TextureBrush(Tools.GrabClonedBitmap(bgi[0].ToString() + ".png"));
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
					#region Cell parser
					else if (form == "cells")
					{
						foreach (var celItem in rest.Cast<List<object>>())
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
								form = celItem[i] as Symbol;
								if (form == null)
									break;
								if (form == "file")
								{
									i++;
									file = celItem[i] as string;
									if (id == "") id = file;
									if (partof == "") partof = id;
								}
								else if (form == "id")
								{
									i++;
									if (partof == id) partof = "";
									id = celItem[i] as string;
									if (partof == "") partof = id;
								}
								else if (form == "partof")
								{
									i++;
									partof = celItem[i] as string;
								}
								else if (form == "alpha")
								{
									i++;
									alpha = (int)celItem[i];
								}
								else if (form == "pos")
								{
									i++;
									positions = celItem[i] as List<object>;
									newPos = true;
								}
								else if (form == "offset")
								{
									i++;
									offset = celItem[i] as List<object>;
								}
								else if (form == "fix")
								{
									i++;
									fix = (int)celItem[i];
									newFix = true;
								}
								else if (form == "locked")
								{
									fix = 9999;
									newFix = true;
								}
								else if (form == "on")
								{
									i++;
									on = celItem[i] as string;
								}
								else if (form == "unmapped")
								{
									mapped = false;
								}
								else if (form == "ghost")
								{
									ghosted = true;
								}
							}

							//try to preload the image
							try
							{
								image = Tools.GrabClonedBitmap(file + ".png"); //Mix.GetBitmap(file + ".png").ReleaseClone();
							}
							catch (System.IO.FileNotFoundException ex)
							{
								DarkUI.Forms.DarkMessageBox.ShowWarning(ex.Message, Application.ProductName);
								continue;
							}

							//find the partof object
							var part = Parts.FirstOrDefault(o => o.ID == partof);
							if (part == null)
							{
								part = new Part()
								{
									ID = partof,
									Cells = new List<Cell>(),
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

							var c = new Cell()
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
								var i = (int)(s - '0');
								c.OnSets[i] = true;
							}
							c.Offset = new Point((int)offset[0], (int)offset[1]);
							c.Part = part;
							part.Cells.Add(c);
							part.UpdateBounds();

							Cells.Add(c);
						}
					}
					#endregion
					else if (form == "events")
						LoadEvents((List<object>)item);
				}
			}

			//Determine max set count
			foreach (var cell in Cells)
			{
				for (var i = 0; i < 10; i++)
				{
					if (cell.OnSets[i] && i > Sets)
						Sets = i;
				}
			}
			Sets++;
		}

		public Part GetPartFromPoint(Point point, out Cell backCel)
		{
			Part ret = null;
			backCel = null;
			for (var i = Cells.Count; i > 0; i--)
			{
				var cell = Cells[i - 1];
				var part = cell.Part;

				if (cell.Ghost) continue;
				if (!cell.OnSet) continue;
				if (!cell.Visible) continue;

				var x = point.X - part.Position.X - cell.Offset.X;
				var y = point.Y - part.Position.Y - cell.Offset.Y;
				if (x <= 0 || y <= 0 || x >= cell.Image.Width || y >= cell.Image.Height)
					continue;
				var color = cell.Image.GetPixel(x, y);
				if (color.A != 0)
				{
					ret = part;
					backCel = cell;
				}
			}
			return ret;
		}

		public void DrawToBitmap(Bitmap bitmap)
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
				gfx = Graphics.FromImage(bitmap);
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

			foreach (var cell in Cells.Reverse<Cell>())
			{
				var part = cell.Part;
				if (!cell.OnSet) continue;
				if (!cell.Visible) continue;
				
				if (cell.Opacity == 0)
					continue;
				matrix[3][3] = cell.Opacity / 256.0f;
				attrs.SetColorMatrix(new ColorMatrix(matrix));

				try
				{
					gfx.DrawImage(cell.Image, new Rectangle(cell.Part.Position.X + cell.Offset.X, cell.Part.Position.Y + cell.Offset.Y, cell.Image.Width, cell.Image.Height), 0, 0, cell.Image.Width, cell.Image.Height, GraphicsUnit.Pixel, attrs);
				}
				catch (Exception)
				{ }
			}
		}
	}

	public class Cell
	{
		public string ImageFilename { get; set; }
		public string Filename { get { return ImageFilename; } }
		public Bitmap Image { get; set; }
		public Point Offset { get; set; }
		public bool Visible { get; set; }
		public bool[] OnSets { get; set; }
		public bool OnSet { get { return OnSets[Viewer.Scene.Set]; } set { OnSets[Viewer.Scene.Set] = value; } }
		public Part Part { get; set; }
		public string ID { get; set; }
		public int Opacity { get; set; }
		public bool Ghost { get; set; }

		public Cell()
		{
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
		public List<Cell> Cells { get; set; }
		public Point[] Positions { get; set; }
		public Point Position { get { return Positions[Viewer.Scene.Set]; } set { Positions[Viewer.Scene.Set] = value; } }
		public Point[] InitialPositions { get; set; }
		public Point InitialPosition { get { return InitialPositions[Viewer.Scene.Set]; } set { InitialPositions[Viewer.Scene.Set] = value; } }
		public int Fix { get; set; }
		public int InitialFix { get; set; }
		public bool Locked { get { return Fix >= 999; } set { Fix = value ? 999 : 0; } }
		public string ID { get; set; }
		public Part LastCollidedWith { get; set; }
		public bool Visible
		{
			get
			{
				return Cells.Any(c => c.Visible);
			}
			set
			{
				Cells.ForEach(c => c.Visible = value);
			}
		}

		public Part()
		{
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
			foreach (var cell in Cells)
			{
				if (cell.Image.Width + cell.Offset.X > w)
					w = cell.Image.Width + cell.Offset.X;
				if (cell.Image.Height + cell.Offset.Y > h)
					h = cell.Image.Height + cell.Offset.Y;
			}
			Bounds = new Size(w, h);
		}
	}
}
