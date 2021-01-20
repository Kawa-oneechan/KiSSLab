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

		public List<Object> Objects { get; set; }
		public List<Cell> Cells { get; set; }

		public int ScreenWidth { get; private set; }
		public int ScreenHeight { get; private set; }
		public Color BackgroundColor { get; set; }
		public Brush BackgroundBrush { get; set; }
		public int Palette { get; set; }
		public int Palettes { get; private set; }
		public int Sets { get; private set; }

		private Bitmap palette;
		private float[][] matrix;

		private ImageAttributes attrs;

		public Scene(Viewer viewer, string configFile)
		{
			Viewer = viewer;

			Objects = new List<Object>();
			Cells = new List<Cell>();

			Events = new Dictionary<string, List<object>>();
			Timers = new Dictionary<int, Timer>();

			attrs = new ImageAttributes();

			ScreenWidth = 480;
			ScreenHeight = 400;
			BackgroundColor = Color.CornflowerBlue;
			BackgroundBrush = null;

			matrix = new[] {
				new float[] {1, 0, 0, 0, 0},
				new float[] {0, 1, 0, 0, 0},
				new float[] {0, 0, 1, 0, 0},
				new float[] {0, 0, 0, 1, 0},
				new float[] {0, 0, 0, 0, 1}
			};

			try
			{
				palette = Mix.GetBitmap("palettes.png");
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
						if (rest.Count == 3 && rest[0] is int && rest[1] is int && rest[2] is int)
						{
							BackgroundColor = Color.FromArgb((int)rest[0], (int)rest[1], (int)rest[2]);
						}
						else if (rest.Count >= 7 && rest[0] is Symbol && rest[0].ToString() == "gradient")
						{
							var angle = 90f;
							if (rest.Count > 7 && rest[7] is int)
								angle = (float)(int)rest[7];
							BackgroundBrush = new LinearGradientBrush(new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.FromArgb((int)rest[1], (int)rest[2], (int)rest[3]), Color.FromArgb((int)rest[4], (int)rest[5], (int)rest[6]), angle);
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
							}

							//try to preload the image
							try
							{
								//copy the loaded image to a new object so the handle gets disposed.
								//this lets you edit cell images while the doll is still open!
								using (var imgFromFile = Mix.GetBitmap(file + ".png"))
								{
									image = new Bitmap(imgFromFile.Width, imgFromFile.Height, imgFromFile.PixelFormat);
									var r = new Rectangle(0, 0, imgFromFile.Width, imgFromFile.Height);
									var lockBits = imgFromFile.LockBits(r, ImageLockMode.ReadWrite, imgFromFile.PixelFormat);
									var pixels = new byte[lockBits.Stride * lockBits.Height];
									System.Runtime.InteropServices.Marshal.Copy(lockBits.Scan0, pixels, 0, pixels.Length);
									imgFromFile.UnlockBits(lockBits);
									lockBits = image.LockBits(r, ImageLockMode.ReadWrite, image.PixelFormat);
									System.Runtime.InteropServices.Marshal.Copy(pixels, 0, lockBits.Scan0, pixels.Length);
									image.UnlockBits(lockBits);
								}
							}
							catch (System.IO.FileNotFoundException ex)
							{
								DarkUI.Forms.DarkMessageBox.ShowWarning(ex.Message, Application.ProductName);
								continue;
							}

							//find the partof object
							var part = Objects.FirstOrDefault(o => o.ID == partof);
							if (part == null)
							{
								part = new Object()
								{
									ID = partof,
									Cells = new List<Cell>(),
									Visible = true,
								};
								Objects.Add(part);
							}

							if (newPos)
							{
								if (positions[0].ToString().EndsWith(":"))
								{
									var skipTo = int.Parse(positions[0].ToString().Remove(1));
									var newPositions = new List<object>();
									for (var i = 0; i < skipTo; i++)
									{
										newPositions.Add(new List<object>() { 0, 0 });
									}
									newPositions.AddRange(positions.Skip(1));
									positions = newPositions;
								}
								if (positions[0].ToString() == "*")
								{
									positions[0] = new List<object>() { 0, 0 };
								}

								if (positions[0] is List<object>)
								{
									for (var i = 0; i < 10; i++)
									{
										if (i >= positions.Count || positions[i].ToString() == "*" && i > 0)
										{
											part.Positions[i] = part.Positions[i - 1];
											continue;
										}
										var pos = positions[i] as List<object>;
										part.Positions[i] = new Point((int)pos[0], (int)pos[1]);
									}
								}
								else
								{
									var pos = positions as List<object>;
									if (pos[0].ToString() == "*")
										pos[0] = new List<object>() { 0, 0 };
									for (var i = 0; i < 10; i++)
									{
										part.Positions[i] = new Point((int)pos[0], (int)pos[1]);
									}
								}

								for (var i = 0; i < 10; i++)
								{
									part.InitialPositions[i] = part.Positions[i];
								}
							}
							if (newFix)
							{
								part.Fix = fix;
							}

							var c = new Cell()
							{
								Image = image,
								ImageFilename = file + ".png",
								Visible = mapped,
								ID = id,
								Opacity = alpha,
							};
							foreach (var s in on)
							{
								var i = (int)(s - '0');
								c.OnSets[i] = true;
							}
							c.Offset = new Point((int)offset[0], (int)offset[1]);
							c.Object = part;
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

		public Object GetObjectFromPoint(Point point, out Cell backCel)
		{
			Object ret = null;
			backCel = null;
			for (var i = Cells.Count; i > 0; i--)
			{
				var cell = Cells[i - 1];
				var obj = cell.Object;

				if (!cell.OnSet) continue;
				if (!cell.Visible) continue;
				if (!obj.Visible) continue;

				var x = point.X - obj.Position.X - cell.Offset.X;
				var y = point.Y - obj.Position.Y - cell.Offset.Y;
				if (x <= 0 || y <= 0 || x >= cell.Image.Width || y >= cell.Image.Height)
					continue;
				var color = cell.Image.GetPixel(x, y);
				if (color.A != 0)
				{
					ret = obj;
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

			using (var gfx = Graphics.FromImage(bitmap))
			{
				gfx.Clear(BackgroundColor);
				if (BackgroundBrush != null)
					gfx.FillRectangle(BackgroundBrush, 0, 0, ScreenWidth, ScreenHeight);
				foreach (var cell in Cells.Reverse<Cell>())
				{
					var obj = cell.Object;
					if (!cell.OnSet) continue;
					if (!cell.Visible) continue;
					if (!obj.Visible) continue;

					if (cell.Opacity == 0)
						continue;
					matrix[3][3] = cell.Opacity / 256.0f;
					attrs.SetColorMatrix(new ColorMatrix(matrix));

					gfx.DrawImage(cell.Image, new Rectangle(cell.Object.Position.X + cell.Offset.X, cell.Object.Position.Y + cell.Offset.Y, cell.Image.Width, cell.Image.Height), 0, 0, cell.Image.Width, cell.Image.Height, GraphicsUnit.Pixel, attrs);
				}
			}
		}
	}

	public class Cell
	{
		[Browsable(false)]
		public string ImageFilename { get; set; }
		[Description("The name of the file this cell's image was loaded from.")]
		public string Filename { get { return ImageFilename; } }
		[Description("Gets or sets the image making up this cell.")]
		public Bitmap Image { get; set; }
		[Description("The offset from the object's top-left corner to draw this cell.")]
		public Point Offset { get; set; }
		[Description("Determines whether this cell is visible or hidden.")]
		public bool Visible { get; set; }
		[Browsable(false)]
		public bool[] OnSets { get; set; }
		public bool OnSet { get { return OnSets[Viewer.Set]; } set { OnSets[Viewer.Set] = value; } }
		[Browsable(false)]
		public Object Object { get; set; }
		public string ID { get; set; }
		[System.ComponentModel.Editor(typeof(AlphaEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public int Opacity { get; set; }

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

	public class Object
	{
		[Browsable(false)]
		public List<Cell> Cells { get; set; }
		[Browsable(false)]
		public Point[] Positions { get; set; }
		public Point Position { get { return Positions[Viewer.Set]; } set { Positions[Viewer.Set] = value; } }
		[Browsable(false)]
		public Point[] InitialPositions { get; set; }
		public Point InitialPosition { get { return InitialPositions[Viewer.Set]; } set { InitialPositions[Viewer.Set] = value; } }
		public bool Visible { get; set; }
		public int Fix { get; set; }
		public int InitialFix { get; set; }
		public bool Locked { get { return Fix >= 999; } set { Fix = value ? 999 : 0; } }
		public string ID { get; set; }
		[Browsable(false)]
		public Object LastCollidedWith { get; set; }

		public Object()
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
