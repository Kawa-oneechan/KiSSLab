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
		public Editor()
		{
			InitializeComponent();

			tabs.SelectedIndex = 3;
		}

		public void SetScene(Scene scene)
		{
			this.objects.Items.Clear();
			this.objects.Items.AddRange(scene.Objects.ToArray());
			this.objects.SelectedIndex = 0;

			this.cells.Items.Clear();
			this.cells.Items.AddRange(scene.Cells.ToArray());
			this.cells.SelectedIndex = 0;

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
				t.BackColor = Colors.GreyBackground;

			this.cell.BackColor = this.obj.BackColor = Colors.GreyBackground;
			this.cell.ViewBackColor = this.obj.ViewBackColor = Colors.LightBackground;
			this.cell.ViewForeColor = this.obj.ViewForeColor = Colors.LightText;
			this.cell.LineColor = this.obj.LineColor = Colors.DarkBorder;
			this.cell.HelpBackColor = this.obj.HelpBackColor = Colors.DarkBackground;
			this.cell.HelpForeColor = this.obj.HelpForeColor = Colors.LightText;
		}
		#endregion

		void cel_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			((Viewer)this.ParentForm).DrawScene();
		}

		private void objects_SelectedItemChanged(object sender, EventArgs e)
		{
			this.obj.SelectedObject = this.objects.SelectedItem;
		}

		private void cells_SelectedItemChanged(object sender, EventArgs e)
		{
			this.cell.SelectedObject = this.cells.SelectedItem;
		}
	}

	public class AlphaPropertyControl : UserControl
	{
		public int Value { get; set; }
		public IWindowsFormsEditorService EdSvc { get; set; }
		private Label precise;
		private TrackBar track;

		public AlphaPropertyControl(int value)
		{
			Height = 24;
			Value = value;

			precise = new Label()
			{
				Text = value.ToString(),
				Width = 48,
				TextAlign = ContentAlignment.MiddleCenter,
				Dock = DockStyle.Right,
			};
			track = new TrackBar()
			{
				Minimum = 0,
				Maximum = 255,
				Value = value,
				TickFrequency = 25,
				Dock = DockStyle.Fill,
			};

			track.ValueChanged += new EventHandler(track_ValueChanged);

			Controls.Add(track);
			Controls.Add(precise);
		}

		void track_ValueChanged(object sender, EventArgs e)
		{
			Value = track.Value;
			precise.Text = Value.ToString();
		}
	}

	public class AlphaEditor : UITypeEditor
	{
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && context.Instance != null && provider != null)
			{
				var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (edSvc != null)
				{
					var editor = new AlphaPropertyControl((int)Convert.ChangeType(value, context.PropertyDescriptor.PropertyType));
					editor.EdSvc = edSvc;
					edSvc.DropDownControl(editor);
					return editor.Value;
				}
			}
			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}
	}
}
