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
		private Part held, dropped;
		private Point heldOffset, fix;
		private Point lastClick;
		private Point? dragStart = null;
		private Point startScroll;

		public Scene Scene;

		public bool Hilight;
		public Cel HilightedCel;

		private System.Windows.Forms.Timer AlarmTimer;
		private string[] lastOpened;
		public static SoundSystem Sound;
		public static MyConfig Config;

		public Viewer(string[] args)
		{
			InitializeComponent();

			Config = new MyConfig(@"Kawa\KiSS Thing");

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
			if (!string.IsNullOrWhiteSpace(Config.AutoLoad) && args.Length == 0)
				args = new string[] { Config.AutoLoad };

			this.Text = Application.ProductName;
			this.DoubleBuffered = true;
			this.Icon = global::KiSSLab.Properties.Resources.app;

			highlightToolStripButton.Checked = Hilight;

			var after = mainToolStrip.Items.IndexOf(nextSetToolStripButton);
			for (var i = 0; i < 10; i++)
			{
				after++;
				mainToolStrip.Items.Insert(after, new ToolStripButton(
					i.ToString(), null, (s, e) =>
					{
						Scene.Set = int.Parse(((ToolStripButton)s).Text);
						if (dropped != null) dropped.LastCollidedWith = null;
						var maybe = string.Format("set|{0}", Scene.Set);
						if (Scene.Events.ContainsKey(maybe))
							Scene.RunEvent(Scene.Events[maybe]);
						UpdatePalAndSetButtons();
						DrawScene();
					}, "s" + i.ToString()
				)
				{
					ToolTipText = string.Format("Switch to set {0}", i),
					DisplayStyle = ToolStripItemDisplayStyle.Text,
					Checked = (i == 0),
				});
			}
			after = mainToolStrip.Items.IndexOf(nextPalToolStripButton);
			for (var i = 0; i < 10; i++)
			{
				after++;
				mainToolStrip.Items.Insert(after, new ToolStripButton(
					i.ToString(), null, (s, e) =>
					{
						Scene.Palette = int.Parse(((ToolStripButton)s).Text);
						var maybe = string.Format("col|{0}", Scene.Palette);
						if (Scene.Events.ContainsKey(maybe))
							Scene.RunEvent(Scene.Events[maybe]);
						UpdatePalAndSetButtons();
						DrawScene();
					}, "p" + i.ToString()
				)
				{
					ToolTipText = string.Format("Switch to palette {0}", i),
					DisplayStyle = ToolStripItemDisplayStyle.Text,
					Checked = (i == 0),
				});
			}
			propertiesToolStripButton.Checked = Config.Editor == 1;

			toolStripZoomBar.Value = Config.ZoomLevel;

			editor.Visible = Config.Editor == 1;
			if (!editor.Visible)
				screenContainerPanel.Width += editor.Width;

			Viewer_Resize(null, null);
			UpdateColors();

			bitmap = new Bitmap(480, 400);
			Sound = new SoundSystem();

			AlarmTimer = new System.Windows.Forms.Timer();
			AlarmTimer.Interval = 5;
			AlarmTimer.Tick += new EventHandler(AlarmTimer_Tick);

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
				((ToolStripButton)mainToolStrip.Items.Find("s" + Scene.Set.ToString(), false)[0]).Checked = false;
				if (e.KeyValue == 48)
					Scene.Set = 0;
				else
					Scene.Set = e.KeyValue - 48;
				if (Scene.Set >= Scene.Sets)
					Scene.Set = Scene.Sets - 1;
				((ToolStripButton)mainToolStrip.Items.Find("s" + Scene.Set.ToString(), false)[0]).Checked = true;
				DrawScene();
			}
			else if (e.Control && e.KeyValue == 9)
			{
				if (e.Shift)
					PreviousSet_Click(null, null);
				else
					NextSet_Click(null, null);
			}
		}

		void Viewer_Resize(object sender, EventArgs e)
		{
			Config.WindowState = (int)this.WindowState;
			if (screenContainerPanel.ClientSize.Width > screenPictureBox.Width)
				screenPictureBox.Left = (screenContainerPanel.ClientSize.Width / 2) - (screenPictureBox.Width / 2);
			else
				screenPictureBox.Left = 0;
			if (screenContainerPanel.ClientSize.Height > screenPictureBox.Height)
				screenPictureBox.Top = (screenContainerPanel.ClientSize.Height / 2) - (screenPictureBox.Height / 2);
			else
				screenPictureBox.Top = 0;
		}

		private void Screen_MouseDown(object sender, MouseEventArgs e)
		{
			if (Scene == null)
				return;

			var eX = e.X / Scene.Zoom;
			var eY = e.Y / Scene.Zoom;
			var realLocation = new Point(eX, eY);
			if (gridToolStripButton.Checked)
			{
				eX = (eX / 8) * 8;
				eY = (eY / 8) * 8;
			}
			var eLocation = new Point(eX, eY);
			lastClick = eLocation;
			
			var cel = default(Cel);
			var part = Scene.GetPartFromPoint(realLocation, out cel);
			if (part != null)
			{
				unfixToolStripMenuItem.Enabled = unfixContextMenuItem.Enabled = part.Locked;
				refixToolStripMenuItem.Enabled = refixContextMenuItem.Enabled = !part.Locked;
			}
			else
			{
				dragStart = screenContainerPanel.PointToClient(screenPictureBox.PointToScreen(realLocation));
				startScroll = screenContainerPanel.AutoScrollPosition;
			}

			editor.Pick(part, cel);
			if (Hilight) DrawScene();

			if (part != null && e.Button == MouseButtons.Left)
			{
				Scene.Catch(part, cel);
				if (!part.Locked)
				{
					held = part;
					heldOffset = new Point(eX - part.Position.X, eY - part.Position.Y);
					if (held.Fix > 0)
					{
						held.Fix--;
						if (held.Fix == 0)
						{
							var maybe = string.Format("unfix|{0}", cel);
							if (Scene.Events.ContainsKey(maybe))
								Scene.RunEvent(Scene.Events[maybe]);
							else
							{
								maybe = string.Format("unfix|{0}", held);
								if (Scene.Events.ContainsKey(maybe))
									Scene.RunEvent(Scene.Events[maybe]);
							}
						}
					}
					fix = new Point(part.Position.X, part.Position.Y);
				}
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

			var eX = e.X / Scene.Zoom;
			var eY = e.Y / Scene.Zoom;
			var realLocation = new Point(eX, eY);
			if (gridToolStripButton.Checked)
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

			var cel = default(Cel);
			var part = Scene.GetPartFromPoint(realLocation, out cel);
			var statusPart = part;
			if (statusPart == null && HilightedCel != null)
				statusPart = HilightedCel.Part;
			if (statusPart != null && cel != null)
			{
				mainStatusStrip.Items[0].Text = string.Format("{0} » {1}", statusPart.ID, cel.ID);
				mainStatusStrip.Items[1].Text = string.Format("{0} by {1}", statusPart.Position.X, statusPart.Position.Y);
				mainStatusStrip.Items[2].Image = (statusPart.Locked || statusPart.Fix > 0) ? refixToolStripMenuItem.Image : unfixToolStripMenuItem.Image;
				mainStatusStrip.Items[3].Text = statusPart.Fix.ToString();
			}
			else
			{
				mainStatusStrip.Items[0].Text = mainStatusStrip.Items[1].Text = mainStatusStrip.Items[3].Text = string.Empty;
				mainStatusStrip.Items[2].Image = null;
			}

			if (part == null)
			{
				if (dragStart == null)
					Cursor = Cursors.Default;
				else
				{
					Cursor = Cursors.SizeAll;
					var dragPoint = screenContainerPanel.PointToClient(screenPictureBox.PointToScreen(realLocation));
					if (screenContainerPanel.HorizontalScroll.Visible)
					{
						var x = -startScroll.X + (dragStart.Value.X - dragPoint.X);
						var y = -startScroll.Y + (dragStart.Value.Y - dragPoint.Y);
						screenContainerPanel.AutoScrollPosition = new Point(x, y);
					}
				}
			}
			else if (part.Locked)
				Cursor = Cursors.Default;
			else
				Cursor = Cursors.Hand;
		}

		private void Screen_MouseUp(object sender, MouseEventArgs e)
		{
			if (Scene == null)
				return;
			//if (drawing)
			//	return;

			if (dragStart != null)
			{
				dragStart = null;
				screenContainerPanel.HorizontalScroll.Value = screenContainerPanel.HorizontalScroll.Value;
			}

			var eX = e.X / Scene.Zoom;
			var eY = e.Y / Scene.Zoom;
			var eLocation = new Point(eX, eY);
			if (eLocation.Equals(lastClick) && HilightedCel != null)
			{
				//Scene.Click(HilightedCel);
			}

			/*
			if (held == null)
			{
				Scene.Release(null, HilightedCel);
				Scene.Viewer.DrawScene();
				return;
			}
			*/

			editor.Pick(held, HilightedCel);
			if (e.Button == MouseButtons.Left)
				Scene.Release(held, HilightedCel);

			if (held != null)
			{
				dropped = held;
				held = null;
			}

			Scene.Viewer.DrawScene();
		}

		private void Screen_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var eX = e.X / Scene.Zoom;
				var eY = e.Y / Scene.Zoom;
				var realLocation = new Point(eX, eY);

				var cel = default(Cel);
				var part = Scene.GetPartFromPoint(realLocation, out cel);
				if (part == null || cel == null)
					return;

				var screenLocation = screenPictureBox.PointToScreen(new Point(e.X, e.Y));

				celMenuHeader.Text = cel.ID;
				celContextMenu.Show(screenLocation);
			}
		}

		private void Screen_Paint(object sender, PaintEventArgs e)
		{
			if (Scene == null)
				return;
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.DrawImage(bitmap, 0, 0, bitmap.Width * Scene.Zoom, bitmap.Height * Scene.Zoom);

			/*
			var pen = new Pen(Color.FromArgb(64, 255, 255, 255), 1);
			for (var x = 0; x < screenPictureBox.Width; x += 8 * Zoom)
				e.Graphics.DrawLine(pen, x, 0, x, screenPictureBox.Height);
			for (var y = 0; y < screenPictureBox.Height; y += 8 * Zoom)
				e.Graphics.DrawLine(pen, 0, y, screenPictureBox.Width, y);
			*/
		}

		private void Open_Click(object sender, EventArgs e)
		{
			using (var cdlg = new OpenFileDialog())
			{
				cdlg.Filter = "Doll files|*.zip;*.lisp";
				if (cdlg.ShowDialog() == DialogResult.Cancel)
					return;
				if (sender != openExpansionToolStripMenuItem)
					Mix.Reset();
				if (sender == openInNewToolStripMenuItem)
				{
					var newViewer = new Viewer(new[] { cdlg.FileName });
					newViewer.Show();
					return;
				}
				if (cdlg.FileName.EndsWith("lisp", StringComparison.CurrentCultureIgnoreCase))
					OpenDoll(Path.GetDirectoryName(cdlg.FileName), Path.GetFileName(cdlg.FileName));
				else
					OpenDoll(cdlg.FileName, null);
			}
		}

		private void Reset_Click(object sender, EventArgs e)
		{
			if (HilightedCel == null)
				return;
			var part = HilightedCel.Part;
			part.Position = part.InitialPosition;
			DrawScene();
		}
		
		private void Unfix_Click(object sender, EventArgs e)
		{
			if (HilightedCel == null)
				return;
			var part = HilightedCel.Part;
			part.Fix = 0;
			var maybe = string.Format("unfix|{0}", HilightedCel);
			if (Scene.Events.ContainsKey(maybe))
				Scene.RunEvent(Scene.Events[maybe]);
			else
			{
				maybe = string.Format("unfix|{0}", part);
				if (Scene.Events.ContainsKey(maybe))
					Scene.RunEvent(Scene.Events[maybe]);
			}
		}

		private void Refix_Click(object sender, EventArgs e)
		{
			if (HilightedCel == null)
				return;
			var part = HilightedCel.Part;
			part.Fix = part.InitialFix;
		}

#region Dark mode lightshit I mean switch
		private void UpdateColors()
		{
			var wantLight = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 0);
			
			//Logic: AppsUseLightTheme is TRUE for LIGHT. Config.ForcedLight is 0 for MATCH, 1 for LIGHT, 2 for DARK so kinda the opposite.
			if (Config.ForcedLight != 0)
				wantLight = (Config.ForcedLight - 1) ^ 1;

			if (wantLight == 1)
			{
				Colors.GreyBackground = Color.FromArgb(240, 240, 240);
				Colors.HeaderBackground = Color.FromArgb(240, 240, 240);
				Colors.BlueBackground = Color.FromArgb(153, 180, 209);
				Colors.DarkBlueBackground = Color.FromArgb(153, 180, 209);
				Colors.DarkBackground = Color.FromArgb(240, 240, 240);
				Colors.MediumBackground = Color.FromArgb(240, 240, 240);
				Colors.LightBackground = Color.FromArgb(255, 255, 255);
				Colors.LighterBackground = Color.FromArgb(255, 255, 255);
				Colors.LightestBackground = Color.FromArgb(255, 255, 255);
				Colors.LightBorder = Color.FromArgb(227, 227, 227);
				Colors.DarkBorder = Color.FromArgb(160, 160, 160);
				Colors.LightText = Color.FromArgb(0, 0, 0);
				Colors.DisabledText = Color.FromArgb(109, 109, 109);
				Colors.BlueHighlight = Color.FromArgb(0, 102, 204);
				Colors.BlueSelection = Color.FromArgb(0, 120, 215);
				Colors.GreyHighlight = Color.FromArgb(160, 160, 160);
				Colors.GreySelection = Color.FromArgb(160, 160, 160);
				Colors.DarkGreySelection = Color.FromArgb(105, 105, 105);
				Colors.DarkBlueBorder = Color.FromArgb(0, 120, 215);
				Colors.LightBlueBorder = Color.FromArgb(153, 180, 209);
				Colors.DarkBlueBorder = Color.FromArgb(0, 120, 215);

				openToolStripButton.Image = openToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Open_Light;
				highlightToolStripButton.Image = showHighlightToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Highlight_Light;
				gridToolStripButton.Image = alignToGridToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Grid_Light;
				propertiesToolStripButton.Image = propertiesToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Properties_Light;
				nextSetToolStripButton.Image = global::KiSSLab.Properties.Resources.CycleSets_Light;
				nextPalToolStripButton.Image = global::KiSSLab.Properties.Resources.Colors_Light;
				copyCelToolStripMenuItem.Image = copyCelContextMenuItem.Image = global::KiSSLab.Properties.Resources.Copy_Light;
				exitToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Exit_Light;
				reopenToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Reset_Light;
				resetPositionToolStripMenuItem.Image = resetPositionContextMenuItem.Image = global::KiSSLab.Properties.Resources.Undo_Light;
				unfixToolStripMenuItem.Image = unfixContextMenuItem.Image = global::KiSSLab.Properties.Resources.Unlock_Light;
				refixToolStripMenuItem.Image = refixContextMenuItem.Image = global::KiSSLab.Properties.Resources.Lock_Light;
				zoomToolStripLabel.Image = global::KiSSLab.Properties.Resources.Zoom_Light;
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

				openToolStripButton.Image = openToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Open_Dark;
				highlightToolStripButton.Image = showHighlightToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Highlight_Dark;
				gridToolStripButton.Image = alignToGridToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Grid_Dark;
				propertiesToolStripButton.Image = propertiesToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Properties_Dark;
				nextSetToolStripButton.Image = global::KiSSLab.Properties.Resources.CycleSets_Dark;
				nextPalToolStripButton.Image = global::KiSSLab.Properties.Resources.Colors_Dark;
				copyCelToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Copy_Dark;
				exitToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Exit_Dark;
				reopenToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Reset_Dark;
				resetPositionToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Undo_Dark;
				unfixToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Unlock_Dark;
				refixToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Lock_Dark;
				zoomToolStripLabel.Image = global::KiSSLab.Properties.Resources.Zoom_Dark;
			}
			editor.UpdateColors();
			foreach (var dd in mainMenuStrip.Items.OfType<ToolStripMenuItem>())
			{
				dd.BackColor = Colors.GreyBackground;
				foreach (var di in dd.DropDownItems.OfType<ToolStripMenuItem>())
				{
					di.BackColor = Colors.GreyBackground;
				}
			}
			foreach (var dd in mainToolStrip.Items.OfType<ToolStripItem>())
			{
				dd.ForeColor = Colors.LightText;
			}
			foreach (var dd in mainStatusStrip.Items.OfType<ToolStripStatusLabel>())
			{
				dd.BackColor = Colors.GreyBackground;
				dd.ForeColor = Colors.LightText;
			}
			foreach (var dd in celContextMenu.Items.OfType<ToolStripMenuItem>().Skip(1))
			{
				dd.BackColor = Colors.GreyBackground;
			}
			screenContainerPanel.BackColor = Colors.DarkBackground;
			celMenuHeader.BackColor = Colors.BlueBackground;
			celMenuHeader.ForeColor = Colors.LightText;
			toolStripZoomBar.BackColor = Colors.GreyBackground;
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
			if (Hilight && HilightedCel != null)
			{
				using (var g = Graphics.FromImage(bitmap))
				{
					var cel = HilightedCel;
					var part = cel.Part;
					var color = Color.Lime;
					if (part.Locked)
						color = Color.Red;
					else if (part.Fix > 0)
						color = Color.Blue;
					var dotted = new Pen(color, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
					g.DrawRectangle(dotted, new Rectangle(part.Position.X + cel.Offset.X, part.Position.Y + cel.Offset.Y, cel.Image.Width - 1, cel.Image.Height - 1));
				}
			}

			screenPictureBox.Refresh();
		}

		public void OpenDoll(string source, string configFile)
		{
			Sound.StopEverything();

			Mix.Load(source);
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
				mainToolStrip.Items.Find("s" + i.ToString(), false)[0].Enabled = i < Scene.Sets;
				mainToolStrip.Items.Find("p" + i.ToString(), false)[0].Enabled = i < Scene.Palettes;
				((ToolStripButton)mainToolStrip.Items.Find("s" + i.ToString(), false)[0]).Checked = i == 0;
				((ToolStripButton)mainToolStrip.Items.Find("p" + i.ToString(), false)[0]).Checked = i == 0;
			}
			Scene.Palette = 0;
			Scene.Set = 0;
			Scene.Zoom = Config.ZoomLevel;

			bitmap = new Bitmap(Scene.ScreenWidth, Scene.ScreenHeight);
			screenPictureBox.ClientSize = new Size(bitmap.Width * Scene.Zoom, bitmap.Height * Scene.Zoom);
			editor.SetScene(Scene);
			Viewer_Resize(null, null);
			this.Text = string.Format("{0} - {1}", Application.ProductName, configFile);

			if (Scene.Events.ContainsKey("initialize"))
				Scene.RunEvent(Scene.Events["initialize"]);
			if (Scene.Events.ContainsKey("begin"))
				Scene.RunEvent(Scene.Events["begin"]);

			DrawScene();

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
					if (act == null)
					{
						oneDied = true;
						break;
					}
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

		private void CopyCel_Click(object sender, EventArgs e)
		{
			Clipboard.SetImage(HilightedCel.Image);
		}

		private void About_Click(object sender, EventArgs e)
		{
			(new About()).ShowDialog(this);
		}

		private void Highlight_Click(object sender, EventArgs e)
		{
			if (sender == highlightToolStripButton)
				showHighlightToolStripMenuItem.Checked = highlightToolStripButton.Checked;
			else
				highlightToolStripButton.Checked = showHighlightToolStripMenuItem.Checked;
			Hilight = highlightToolStripButton.Checked;
			DrawScene();
		}

		private void NextSet_Click(object sender, EventArgs e)
		{
			if (Scene.Set < Scene.Sets - 1)
				Scene.Set++;
			else
				Scene.Set = 0;
			if (dropped != null) dropped.LastCollidedWith = null;
			var maybe = string.Format("set|{0}", Scene.Set);
			if (Scene.Events.ContainsKey(maybe))
				Scene.RunEvent(Scene.Events[maybe]);
			UpdatePalAndSetButtons();
			DrawScene();
		}

		private void PreviousSet_Click(object sender, EventArgs e)
		{
			if (Scene.Set == 0)
				Scene.Set = Scene.Sets - 1;
			else
				Scene.Set--;
			if (dropped != null) dropped.LastCollidedWith = null;
			var maybe = string.Format("set|{0}", Scene.Set);
			if (Scene.Events.ContainsKey(maybe))
				Scene.RunEvent(Scene.Events[maybe]);
			UpdatePalAndSetButtons();
			DrawScene();
		}

		private void NextPal_Click(object sender, EventArgs e)
		{
			((ToolStripButton)mainToolStrip.Items.Find("p" + Scene.Palette.ToString(), false)[0]).Checked = false;
			Scene.Palette++;
			if (Scene.Palette == Scene.Palettes) Scene.Palette = 0;
			var maybe = string.Format("col|{0}", Scene.Palette);
			if (Scene.Events.ContainsKey(maybe))
				Scene.RunEvent(Scene.Events[maybe]);
			DrawScene();
			((ToolStripButton)mainToolStrip.Items.Find("p" + Scene.Palette.ToString(), false)[0]).Checked = true;
		}

		private void ShowEditor_Click(object sender, EventArgs e)
		{
			if (sender == propertiesToolStripMenuItem)
				propertiesToolStripButton.Checked = propertiesToolStripMenuItem.Checked;
			else
				propertiesToolStripMenuItem.Checked = propertiesToolStripButton.Checked;
			editor.Visible = propertiesToolStripButton.Checked;
			Viewer_Resize(null, null);
			Config.Editor = propertiesToolStripButton.Checked ? 1 : 0;
		}

		private void gridToolStripButton_CheckedChanged(object sender, EventArgs e)
		{
			alignToGridToolStripMenuItem.Checked = gridToolStripButton.Checked;
		}

		public void UpdatePalAndSetButtons()
		{
			for (var i = 0; i < 10; i++)
			{
				((ToolStripButton)mainToolStrip.Items.Find("s" + i, false)[0]).Checked = false;
				((ToolStripButton)mainToolStrip.Items.Find("p" + i, false)[0]).Checked = false;
			}
			((ToolStripButton)mainToolStrip.Items.Find("s" + Scene.Set.ToString(), false)[0]).Checked = true;
			((ToolStripButton)mainToolStrip.Items.Find("p" + Scene.Palette.ToString(), false)[0]).Checked = true;
		}

		private void toolStripZoomBar_ValueChanged(object sender, EventArgs e)
		{
			Config.ZoomLevel = toolStripZoomBar.Value;
			if (bitmap != null) screenPictureBox.ClientSize = new Size(bitmap.Width * Config.ZoomLevel, bitmap.Height * Config.ZoomLevel);
			if (Scene != null) Scene.Zoom = Config.ZoomLevel;
			Viewer_Resize(null, null);
			DrawScene();
		}
	}

	public class MyConfig : RegistryConfig
	{
		[Setting(-1)]
		public int WindowLeft { get; set; }
		[Setting(-1)]
		public int WindowTop { get; set; }
		[Setting(816)]
		public int WindowWidth { get; set; }
		[Setting(484)]
		public int WindowHeight { get; set; }
		[Setting(0)]
		public int WindowState { get; set; }
		[Setting(1)]
		public int ZoomLevel { get; set; }
		[Setting("")]
		public string AutoLoad { get; set; }
		[Setting(0)]
		public int Editor { get; set; }
		[Setting("")]
		public string AutoCollide { get; set; }
		[Setting(0)]
		public int ForcedLight { get; set; }

		public MyConfig(string path) : base(path) { }
	}

	[System.Windows.Forms.Design.ToolStripItemDesignerAvailability(System.Windows.Forms.Design.ToolStripItemDesignerAvailability.ToolStrip | System.Windows.Forms.Design.ToolStripItemDesignerAvailability.StatusStrip)]
	public class ToolStripZoomBarItem : ToolStripControlHost
	{
		public event EventHandler ValueChanged;

		public int Value
		{
			get { return ((TrackBar)this.Control).Value; }
			set { ((TrackBar)this.Control).Value = value; }
		}
		public override Color BackColor
		{
			get { return ((TrackBar)this.Control).BackColor; }
			set { ((TrackBar)this.Control).BackColor = value; }
		}

		public ToolStripZoomBarItem() : base(new TrackBar())
		{
			TrackBar tb = (TrackBar)this.Control;
			tb.SetRange(1, 3);
			tb.LargeChange = tb.SmallChange = 1;
			tb.TickStyle = TickStyle.None;

			tb.ValueChanged += (s, e) =>
			{
				if (ValueChanged != null)
					ValueChanged(s, e);
			};
		}
	}

	public class ControlScrollFilter : IMessageFilter
	{
		public bool PreFilterMessage(ref Message m)
		{
			switch (m.Msg)
			{
				case 0x020A: //WM.MOUSEWHEEL:
				case 0x020E: //WM.MOUSEHWHEEL:
				{
					var hControlUnderMouse = Native.WindowFromPoint(new Point((int)m.LParam));

					if (hControlUnderMouse == m.HWnd)
						return false;

					Native.SendMessage(hControlUnderMouse, (uint)m.Msg, m.WParam, m.LParam);
					return true;
				}
			}

			return false;
		}

		internal sealed class Native
		{
			[System.Runtime.InteropServices.DllImport("user32.dll")]
			internal static extern IntPtr WindowFromPoint(Point point);

			[System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
			internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);
		}
	}
}
