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
			objPosXTextBox.Maximum = scene.ScreenWidth;
			objPosYTextBox.Maximum = scene.ScreenHeight;

			this.objects.Items.Clear();
			this.objects.Items.AddRange(scene.Objects.ToArray());
			this.objects.SelectedIndex = 0;

			this.cells.Items.Clear();
			this.cells.Items.AddRange(scene.Cells.ToArray());
			this.cells.SelectedIndex = 0;

			for (var i = scene.Sets; i < 10; i++)
			{
				cellMapCheckBoxes[i].Enabled = cellMapCheckBoxes[i].Checked = false;
			}

			scene.Decode(this.events);
		}

		public void Pick(Object obj, Cell cell)
		{
			if (obj != null)
				objects.SelectedItem = obj;
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

		private void objects_SelectedItemChanged(object sender, EventArgs e)
		{
			locked = true;
			var obj = (Object)this.objects.SelectedItem;
			objIDTextBox.Text = obj.ID;
			objPosXTextBox.Text = obj.Position.X.ToString();
			objPosYTextBox.Text = obj.Position.Y.ToString();
			objFixTextBox.Text = obj.Fix.ToString();
			objVisibleCheckBox.Checked = obj.Visible;

			objCellsListView.Items.Clear();
			foreach (var componentCell in obj.Cells)
				objCellsListView.Items.Add(new DarkListItem()
				{
					Text = componentCell.ID,
					Tag = componentCell,
					//Icon = (Bitmap)componentCell.Image.GetThumbnailImage(16, 16, null, IntPtr.Zero),
				});
			objCellsListView.SelectItem(0);

			locked = false;
		}

		private void objPosXTextBox_ValueChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var obj = (Object)this.objects.SelectedItem;
			obj.Position = new Point((int)objPosXTextBox.Value, (int)objPosYTextBox.Value);
			((Viewer)this.ParentForm).DrawScene();
		}

		private void objFixTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (locked) return;
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
				e.Handled = true;
			var obj = (Object)this.objects.SelectedItem;
			var f = string.IsNullOrWhiteSpace(objFixTextBox.Text) ? "0" : objFixTextBox.Text;
			obj.Fix = int.Parse(f);
		}

		private void objVisibleCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (locked) return;
			var obj = (Object)this.objects.SelectedItem;
			obj.Visible = objVisibleCheckBox.Checked;
			((Viewer)this.ParentForm).DrawScene();
		}

		private void objCellsListView_DoubleClick(object sender, EventArgs e)
		{
			tabs.SelectedIndex = 1;
			cells.SelectedItem = objCellsListView.Items[objCellsListView.SelectedIndices[0]].Tag;
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

		private void cellOffXTextBox_ValueChanged(object sender, EventArgs e)
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
