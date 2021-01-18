using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Config;
using DarkUI.Controls;
using DarkUI.Forms;
using Kawa.Configuration;
using Kawa.Mix;
using Microsoft.Win32;

namespace KiSSLab
{
	static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Viewer(args));
		}
	}

	public class Viewer : DarkForm
	{
		private Editor editor;
		private DarkMenuStrip menu;
		private DarkToolStrip tools;
		private DarkStatusStrip status;
		private Panel screenContainer;
		private PictureBox screen;
		private Bitmap bitmap;
		private Object held, dropped;
		private Point heldOffset, fix;
		private Panel debug;

		public static Scene Scene;
		public static int Zoom = 1;

		public static int Set;
		public static bool Hilight;
		public Cell HilightedCell;

		private string[] lastOpened;

		//[System.Runtime.InteropServices.DllImport("Shell32.dll")]
		//private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

		public Viewer(string[] args)
		{
			/*
			var reg = (string)Registry.GetValue(@"HKEY_CLASSES_ROOT\.lcnf", "", "");
			if (reg == null)
			{
				Registry.SetValue(@"HKEY_CLASSES_ROOT\.lcnf", "", "KiSSThingLCNF");
				var myThing = Registry.ClassesRoot.CreateSubKey("KiSSThingLCNF");
				myThing.SetValue("", "Kawa's KiSS Thing");
				var openCommand = myThing.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command");
				openCommand.SetValue("", string.Format("\"{0}\" \"%1\"", Application.ExecutablePath));
				myThing.CreateSubKey("DefaultIcon").SetValue("", string.Format("\"{0},1\"", Application.ExecutablePath));
				SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
			}
			*/

			Config.Path = @"Kawa\KiSS Thing";
			Config.Load();

			var windowState = Config.WindowState; //cache the real value because we be sizin' yo
			this.ClientSize = new Size(Config.WindowWidth, Config.WindowHeight);
			this.StartPosition = FormStartPosition.Manual;
			if (Config.WindowLeft == -1 && Config.WindowTop == -1)
				this.Location = new Point(32, 32);
			else
				this.Location = new Point(Config.WindowLeft, Config.WindowTop);
			this.WindowState = (FormWindowState)windowState;
			this.Move += (s, e) => { if (this.WindowState != FormWindowState.Normal) return; Config.WindowLeft = this.Location.X; Config.WindowTop = this.Location.Y; };
			this.ResizeEnd += (s, e) => { if (this.WindowState != FormWindowState.Normal) return; Config.WindowWidth = this.ClientSize.Width; Config.WindowHeight = this.ClientSize.Height; };
			this.FormClosing += (s, e) => { Config.Save(); };
			if (Config.ZoomLevel < 1 || Config.ZoomLevel > 3)
				Config.ZoomLevel = 1;
			Zoom = Config.ZoomLevel;
			if (!string.IsNullOrWhiteSpace(Config.AutoLoad))
				args = new string[] { Config.AutoLoad };


			this.Text = Application.ProductName;
			this.Font = new Font("Segoe UI", 9);
			this.DoubleBuffered = true;
			this.Icon = global::KiSSLab.Properties.Resources.app;

			menu = new DarkMenuStrip();
			var file = new ToolStripMenuItem("&File");
			file.DropDownItems.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem("&Open", global::KiSSLab.Properties.Resources.Open, Open_Click, Keys.Control | Keys.O),
				new ToolStripMenuItem("&Reopen", global::KiSSLab.Properties.Resources.Reset, (s, e) => { OpenDoll(lastOpened[0], lastOpened[1]); }, Keys.Control | Keys.R),
				new ToolStripSeparator(),
				new ToolStripMenuItem("E&xit", global::KiSSLab.Properties.Resources.Exit, (s, e) => { this.Close(); }, Keys.Alt | Keys.F4),

			});
			menu.Items.Add(file);
			var edit = new ToolStripMenuItem("&Edit");
			edit.DropDownItems.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem("&Copy cell", global::KiSSLab.Properties.Resources.Copy, (s, e) => { Clipboard.SetImage(HilightedCell.Image); }, Keys.Control | Keys.C),
				new ToolStripMenuItem("&Reset position", global::KiSSLab.Properties.Resources.Reset, (s, e) => { Reset_Click(null, null); }, Keys.Control | Keys.Shift | Keys.R),
			});
			menu.Items.Add(edit);
			var help = new ToolStripMenuItem("&Help");
			help.DropDownItems.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem("&About", null, (s, e) => { (new About()).ShowDialog(this); }),
			});
			menu.Items.Add(help);
			tools = new DarkToolStrip();
			tools.Items.AddRange(new ToolStripItem[]
			{
					new ToolStripButton("Open", global::KiSSLab.Properties.Resources.Open, Open_Click) { DisplayStyle = ToolStripItemDisplayStyle.Image },
					new ToolStripSeparator(),
					new ToolStripButton("Highlight current cell", global::KiSSLab.Properties.Resources.Highlight, (s, e) =>
					{
						Hilight = ((ToolStripButton)s).Checked;
						DrawScene();
					})
					{
						CheckOnClick = true,
						Checked = Hilight,
						DisplayStyle = ToolStripItemDisplayStyle.Image
					},
					new ToolStripSeparator(),
					new ToolStripButton("Next set", global::KiSSLab.Properties.Resources.CycleSets, (s, e) =>
					{
						((ToolStripButton)tools.Items.Find("s" + Set.ToString(), false)[0]).Checked = false;
						Set++;
						if (Set == Scene.Sets) Set = 0;
						if (dropped != null) dropped.LastCollidedWith = null;
						DrawScene();
						((ToolStripButton)tools.Items.Find("s" + Set.ToString(), false)[0]).Checked = true;
					})
					{
						DisplayStyle = ToolStripItemDisplayStyle.Image,
					},
			});
			for (var i = 0; i < 10; i++)
			{
				tools.Items.Add(new ToolStripButton(
					i.ToString(), null, (s, e) =>
					{
						((ToolStripButton)tools.Items.Find("s" + Set.ToString(), false)[0]).Checked = false;
						Set = int.Parse(((ToolStripButton)s).Text);
						if (dropped != null) dropped.LastCollidedWith = null;
						DrawScene();
						((ToolStripButton)s).Checked = true;
					}, "s" + i.ToString()
				)
				{
					ToolTipText = string.Format("Switch to set {0}", i),
					DisplayStyle = ToolStripItemDisplayStyle.Text,
					Checked = (i == 0),
				});
			}
			tools.Items.AddRange(new ToolStripItem[] {
				new ToolStripSeparator(),
				new ToolStripButton("Next palette", global::KiSSLab.Properties.Resources.Colors, (s, e) =>
				{
					((ToolStripButton)tools.Items.Find("p" + Scene.Palette.ToString(), false)[0]).Checked = false;
					Scene.Palette++;
					if (Scene.Palette == Scene.Palettes) Scene.Palette = 0;
					DrawScene();
					((ToolStripButton)tools.Items.Find("p" + Scene.Palette.ToString(), false)[0]).Checked = true;
				})
				{
					DisplayStyle = ToolStripItemDisplayStyle.Image,
				},
			});
			for (var i = 0; i < 10; i++)
			{
				tools.Items.Add(new ToolStripButton(
					i.ToString(), null, (s, e) =>
					{
						((ToolStripButton)tools.Items.Find("p" + Scene.Palette.ToString(), false)[0]).Checked = false;
						Scene.Palette = int.Parse(((ToolStripButton)s).Text);
						DrawScene();
						((ToolStripButton)s).Checked = true;
					}, "p" + i.ToString()
				)
				{
					ToolTipText = string.Format("Switch to palette {0}", i),
					DisplayStyle = ToolStripItemDisplayStyle.Text,
					Checked = (i == 0),
				});
			}
			tools.Items.Add(new ToolStripSeparator());
			for (var i = 1; i <= 3; i++)
			{
				tools.Items.Add(new ToolStripButton(
					string.Format("×{0}", i), null, (s, e) =>
					{
						((ToolStripButton)tools.Items.Find("z" + Zoom.ToString(), false)[0]).Checked = false;
						Zoom = int.Parse(((ToolStripButton)s).Text.Substring(1));
						screen.ClientSize = new Size(bitmap.Width * Zoom, bitmap.Height * Zoom);
						((ToolStripButton)s).Checked = true;
						Config.ZoomLevel = Zoom;
						Viewer_Resize(null, null);
						DrawScene();
					}, "z" + i.ToString()
				)
				{
					ToolTipText = string.Format("Set zoom level to {0}", i),
					DisplayStyle = ToolStripItemDisplayStyle.Text,
					Checked = (i == Zoom),
				});
			}
			tools.Items.AddRange(new ToolStripItem[] {
				new ToolStripSeparator(),
				new ToolStripButton("Show editor", global::KiSSLab.Properties.Resources.Properties, (s, e) =>
				{
					var me = (ToolStripButton)s;
					me.Checked = !me.Checked;
					editor.Visible = me.Checked;
					Viewer_Resize(null, null);
					Config.Editor = me.Checked ? 1 : 0;
				})
				{
					DisplayStyle = ToolStripItemDisplayStyle.Image,
					Checked = Config.Editor == 1,
				},
			});
			tools.GripStyle = ToolStripGripStyle.Hidden;
			tools.Padding = new Padding(8, 0, 8, 0);
			//((ToolStripButton)tools.Items.Find("×" + Zoom.ToString(), false)[0]).Checked = true;

			status = new DarkStatusStrip()
			{
				Padding = new Padding(0),
			};

			status.Items.AddRange(new[] {
				new ToolStripStatusLabel() { AutoSize = false, TextAlign = ContentAlignment.MiddleLeft,  Width = 160, BorderStyle = Border3DStyle.Sunken },
				new ToolStripStatusLabel() { AutoSize = false, TextAlign = ContentAlignment.MiddleLeft, Width = 80 },
				new ToolStripStatusLabel() { AutoSize = false, Width = 20 },
				new ToolStripStatusLabel() { AutoSize = false, TextAlign = ContentAlignment.MiddleLeft, Width = 40 },
			});
			screen = new PictureBox()
			{
				ClientSize = new Size(480, 400),
				BorderStyle = BorderStyle.FixedSingle,
			};
			screen.MouseDown += new MouseEventHandler(Screen_MouseDown);
			screen.MouseMove += new MouseEventHandler(Screen_MouseMove);
			screen.MouseUp += new MouseEventHandler(Screen_MouseUp);
			screen.Paint += new PaintEventHandler(Screen_Paint);
			editor = new Editor(this)
			{
				Dock = DockStyle.Right,
				Width = 320,
				Visible = Config.Editor == 1,
			};
			screenContainer = new Panel()
			{
				AutoScroll = true,
				Dock = DockStyle.Fill,
			};
			screenContainer.Controls.Add(screen);
			this.Controls.Add(screenContainer);
			this.Controls.Add(tools);
			this.Controls.Add(editor);
			this.Controls.Add(menu);
			this.Controls.Add(status);
			this.Resize += new EventHandler(Viewer_Resize);
			this.KeyUp += new KeyEventHandler(Viewer_KeyUp);
			this.KeyPreview = true;

			Viewer_Resize(null, null);
			UpdateColors();

			debug = new Panel()
			{
				Size = new Size(3, 3),
				Location = new Point(-8, -8),
				BackColor = Color.Red,
				Visible = false,
			};
			screen.Controls.Add(debug);
			Tools.PointDebug = debug;

			bitmap = new Bitmap(480, 400);
			/*
			var r = new Random();
			for (var y = 0; y < bitmap.Height; y += 4)
			{
				for (var x = 0; x < bitmap.Width;)
				{
					var c = r.Next(2, 16) * 7;
					var l = r.Next(1, 4) * 4;
					for (var x2 = x; x2 < x + l && x2 < bitmap.Width; x2++)
					{
						bitmap.SetPixel(x2, y, Color.FromArgb(c, c, c));
						bitmap.SetPixel(x2, y + 1, Color.FromArgb(c, c, c));
						bitmap.SetPixel(x2, y + 2, Color.FromArgb(c, c, c));
						bitmap.SetPixel(x2, y + 3, Color.FromArgb(c, c, c));
					}
					x += l;
				}
			}
			*/

			if (args.Length == 1)
			{
				if (args[0].EndsWith("lisp", StringComparison.CurrentCultureIgnoreCase))
					OpenDoll(Path.GetDirectoryName(args[0]), Path.GetFileName(args[0]));
				else
					OpenDoll(args[0], null);
			}
			else if (args.Length == 2)
				OpenDoll(args[1], args[1]);
		}

		void Viewer_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Control && (e.KeyValue >= 48 && e.KeyValue <= 48 + Scene.Sets))
			{
				((ToolStripButton)tools.Items.Find("s" + Set.ToString(), false)[0]).Checked = false;
				Set = e.KeyValue - 49;
				if (Set == -1) Set = 9;
				((ToolStripButton)tools.Items.Find("s" + Set.ToString(), false)[0]).Checked = true;
				DrawScene();
			}
		}

		void Viewer_Resize(object sender, EventArgs e)
		{
			Config.WindowState = (int)this.WindowState; 
			if (screenContainer.ClientSize.Width > screen.Width)
				screen.Left = (screenContainer.ClientSize.Width / 2) - (screen.Width / 2);
			else
				screen.Left = 0;
			if (screenContainer.ClientSize.Height > screen.Height)
				screen.Top = (screenContainer.ClientSize.Height / 2) - (screen.Height / 2);
			else
				screen.Top = 0;
		}

		private void Screen_MouseDown(object sender, MouseEventArgs e)
		{
			if (Scene == null)
				return;

			var eX = e.X / Zoom;
			var eY = e.Y / Zoom;
			if (e.Button == MouseButtons.Right)
			{
				eX = (eX / 8) * 8;
				eY = (eY / 8) * 8;
			}
			var eLocation = new Point(eX, eY);
			
			var cell = default(Cell);
			var obj = Scene.GetObjectFromPoint(eLocation, out cell);
			editor.Pick(obj, cell);
			if (Hilight) DrawScene();
			if (obj != null && !obj.Locked)
			{
				held = obj;
				heldOffset = new Point(eX - obj.Position.X, eY - obj.Position.Y);
				if (held.Fix > 0) held.Fix--;
				fix = new Point(obj.Position.X, obj.Position.Y);
			}
			else
			{
				held = null;
			}
		}

		private void Screen_MouseMove(object sender, MouseEventArgs e)
		{
			if (Scene == null)
				return;

			var eX = e.X / Zoom;
			var eY = e.Y / Zoom;
			if (e.Button == MouseButtons.Right)
			{
				eX = (eX / 8) * 8;
				eY = (eY / 8) * 8;
			}
			var eLocation = new Point(eX, eY);

			if (held != null)
			{
				held.Position = new Point(eX - heldOffset.X, eY - heldOffset.Y);

				if (held.Position.X < 0)
					held.Position = new Point(0, held.Position.Y);
				if (held.Position.Y < 0)
					held.Position = new Point(held.Position.X, 0);
				if (held.Position.X + held.Bounds.Width > bitmap.Width)
					held.Position = new Point(bitmap.Width - held.Bounds.Width, held.Position.Y);
				if (held.Position.Y + held.Bounds.Height > bitmap.Height)
					held.Position = new Point(held.Position.X, bitmap.Height - held.Bounds.Height);

				if (held.Fix > 0)
				{
					if (Tools.Distance(fix, held.Position) > 16)
					{
						held.Position = new Point(fix.X, fix.Y);
						held = null;
					}
				}
				DrawScene();
				return;
			}

			var cell = default(Cell);
			var obj = Scene.GetObjectFromPoint(eLocation, out cell);
			var statusObj = obj;
			if (statusObj == null && HilightedCell != null)
				statusObj = HilightedCell.Object;
			if (statusObj != null)
			{
				status.Items[0].Text = statusObj.ID;
				status.Items[1].Text = string.Format("{0} by {1}", statusObj.Position.X, statusObj.Position.Y);
				status.Items[2].Image = (statusObj.Locked || statusObj.Fix > 0) ? global::KiSSLab.Properties.Resources.Lock : global::KiSSLab.Properties.Resources.Unlock;
				status.Items[3].Text = statusObj.Fix.ToString();
			}
			else
			{
				status.Items[0].Text = status.Items[1].Text = status.Items[3].Text = string.Empty;
				status.Items[2].Image = null;
			}

			if (obj == null)
				Cursor = Cursors.Default;
			else if (obj.Locked)
				Cursor = Cursors.No;
			else
				Cursor = Cursors.Hand;
		}

		private void Screen_MouseUp(object sender, MouseEventArgs e)
		{
			if (Scene == null)
				return;

			var eX = e.X / Zoom;
			var eY = e.Y / Zoom;
			var eLocation = new Point(eX, eY);

			if (held == null)
				return;

			Scene.Release(held);

			if (held != null)
			{
				dropped = held;
				held = null;
			}
		}

		private void Screen_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.DrawImage(bitmap, 0, 0, bitmap.Width * Zoom, bitmap.Height * Zoom);

			/*
			var pen = new Pen(Color.FromArgb(64, 255, 255, 255), 1);
			for (var x = 0; x < screen.Width; x += 8 * Zoom)
				e.Graphics.DrawLine(pen, x, 0, x, screen.Height);
			for (var y = 0; y < screen.Height; y += 8 * Zoom)
				e.Graphics.DrawLine(pen, 0, y, screen.Width, y);
			*/
		}

		private void Open_Click(object sender, EventArgs e)
		{
			using (var cdlg = new OpenFileDialog())
			{
				cdlg.Filter = "Doll files|*.zip;*.lisp";
				if (cdlg.ShowDialog() == DialogResult.Cancel)
					return;
				if (cdlg.FileName.EndsWith("lisp", StringComparison.CurrentCultureIgnoreCase))
					OpenDoll(Path.GetDirectoryName(cdlg.FileName), Path.GetFileName(cdlg.FileName));
				else
					OpenDoll(cdlg.FileName, null);
			}
		}

		private void Reset_Click(object sender, EventArgs e)
		{
			if (HilightedCell == null)
				return;
			var obj = HilightedCell.Object;
			obj.Position = obj.InitialPosition;
			DrawScene();
		}

#region Dark mode lightshit I mean switch
		private void UpdateColors()
		{
			var wantLight = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 0);
			if (wantLight == 1)
			{
				Colors.GreyBackground = Color.FromKnownColor(KnownColor.Control);
				Colors.HeaderBackground = Color.FromKnownColor(KnownColor.Control);
				Colors.BlueBackground = Color.FromKnownColor(KnownColor.ActiveCaption);
				Colors.DarkBlueBackground = Color.FromKnownColor(KnownColor.ActiveCaption);
				Colors.DarkBackground = Color.FromKnownColor(KnownColor.Control);
				Colors.MediumBackground = Color.FromKnownColor(KnownColor.Control);
				Colors.LightBackground = Color.FromKnownColor(KnownColor.Window);
				Colors.LighterBackground = Color.FromKnownColor(KnownColor.Window);
				Colors.LightestBackground = Color.FromKnownColor(KnownColor.Window);
				Colors.LightBorder = Color.FromKnownColor(KnownColor.WindowFrame);
				Colors.LightText = Color.FromKnownColor(KnownColor.ControlText);
				Colors.DisabledText = Color.FromKnownColor(KnownColor.GrayText);
				Colors.BlueHighlight = Color.FromKnownColor(KnownColor.HotTrack);
				Colors.BlueSelection = Color.FromKnownColor(KnownColor.Highlight);
				Colors.GreyHighlight = Color.FromKnownColor(KnownColor.ControlDark);
				Colors.GreySelection = Color.FromKnownColor(KnownColor.ControlDark);
				Colors.DarkGreySelection = Color.FromKnownColor(KnownColor.ControlDarkDark);
				Colors.DarkBlueBorder = Color.FromKnownColor(KnownColor.WindowFrame);
				Colors.LightBlueBorder = Color.FromKnownColor(KnownColor.ActiveCaption);
				Colors.DarkBlueBorder = Color.FromKnownColor(KnownColor.Highlight);
			}
			else
			{
				Colors.GreyBackground = Color.FromArgb(60, 63, 65);
				Colors.HeaderBackground = Color.FromArgb(57, 60, 62);
				Colors.BlueBackground = Color.FromArgb(66, 77, 95);
				Colors.DarkBlueBackground = Color.FromArgb(52, 57, 66);
				Colors.DarkBackground = Color.FromArgb(43, 43, 43);
				Colors.MediumBackground = Color.FromArgb(49, 51, 53);
				Colors.LightBackground = Color.FromArgb(69, 73, 74);
				Colors.LighterBackground = Color.FromArgb(95, 101, 102);
				Colors.LightestBackground = Color.FromArgb(178, 178, 178);
				Colors.LightBorder = Color.FromArgb(81, 81, 81);
				Colors.DarkBorder = Color.FromArgb(51, 51, 51);
				Colors.LightText = Color.FromArgb(220, 220, 220);
				Colors.DisabledText = Color.FromArgb(153, 153, 153);
				Colors.BlueHighlight = Color.FromArgb(104, 151, 187);
				Colors.BlueSelection = Color.FromArgb(75, 110, 175);
				Colors.GreyHighlight = Color.FromArgb(122, 128, 132);
				Colors.GreySelection = Color.FromArgb(92, 92, 92);
				Colors.DarkGreySelection = Color.FromArgb(82, 82, 82);
				Colors.DarkBlueBorder = Color.FromArgb(51, 61, 78);
				Colors.LightBlueBorder = Color.FromArgb(86, 97, 114);
				Colors.ActiveControl = Color.FromArgb(159, 178, 196);
			}
			editor.UpdateColors();
			foreach (var dd in menu.Items.OfType<ToolStripMenuItem>())
			{
				dd.BackColor = Colors.GreyBackground;
				foreach (var di in dd.DropDownItems.OfType<ToolStripMenuItem>())
				{
					di.BackColor = Colors.GreyBackground;
				}
			}
			foreach (var dd in tools.Items.OfType<ToolStripItem>())
			{
				dd.ForeColor = Colors.LightText;
			}
			foreach (var dd in status.Items.OfType<ToolStripStatusLabel>())
			{
				dd.BackColor = Colors.GreyBackground;
				dd.ForeColor = Colors.LightText;
			}
			screenContainer.BackColor = Colors.GreyBackground;
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x001A) //WM_SETTINGCHANGE
			{
				var lParam = System.Runtime.InteropServices.Marshal.PtrToStringAuto(m.LParam);
				if (lParam == "ImmersiveColorSet")
					UpdateColors();
			}
			base.WndProc(ref m);
		}
#endregion

		public void DrawScene()
		{
			if (Scene == null)
				return;
			Scene.DrawToBitmap(bitmap);
			if (Hilight && HilightedCell != null)
			{
				using (var g = Graphics.FromImage(bitmap))
				{
					var cell = HilightedCell;
					var obj = cell.Object;
					var color = Color.Lime;
					if (obj.Locked)
						color = Color.Red;
					else if (obj.Fix > 0)
						color = Color.Blue;
					var dotted = new Pen(color, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
					g.DrawRectangle(dotted, new Rectangle(obj.Position.X + cell.Offset.X, obj.Position.Y + cell.Offset.Y, cell.Image.Width - 1, cell.Image.Height - 1));
				}
			}
			screen.Refresh();
		}

		public void OpenDoll(string source, string configFile)
		{
			Mix.Initialize(source);
			if (configFile == null)
			{
				var configFiles = Mix.GetFilesWithPattern("*.lisp");
				if (configFiles.Length == 0)
				{
					DarkMessageBox.ShowError("No configuration files found.", Application.ProductName);
					return;
				}
				else if (configFiles.Length == 1)
				{
					configFile = configFiles[0];
				}
				else
				{
					var configPicker = new ConfigPicker(configFiles);
					configPicker.ShowDialog(this);
					configFile = configPicker.Choice;
				}
			}

			if (Scene != null)
			{
				foreach (var timer in Scene.Timers)
					timer.Value.Stop();
			}

			Scene = new Scene(this, configFile);

			for (var i = 0; i < 10; i++)
			{
				tools.Items.Find("s" + i.ToString(), false)[0].Visible = i < Scene.Sets;
				tools.Items.Find("p" + i.ToString(), false)[0].Visible = i < Scene.Palettes;
				((ToolStripButton)tools.Items.Find("s" + i.ToString(), false)[0]).Checked = i == 0;
				((ToolStripButton)tools.Items.Find("p" + i.ToString(), false)[0]).Checked = i == 0;
			}
			Scene.Palette = 0;
			Set = 0;

			bitmap = new Bitmap(Scene.ScreenWidth, Scene.ScreenHeight);
			//screen.BackgroundImage = bitmap;
			//screen.BackgroundImageLayout = ImageLayout.Stretch;
			//Scene.DrawToBitmap(bitmap);
			DrawScene();
			screen.ClientSize = new Size(bitmap.Width * Zoom, bitmap.Height * Zoom);
			editor.SetScene(Scene);
			//if (this.WindowState == FormWindowState.Normal)
			//	this.ClientSize = new Size((screen.Width / Zoom) + 16 + editor.Width, (screen.Height / Zoom) + 8 + menu.Height + tools.Height + status.Height);
			//HilightedCell = scene.Cells[0];
			Viewer_Resize(null, null);
			this.Text = string.Format("{0} - {1}", Application.ProductName, configFile);

			if (Scene.Events.ContainsKey("initialize"))
			{
				Scene.RunEvent(Scene.Events["initialize"]);
				DrawScene();
			}

			lastOpened = new[] { source, configFile };
		}
	}

	public class ConfigPicker : DarkDialog
	{
		private DarkListView list;
		public string Choice;

		public ConfigPicker(string[] configs)
		{
			Choice = configs[0]; //default
			this.Text = "Select a configuration";
			this.Font = new Font("Segoe UI", 9);
			this.ClientSize = new Size(343, 274);
			this.StartPosition = FormStartPosition.CenterParent;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.ControlBox = this.MinimizeBox = this.MaximizeBox = false;

			var panel = new Panel()
			{
				Dock = DockStyle.Fill,
				Padding = new Padding(15, 15, 15, 56),
			};
			this.Controls.Add(panel);
			//this.Padding = new Padding(8);
			list = new DarkListView()
			{
				Dock = DockStyle.Fill,
			};
			foreach (var config in configs)
			{
				list.Items.Add(new DarkListItem() { Text = config });
			}
			list.SelectItem(0);
			list.SelectedIndicesChanged += (s, e) => { Choice = list.Items[list.SelectedIndices[0]].Text; };
			list.DoubleClick += (s, e) => { Choice = list.Items[list.SelectedIndices[0]].Text; Close(); };
			//list.Items.AddRange(configs);
			//list.SelectedIndex = 0;
			//list.Click += (s, e) => { Choice = list.SelectedItem.ToString(); };
			//list.DoubleClick += (s, e) => { Choice = list.SelectedItem.ToString(); Close();	};
			panel.Controls.Add(list);
		}
	}

	public class About : DarkDialog
	{
		public string Choice;

		public About()
		{
			this.Text = string.Format("About {0}", Application.ProductName);
			this.Font = new Font("Segoe UI", 9);
			this.ClientSize = new Size(343, 274);
			this.StartPosition = FormStartPosition.CenterParent;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.ControlBox = this.MinimizeBox = this.MaximizeBox = false;

			var panel = new Panel()
			{
				Dock = DockStyle.Fill,
				Padding = new Padding(15, 15, 15, 56),
			};

			var header = new DarkLabel()
			{
				Font = new Font(this.Font.FontFamily, 18f),
				Text = Application.ProductName,
				Dock = DockStyle.Top,
				Height = 100,
				TextAlign = ContentAlignment.BottomCenter,
				Image = (new Icon(global::KiSSLab.Properties.Resources.app, new Size(64, 64))).ToBitmap(),
				ImageAlign = ContentAlignment.TopCenter,
				Padding = new Padding(0,0 ,0, 8),
			};
			var rest = new DarkLabel()
			{
				Text = string.Format("Because KiSS/GS is old and busted, I guess.\r\n\r\nVersion {0}\r\n\r\n© 2021 Firrhna Productions", Application.ProductVersion),
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.TopCenter,
			};
			panel.Controls.Add(rest);
			panel.Controls.Add(header);

			this.Controls.Add(panel);
		}
	}
}
