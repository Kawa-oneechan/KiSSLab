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
	public partial class Editor : Control
	{
		private Viewer viewer;
		private PropertyGrid cell, obj;
		private MyTab tabs;
		private DarkComboBox cells, objects;
		private DarkTreeView events;

		public void Construct(Viewer viewer)
		{
			this.cell = new PropertyGrid()
			{
				Dock = DockStyle.Fill,
				ToolbarVisible = false,
				PropertySort = PropertySort.Alphabetical,
			};
			this.obj = new PropertyGrid()
			{
				Dock = DockStyle.Fill,
				ToolbarVisible = false,
				PropertySort = PropertySort.Alphabetical,
			};
			this.cell.PropertyValueChanged += new PropertyValueChangedEventHandler(cel_PropertyValueChanged);
			this.obj.PropertyValueChanged += new PropertyValueChangedEventHandler(cel_PropertyValueChanged);

			this.viewer = viewer;

			tabs = new MyTab()
			{
				Dock = DockStyle.Fill,
			};

			tabs.TabPages.Add("Objects");
			var objPanel = new Panel()
			{
				Dock = DockStyle.Top,
				Height = 30,
			};
			this.objects = new DarkComboBox()
			{
				Dock = DockStyle.Top,
				DropDownStyle = ComboBoxStyle.DropDownList,
			};
			objPanel.Controls.Add(this.objects);
			this.objects.SelectedIndexChanged += (s, e) => { this.obj.SelectedObject = this.objects.SelectedItem; };
			tabs.TabPages[0].Controls.Add(this.obj);
			tabs.TabPages[0].Controls.Add(objPanel);
			tabs.TabPages[0].Padding = new Padding(4);
			tabs.TabPages[0].BackColor = SystemColors.Window;

			tabs.TabPages.Add("Cells");
			var celPanel = new Panel()
			{
				Dock = DockStyle.Top,
				Height = 30,
			};
			this.cells = new DarkComboBox()
			{
				Dock = DockStyle.Top,
				DropDownStyle = ComboBoxStyle.DropDownList,
			};
			celPanel.Controls.Add(this.cells);
			this.cells.SelectedIndexChanged += (s, e) => { this.cell.SelectedObject = this.cells.SelectedItem; };
			tabs.TabPages[1].Controls.Add(this.cell);
			tabs.TabPages[1].Controls.Add(celPanel);
			tabs.TabPages[1].Padding = new Padding(4);
			tabs.TabPages[1].BackColor = SystemColors.Window;

			tabs.TabPages.Add("Events");
			this.events = new DarkTreeView()
			{
				Dock = DockStyle.Fill,
				//BorderStyle = BorderStyle.None
			};
			tabs.TabPages[2].Controls.Add(this.events);
			tabs.TabPages[2].Padding = new Padding(4);
			tabs.TabPages[2].BackColor = SystemColors.Window;

			this.Controls.Add(tabs);
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

			Decode(scene);
		}

		public void Pick(Object obj, Cell cell)
		{
			if (obj != null)
				objects.SelectedItem = obj;
			if (cell != null)
			{
				cells.SelectedItem = cell;
				viewer.HilightedCell = cell;
			}
		}

		#region More darkmode lightswitch
		public void UpdateColors()
		{
			this.BackColor = Colors.GreyBackground;

			tabs.TabPages[0].BackColor = Colors.GreyBackground;
			tabs.TabPages[1].BackColor = Colors.GreyBackground;
			tabs.TabPages[2].BackColor = Colors.GreyBackground;

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
			this.viewer.DrawScene();
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
