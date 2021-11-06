using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DarkUI.Config;
using DarkUI.Controls;
using Kawa.SExpressions;

namespace KiSSLab
{
	public partial class Editor : UserControl
	{
		private bool locked;
		private DarkCheckBox[] celMapCheckBoxes;

		public Editor()
		{
			InitializeComponent();

			celMapCheckBoxes = new DarkCheckBox[10];
			for (var i = 0; i < 10; i++)
			{
				var setCheck = celMapCheckBoxes[i] = new DarkCheckBox()
				{
					Text = i.ToString(),
					AutoSize = true,
				};
				setCheck.CheckedChanged += new EventHandler(celMappedCheckBox_CheckedChanged);
				flowLayoutPanel2.Controls.Add(setCheck);
			}

			foreach (var font in FontFamily.Families)
			{
				celLabelFont.Items.Add(font.Name);
			}
		}

		public void SetScene(Scene scene)
		{
			partPosXTextBox.Maximum = scene.ScreenWidth;
			partPosYTextBox.Maximum = scene.ScreenHeight;

			this.parts.Items.Clear();
			this.parts.Items.AddRange(scene.Parts.ToArray());
			this.parts.SelectedIndex = 0;

			this.cels.Items.Clear();
			this.cels.Items.AddRange(scene.Cels.ToArray());
			this.cels.SelectedIndex = 0;

			for (var i = scene.Sets; i < 10; i++)
			{
				celMapCheckBoxes[i].Enabled = celMapCheckBoxes[i].Checked = false;
			}

			if (scene.HighlightedPart != null)
				this.parts.SelectedItem = scene.HighlightedPart;
			if (scene.HighlightedCel != null)
				this.cels.SelectedItem = scene.HighlightedCel;
		}

		public void Pick(Part part, Cel cel)
		{
			if (part != null)
			{
				if (parts.SelectedItem == part)
					parts_SelectedItemChanged(null, null);
				else
					parts.SelectedItem = part;
			}
			if (cel != null)
			{
				if (cels.SelectedItem == cel)
					cels_SelectedItemChanged(null, null);
				else
					cels.SelectedItem = cel;
				((Viewer)this.ParentForm).HilightedCel = cel;
			}
			else
				((Viewer)this.ParentForm).HilightedCel = null;
		}

		#region More darkmode lightswitch
		public void UpdateColors()
		{
			this.BackColor = Colors.DarkBackground;

			foreach (var s in this.Controls.OfType<DarkSectionPanel>())
			{
				s.BackColor = Colors.GreyBackground;

				foreach (var p in s.Controls.OfType<FlowLayoutPanel>())
				{
					foreach (var c in p.Controls.OfType<DarkLabel>())
					{
						c.ForeColor = Colors.LightText;
					}
					foreach (var c in p.Controls.OfType<DarkTextBox>())
					{
						c.BackColor = Colors.LightBackground;
						c.ForeColor = Colors.LightText;
					}
					foreach (var c in p.Controls.OfType<DarkNumericUpDown>())
					{
						c.BackColor = Colors.LightBackground;
						c.ForeColor = Colors.LightText;
					}
				}
			}
		}
		#endregion

		private void parts_SelectedItemChanged(object sender, EventArgs e)
		{
			locked = true;
			var part = (Part)this.parts.SelectedItem;
			partIDTextBox.Text = part.ID;
			partPosXTextBox.Text = part.Position.X.ToString();
			partPosYTextBox.Text = part.Position.Y.ToString();
			partFixTextBox.Text = part.Fix.ToString();
			locked = false;
		}

		private void partPosTextBox_ValueChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var part = (Part)this.parts.SelectedItem;
			part.Position = new Point((int)partPosXTextBox.Value, (int)partPosYTextBox.Value);
			((Viewer)this.ParentForm).DrawScene();
		}

		private void partFixTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
				e.Handled = true;
		}

		private void partFixTextBox_Change(object sender, EventArgs e)
		{
			if (locked) return;
			var part = (Part)this.parts.SelectedItem;
			var f = string.IsNullOrWhiteSpace(partFixTextBox.Text) ? "0" : partFixTextBox.Text;
			part.Fix = int.Parse(f);
		}

		private void cels_SelectedItemChanged(object sender, EventArgs e)
		{
			locked = true;
			var cel = (Cel)this.cels.SelectedItem;
			celFilenameTextBox.Text = cel.ImageFilename;
			celOffXTextBox.Value = cel.Offset.X;
			celOffYTextBox.Value = cel.Offset.Y;
			celVisibleCheckBox.Checked = cel.Visible;
			celGhostedCheckBox.Checked = cel.Ghost;
			celOpacityTrackBar.Value = cel.Opacity;

			celLabelText.Visible = celLabelFont.Visible = celLabelSize.Visible = celLabelBold.Visible = celLabelItalic.Visible = celLabelCenter.Visible = darkLabel5.Visible = darkLabel6.Visible = this.cels.SelectedItem is TextCel;
			celFilenameTextBox.Visible = darkLabel3.Visible = !(this.cels.SelectedItem is TextCel);
			if (celLabelText.Visible)
			{
				var textcel = (TextCel)this.cels.SelectedItem;
				celLabelText.Text = textcel.Text;
				celLabelFont.Text = textcel.Font.Name;
				celLabelSize.Value = (int)textcel.Font.Size;
				celLabelBold.Checked = textcel.Font.Style.HasFlag(FontStyle.Bold);
				celLabelItalic.Checked = textcel.Font.Style.HasFlag(FontStyle.Italic);
				celLabelCenter.Checked = textcel.Centered;
			}

			//celSectionPanel.Height = celLabelText.Visible ? 300  : 270;

			for (var i = 0; i < 10; i++)
				celMapCheckBoxes[i].Checked = cel.OnSets[i];
			locked = false;

			((Viewer)this.ParentForm).HilightedCel = cel;
			((Viewer)this.ParentForm).DrawScene();
		}

		private void celOffTextBox_ValueChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cel = (Cel)this.cels.SelectedItem;
			cel.Offset = new Point((int)celOffXTextBox.Value, (int)celOffYTextBox.Value);
			((Viewer)this.ParentForm).DrawScene();
		}

		private void celOpacityTrackBar_ValueChanged(object sender, EventArgs e)
		{
			celOpacityLabel.Text = string.Format("Opacity ({0}, {1}%)", celOpacityTrackBar.Value, (int)(celOpacityTrackBar.Value / 2.55f));
			if (locked) return;
			var cel = (Cel)this.cels.SelectedItem;
			cel.Opacity = celOpacityTrackBar.Value;
			((Viewer)this.ParentForm).DrawScene();
		}

		private void celVisibleCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cel = (Cel)this.cels.SelectedItem;
			cel.Visible = celVisibleCheckBox.Checked;
			((Viewer)this.ParentForm).DrawScene();
		}

		private void celMappedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cel = (Cel)this.cels.SelectedItem;
			for (var i = 0; i < 10; i++)
			{
				cel.OnSets[i] = celMapCheckBoxes[i].Checked;
			}
			((Viewer)this.ParentForm).DrawScene();
		}

		private void celGhostedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cel = (Cel)this.cels.SelectedItem;
			cel.Ghost = celGhostedCheckBox.Checked;
		}

		private void celLabelText_TextChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cel = (TextCel)this.cels.SelectedItem;
			cel.Text = celLabelText.Text;
			((Viewer)this.ParentForm).DrawScene();
		}

		private void celLabelFont_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cel = (TextCel)this.cels.SelectedItem;
			cel.Font = new Font(celLabelFont.Text, cel.Font.Size, cel.Font.Style);
			cel.Draw();
			((Viewer)this.ParentForm).DrawScene();
		}

		private void celLabelSize_ValueChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cel = (TextCel)this.cels.SelectedItem;
			cel.Font = new Font(cel.Font.Name, (int)celLabelSize.Value, cel.Font.Style);
			cel.Draw();
			((Viewer)this.ParentForm).DrawScene();
		}

		private void celLabelBold_CheckedChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cel = (TextCel)this.cels.SelectedItem;
			var newStyle = cel.Font.Style & ~FontStyle.Bold;
			if (celLabelBold.Checked) newStyle |= FontStyle.Bold;
			cel.Font = new Font(cel.Font.Name, cel.Font.Size, newStyle);
			cel.Draw();
			((Viewer)this.ParentForm).DrawScene();
		}

		private void celLabelItalic_CheckedChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cel = (TextCel)this.cels.SelectedItem;
			var newStyle = cel.Font.Style & ~FontStyle.Italic;
			if (celLabelItalic.Checked) newStyle |= FontStyle.Italic;
			cel.Font = new Font(cel.Font.Name, cel.Font.Size, newStyle);
			cel.Draw();
			((Viewer)this.ParentForm).DrawScene();
		}

		private void celLabelCenter_CheckedChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cel = (TextCel)this.cels.SelectedItem;
			cel.Centered = celLabelCenter.Checked;
			cel.Draw();
			((Viewer)this.ParentForm).DrawScene();
		}

	}
}
