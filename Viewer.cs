using System;
using System.Collections.Generic;
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
	public partial class Viewer : DarkForm
	{
		private Bitmap bitmap;
		private Object held, dropped;
		private Point heldOffset, fix;
		private Point lastClick;
		private Panel debug;

		public static Scene Scene;
		public static int Zoom = 1;

		public static int Set;
		public static bool Hilight;
		public Cell HilightedCell;

		private HighPrecisionTimer.MultimediaTimer AlarmTimer;
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

			InitializeComponent();

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
			if (Config.ZoomLevel < 1 || Config.ZoomLevel > 3)
				Config.ZoomLevel = 1;
			Zoom = Config.ZoomLevel;
			if (!string.IsNullOrWhiteSpace(Config.AutoLoad))
				args = new string[] { Config.AutoLoad };

			this.Text = Application.ProductName;
			this.DoubleBuffered = true;
			this.Icon = global::KiSSLab.Properties.Resources.app;

			highlightToolStripButton.Checked = Hilight;

			var after = tools.Items.IndexOf(nextSetToolStripButton);
			for (var i = 0; i < 10; i++)
			{
				after++;
				tools.Items.Insert(after, new ToolStripButton(
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
			after = tools.Items.IndexOf(nextPalToolStripButton);
			for (var i = 0; i < 10; i++)
			{
				after++;
				tools.Items.Insert(after, new ToolStripButton(
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
			after++;
			for (var i = 1; i <= 3; i++)
			{
				after++;
				tools.Items.Insert(after, new ToolStripButton(
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
			editorToolStripButton.Checked = Config.Editor == 1;

			/*
			screen = new PictureBox()
			{
				ClientSize = new Size(480, 400),
				BorderStyle = BorderStyle.FixedSingle,
			};
			*/
			editor.Visible = Config.Editor == 1;

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
			if (e.Control && (e.KeyValue >= 48 && e.KeyValue <= 57))
			{
				((ToolStripButton)tools.Items.Find("s" + Set.ToString(), false)[0]).Checked = false;
				if (e.KeyValue == 48)
					Set = 0;
				else
					Set = e.KeyValue - 48;
				if (Set >= Scene.Sets)
					Set = Scene.Sets - 1;
				((ToolStripButton)tools.Items.Find("s" + Set.ToString(), false)[0]).Checked = true;
				DrawScene();
			}
			else if (e.Control && e.KeyValue == 9)
			{
				e.Handled = true;
				NextSet_Click(null, null);
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
			if (drawing)
				return;

			var eX = e.X / Zoom;
			var eY = e.Y / Zoom;
			if (e.Button == MouseButtons.Right)
			{
				eX = (eX / 8) * 8;
				eY = (eY / 8) * 8;
			}
			var eLocation = new Point(eX, eY);
			lastClick = eLocation;
			
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
			if (drawing)
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
			if (drawing)
				return;

			var eX = e.X / Zoom;
			var eY = e.Y / Zoom;
			var eLocation = new Point(eX, eY);
			if (eLocation.Equals(lastClick))
			{
				Scene.Click(HilightedCell);
				Scene.Viewer.DrawScene();
			}

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

		private delegate void SafeCallDelegate();
		private void RefreshScreen()
		{
			if (screen.InvokeRequired)
			{
				var d = new SafeCallDelegate(RefreshScreen);
				screen.Invoke(d, new object[] { });
			}
			else
			{
				screen.Refresh();
			}
		}

		private bool drawing;

		public void DrawScene()
		{
			if (Scene == null)
				return;
			if (drawing)
				return;

			drawing = true;
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

			//screen.Refresh();
			RefreshScreen();
			drawing = false;
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

			AlarmTimer = new HighPrecisionTimer.MultimediaTimer();
			AlarmTimer.Interval = 5;
			//AlarmTimer.Tick += new EventHandler(AlarmTimer_Tick);
			AlarmTimer.Elapsed += new EventHandler(AlarmTimer_Tick);
			AlarmTimer.Start();

			lastOpened = new[] { source, configFile };
		}

		void AlarmTimer_Tick(object sender, EventArgs e)
		{
			var oneDied = false;
			foreach (var t in Scene.Timers)
			{
				var timer = t.Value;
				if (timer != null)
				{
					if (timer.TimeLeft > 0)
					{
						timer.TimeLeft--;
						continue;
					}

					var act = timer.Action;
					if (act is List<object>)
					{
						Scene.RunEvent((List<object>)act);
					}
					else
					{
						var alarmID = string.Format("alarm|{0}", act.GetHashCode());
						if (Scene.Events.ContainsKey(alarmID))
						{
							Scene.RunEvent(Scene.Events[alarmID]);
						}
					}
					Scene.Viewer.DrawScene();

					if (timer.Repeat)
						timer.TimeLeft = timer.Interval;
					else
						oneDied = true;
					break;
				}
			}
			if (oneDied)
			{
				var dead = Scene.Timers.Where(t => t.Value.TimeLeft == 0).ToList();
				foreach (var deadOne in dead)
					Scene.Timers.Remove(deadOne.Key);
			}
		}

		private void Viewer_Move(object sender, EventArgs e)
		{
			if (this.WindowState != FormWindowState.Normal)
				return;
			Config.WindowLeft = this.Location.X;
			Config.WindowTop = this.Location.Y;
		}

		private void Viewer_ResizeEnd(object sender, EventArgs e)
		{
			if (this.WindowState != FormWindowState.Normal)
				return;
			Config.WindowWidth = this.ClientSize.Width;
			Config.WindowHeight = this.ClientSize.Height;
		}

		private void Viewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			Config.Save();
		}

		private void Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ReOpen_Click(object sender, EventArgs e)
		{
			OpenDoll(lastOpened[0], lastOpened[1]);
		}

		private void CopyCell_Click(object sender, EventArgs e)
		{
			Clipboard.SetImage(HilightedCell.Image);
		}

		private void About_Click(object sender, EventArgs e)
		{
			(new About()).ShowDialog(this);
		}

		private void Highlight_Click(object sender, EventArgs e)
		{
			Hilight = ((ToolStripButton)sender).Checked;
			DrawScene();
		}

		private void NextSet_Click(object sender, EventArgs e)
		{
			((ToolStripButton)tools.Items.Find("s" + Set.ToString(), false)[0]).Checked = false;
			Set++;
			if (Set == Scene.Sets) Set = 0;
			if (dropped != null) dropped.LastCollidedWith = null;
			DrawScene();
			((ToolStripButton)tools.Items.Find("s" + Set.ToString(), false)[0]).Checked = true;
		}

		private void NextPal_Click(object sender, EventArgs e)
		{
			((ToolStripButton)tools.Items.Find("p" + Scene.Palette.ToString(), false)[0]).Checked = false;
			Scene.Palette++;
			if (Scene.Palette == Scene.Palettes) Scene.Palette = 0;
			DrawScene();
			((ToolStripButton)tools.Items.Find("p" + Scene.Palette.ToString(), false)[0]).Checked = true;
		}

		private void ShowEditor_Click(object sender, EventArgs e)
		{
			var me = (ToolStripButton)sender;
			me.Checked = !me.Checked;
			editor.Visible = me.Checked;
			Viewer_Resize(null, null);
			Config.Editor = me.Checked ? 1 : 0;
		}
	}
}
