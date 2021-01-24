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
		private DarkCheckBox[] cellMapCheckBoxes;

		public Editor()
		{
			InitializeComponent();

			tabs.SelectedIndex = 3;

			cellMapCheckBoxes = new DarkCheckBox[10];
			for (var i = 0; i < 10; i++)
			{
				var setCheck = cellMapCheckBoxes[i] = new DarkCheckBox()
				{
					Text = i.ToString(),
					AutoSize = true,
				};
				setCheck.CheckedChanged += new EventHandler(cellMappedCheckBox_CheckedChanged);
				flowLayoutPanel2.Controls.Add(setCheck);
			}
		}

		public void SetScene(Scene scene)
		{
			partPosXTextBox.Maximum = scene.ScreenWidth;
			partPosYTextBox.Maximum = scene.ScreenHeight;

			this.parts.Items.Clear();
			this.parts.Items.AddRange(scene.Parts.ToArray());
			this.parts.SelectedIndex = 0;

			this.cells.Items.Clear();
			this.cells.Items.AddRange(scene.Cells.ToArray());
			this.cells.SelectedIndex = 0;

			for (var i = scene.Sets; i < 10; i++)
			{
				cellMapCheckBoxes[i].Enabled = cellMapCheckBoxes[i].Checked = false;
			}

			scene.Decode(this.events);
		}

		public void Pick(Part part, Cell cell)
		{
			if (part != null)
				parts.SelectedItem = part;
			if (cell != null)
			{
				cells.SelectedItem = cell;
				((Viewer)this.ParentForm).HilightedCell = cell;
			}
		}

		#region More darkmode lightswitch
		public void UpdateColors()
		{
			this.BackColor = Colors.GreyBackground;

			foreach (var t in tabs.TabPages.OfType<TabPage>())
			{
				t.BackColor = Colors.GreyBackground;

				foreach (var p in t.Controls.OfType<FlowLayoutPanel>())
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
			partVisibleCheckBox.Checked = part.Visible;

			partCellsListView.Items.Clear();
			foreach (var componentCell in part.Cells)
				partCellsListView.Items.Add(new DarkListItem()
				{
					Text = componentCell.ID,
					Tag = componentCell,
					//Icon = (Bitmap)componentCell.Image.GetThumbnailImage(16, 16, null, IntPtr.Zero),
				});
			partCellsListView.SelectItem(0);

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
			if (locked) return;
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
				e.Handled = true;
			var part = (Part)this.parts.SelectedItem;
			var f = string.IsNullOrWhiteSpace(partFixTextBox.Text) ? "0" : partFixTextBox.Text;
			part.Fix = int.Parse(f);
		}

		private void partVisibleCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var part = (Part)this.parts.SelectedItem;
			part.Visible = partVisibleCheckBox.Checked;
			((Viewer)this.ParentForm).DrawScene();
		}

		private void partCellsListView_DoubleClick(object sender, EventArgs e)
		{
			tabs.SelectedIndex = 1;
			cells.SelectedItem = partCellsListView.Items[partCellsListView.SelectedIndices[0]].Tag;
		}

		private void cells_SelectedItemChanged(object sender, EventArgs e)
		{
			locked = true;
			var cell = (Cell)this.cells.SelectedItem;
			cellFilenameTextBox.Text = cell.ImageFilename;
			cellOffXTextBox.Value = cell.Offset.X;
			cellOffYTextBox.Value = cell.Offset.Y;
			cellVisibleCheckBox.Checked = cell.Visible;
			cellOpacityTrackBar.Value = cell.Opacity;
			for (var i = 0; i < 10; i++)
				cellMapCheckBoxes[i].Checked = cell.OnSets[i];
			cellPreviewPanel.BackgroundImage = cell.Image;
			locked = false;
		}

		private void cellOffTextBox_ValueChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cell = (Cell)this.cells.SelectedItem;
			cell.Offset = new Point((int)cellOffXTextBox.Value, (int)cellOffYTextBox.Value);
			((Viewer)this.ParentForm).DrawScene();
		}

		private void cellOpacityTrackBar_ValueChanged(object sender, EventArgs e)
		{
			cellOpacityLabel.Text = string.Format("Opacity ({0}, {1}%)", cellOpacityTrackBar.Value, (int)(cellOpacityTrackBar.Value / 2.55f));
			if (locked) return;
			var cell = (Cell)this.cells.SelectedItem;
			cell.Opacity = cellOpacityTrackBar.Value;
			((Viewer)this.ParentForm).DrawScene();
		}

		private void cellVisibleCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cell = (Cell)this.cells.SelectedItem;
			cell.Visible = cellVisibleCheckBox.Checked;
			((Viewer)this.ParentForm).DrawScene();
		}

		private void cellMappedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var cell = (Cell)this.cells.SelectedItem;
			for (var i = 0; i < 10; i++)
			{
				cell.OnSets[i] = cellMapCheckBoxes[i].Checked;
			}
			((Viewer)this.ParentForm).DrawScene();
		}
	}
}
