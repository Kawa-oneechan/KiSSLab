namespace KiSSLab
{
	partial class Editor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabs = new KiSSLab.MyTab();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.obj = new System.Windows.Forms.PropertyGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			this.objects = new DarkUI.Controls.DarkComboBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.cell = new System.Windows.Forms.PropertyGrid();
			this.panel2 = new System.Windows.Forms.Panel();
			this.cells = new DarkUI.Controls.DarkComboBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.events = new DarkUI.Controls.DarkTreeView();
			this.tabs.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tabPage1);
			this.tabs.Controls.Add(this.tabPage2);
			this.tabs.Controls.Add(this.tabPage3);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(393, 553);
			this.tabs.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.obj);
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage1.Size = new System.Drawing.Size(385, 524);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Objects";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// obj
			// 
			this.obj.Dock = System.Windows.Forms.DockStyle.Fill;
			this.obj.Location = new System.Drawing.Point(4, 34);
			this.obj.Name = "obj";
			this.obj.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.obj.Size = new System.Drawing.Size(377, 486);
			this.obj.TabIndex = 1;
			this.obj.ToolbarVisible = false;
			this.obj.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.cel_PropertyValueChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.objects);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(377, 30);
			this.panel1.TabIndex = 0;
			// 
			// objects
			// 
			this.objects.Dock = System.Windows.Forms.DockStyle.Top;
			this.objects.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.objects.Location = new System.Drawing.Point(0, 0);
			this.objects.Name = "objects";
			this.objects.Size = new System.Drawing.Size(377, 24);
			this.objects.TabIndex = 0;
			this.objects.SelectedIndexChanged += new System.EventHandler(this.objects_SelectedItemChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.cell);
			this.tabPage2.Controls.Add(this.panel2);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage2.Size = new System.Drawing.Size(385, 524);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Cells";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// cell
			// 
			this.cell.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cell.Location = new System.Drawing.Point(4, 34);
			this.cell.Name = "cell";
			this.cell.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.cell.Size = new System.Drawing.Size(377, 486);
			this.cell.TabIndex = 2;
			this.cell.ToolbarVisible = false;
			this.cell.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.cel_PropertyValueChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.cells);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(4, 4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(377, 30);
			this.panel2.TabIndex = 1;
			// 
			// cells
			// 
			this.cells.Dock = System.Windows.Forms.DockStyle.Top;
			this.cells.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.cells.Location = new System.Drawing.Point(0, 0);
			this.cells.Name = "cells";
			this.cells.Size = new System.Drawing.Size(377, 24);
			this.cells.TabIndex = 0;
			this.cells.SelectedIndexChanged += new System.EventHandler(this.cells_SelectedItemChanged);
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.Color.Transparent;
			this.tabPage3.Controls.Add(this.events);
			this.tabPage3.Location = new System.Drawing.Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage3.Size = new System.Drawing.Size(385, 524);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Events";
			// 
			// events
			// 
			this.events.Dock = System.Windows.Forms.DockStyle.Fill;
			this.events.Location = new System.Drawing.Point(4, 4);
			this.events.Name = "events";
			this.events.Size = new System.Drawing.Size(377, 516);
			this.events.TabIndex = 0;
			this.events.Text = "darkListView1";
			// 
			// Editor
			// 
			this.Controls.Add(this.tabs);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Editor";
			this.Size = new System.Drawing.Size(393, 553);
			this.tabs.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private MyTab tabs;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabPage tabPage3;
		private DarkUI.Controls.DarkComboBox objects;
		private System.Windows.Forms.Panel panel2;
		private DarkUI.Controls.DarkComboBox cells;
		private System.Windows.Forms.PropertyGrid obj;
		private System.Windows.Forms.PropertyGrid cell;
		private DarkUI.Controls.DarkTreeView events;



	}
}