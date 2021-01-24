﻿namespace KiSSLab
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
			this.partCellsListView = new DarkUI.Controls.DarkListView();
			this.darkTitle4 = new DarkUI.Controls.DarkTitle();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.darkTitle1 = new DarkUI.Controls.DarkLabel();
			this.partIDTextBox = new DarkUI.Controls.DarkTextBox();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.partPosXTextBox = new DarkUI.Controls.DarkNumericUpDown();
			this.partPosYTextBox = new DarkUI.Controls.DarkNumericUpDown();
			this.darkLabel2 = new DarkUI.Controls.DarkLabel();
			this.partFixTextBox = new DarkUI.Controls.DarkTextBox();
			this.partVisibleCheckBox = new DarkUI.Controls.DarkCheckBox();
			this.darkTitle3 = new DarkUI.Controls.DarkTitle();
			this.panel1 = new System.Windows.Forms.Panel();
			this.parts = new DarkUI.Controls.DarkComboBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.cellPreviewPanel = new System.Windows.Forms.Panel();
			this.darkTitle5 = new DarkUI.Controls.DarkTitle();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.darkLabel3 = new DarkUI.Controls.DarkLabel();
			this.cellFilenameTextBox = new DarkUI.Controls.DarkTextBox();
			this.darkLabel4 = new DarkUI.Controls.DarkLabel();
			this.cellOffXTextBox = new DarkUI.Controls.DarkNumericUpDown();
			this.cellOffYTextBox = new DarkUI.Controls.DarkNumericUpDown();
			this.cellVisibleCheckBox = new DarkUI.Controls.DarkCheckBox();
			this.cellOpacityLabel = new DarkUI.Controls.DarkLabel();
			this.cellOpacityTrackBar = new System.Windows.Forms.TrackBar();
			this.darkTitle2 = new DarkUI.Controls.DarkTitle();
			this.darkTitle6 = new DarkUI.Controls.DarkTitle();
			this.panel2 = new System.Windows.Forms.Panel();
			this.cells = new DarkUI.Controls.DarkComboBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.events = new DarkUI.Controls.DarkTreeView();
			this.tabs.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.partPosXTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.partPosYTextBox)).BeginInit();
			this.panel1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cellOffXTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cellOffYTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cellOpacityTrackBar)).BeginInit();
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
			this.tabs.Size = new System.Drawing.Size(320, 553);
			this.tabs.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabPage1.Controls.Add(this.partCellsListView);
			this.tabPage1.Controls.Add(this.darkTitle4);
			this.tabPage1.Controls.Add(this.flowLayoutPanel1);
			this.tabPage1.Controls.Add(this.darkTitle3);
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage1.Size = new System.Drawing.Size(312, 524);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Parts";
			// 
			// partCellsListView
			// 
			this.partCellsListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.partCellsListView.Location = new System.Drawing.Point(4, 211);
			this.partCellsListView.Name = "partCellsListView";
			this.partCellsListView.ShowIcons = true;
			this.partCellsListView.Size = new System.Drawing.Size(304, 309);
			this.partCellsListView.TabIndex = 13;
			this.partCellsListView.Text = "darkListView1";
			this.partCellsListView.DoubleClick += new System.EventHandler(this.partCellsListView_DoubleClick);
			// 
			// darkTitle4
			// 
			this.darkTitle4.Dock = System.Windows.Forms.DockStyle.Top;
			this.darkTitle4.Location = new System.Drawing.Point(4, 188);
			this.darkTitle4.Name = "darkTitle4";
			this.darkTitle4.Size = new System.Drawing.Size(304, 23);
			this.darkTitle4.TabIndex = 12;
			this.darkTitle4.Text = "Component cells";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.darkTitle1);
			this.flowLayoutPanel1.Controls.Add(this.partIDTextBox);
			this.flowLayoutPanel1.Controls.Add(this.darkLabel1);
			this.flowLayoutPanel1.Controls.Add(this.partPosXTextBox);
			this.flowLayoutPanel1.Controls.Add(this.partPosYTextBox);
			this.flowLayoutPanel1.Controls.Add(this.darkLabel2);
			this.flowLayoutPanel1.Controls.Add(this.partFixTextBox);
			this.flowLayoutPanel1.Controls.Add(this.partVisibleCheckBox);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 57);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(304, 131);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// darkTitle1
			// 
			this.darkTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkTitle1.Location = new System.Drawing.Point(3, 0);
			this.darkTitle1.Name = "darkTitle1";
			this.darkTitle1.Size = new System.Drawing.Size(120, 23);
			this.darkTitle1.TabIndex = 0;
			this.darkTitle1.Text = "ID";
			this.darkTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// partIDTextBox
			// 
			this.partIDTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.partIDTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.partIDTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.partIDTextBox.Location = new System.Drawing.Point(129, 3);
			this.partIDTextBox.Name = "partIDTextBox";
			this.partIDTextBox.Size = new System.Drawing.Size(170, 23);
			this.partIDTextBox.TabIndex = 1;
			// 
			// darkLabel1
			// 
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(3, 29);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(120, 23);
			this.darkLabel1.TabIndex = 2;
			this.darkLabel1.Text = "Position";
			this.darkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// partPosXTextBox
			// 
			this.partPosXTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.partPosXTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.partPosXTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.partPosXTextBox.Location = new System.Drawing.Point(129, 32);
			this.partPosXTextBox.Name = "partPosXTextBox";
			this.partPosXTextBox.Size = new System.Drawing.Size(82, 23);
			this.partPosXTextBox.TabIndex = 3;
			this.partPosXTextBox.ValueChanged += new System.EventHandler(this.partPosTextBox_ValueChanged);
			// 
			// partPosYTextBox
			// 
			this.partPosYTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.partPosYTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.partPosYTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.partPosYTextBox.Location = new System.Drawing.Point(217, 32);
			this.partPosYTextBox.Name = "partPosYTextBox";
			this.partPosYTextBox.Size = new System.Drawing.Size(82, 23);
			this.partPosYTextBox.TabIndex = 4;
			this.partPosYTextBox.ValueChanged += new System.EventHandler(this.partPosTextBox_ValueChanged);
			// 
			// darkLabel2
			// 
			this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel2.Location = new System.Drawing.Point(3, 58);
			this.darkLabel2.Name = "darkLabel2";
			this.darkLabel2.Size = new System.Drawing.Size(120, 23);
			this.darkLabel2.TabIndex = 8;
			this.darkLabel2.Text = "Fix";
			this.darkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// partFixTextBox
			// 
			this.partFixTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.partFixTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowLayoutPanel1.SetFlowBreak(this.partFixTextBox, true);
			this.partFixTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.partFixTextBox.Location = new System.Drawing.Point(129, 61);
			this.partFixTextBox.Name = "partFixTextBox";
			this.partFixTextBox.Size = new System.Drawing.Size(82, 23);
			this.partFixTextBox.TabIndex = 9;
			this.partFixTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.partFixTextBox_KeyPress);
			// 
			// partVisibleCheckBox
			// 
			this.partVisibleCheckBox.AutoSize = true;
			this.partVisibleCheckBox.Location = new System.Drawing.Point(3, 90);
			this.partVisibleCheckBox.Name = "partVisibleCheckBox";
			this.partVisibleCheckBox.Size = new System.Drawing.Size(60, 19);
			this.partVisibleCheckBox.TabIndex = 6;
			this.partVisibleCheckBox.Text = "Visible";
			this.partVisibleCheckBox.CheckedChanged += new System.EventHandler(this.partVisibleCheckBox_CheckedChanged);
			// 
			// darkTitle3
			// 
			this.darkTitle3.Dock = System.Windows.Forms.DockStyle.Top;
			this.darkTitle3.Location = new System.Drawing.Point(4, 34);
			this.darkTitle3.Name = "darkTitle3";
			this.darkTitle3.Size = new System.Drawing.Size(304, 23);
			this.darkTitle3.TabIndex = 10;
			this.darkTitle3.Text = "Part properties";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.parts);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(304, 30);
			this.panel1.TabIndex = 0;
			// 
			// parts
			// 
			this.parts.Dock = System.Windows.Forms.DockStyle.Top;
			this.parts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.parts.Location = new System.Drawing.Point(0, 0);
			this.parts.Name = "parts";
			this.parts.Size = new System.Drawing.Size(304, 24);
			this.parts.TabIndex = 0;
			this.parts.SelectedIndexChanged += new System.EventHandler(this.parts_SelectedItemChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabPage2.Controls.Add(this.cellPreviewPanel);
			this.tabPage2.Controls.Add(this.darkTitle5);
			this.tabPage2.Controls.Add(this.flowLayoutPanel2);
			this.tabPage2.Controls.Add(this.darkTitle6);
			this.tabPage2.Controls.Add(this.panel2);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage2.Size = new System.Drawing.Size(312, 524);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Cells";
			// 
			// cellPreviewPanel
			// 
			this.cellPreviewPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.cellPreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cellPreviewPanel.Location = new System.Drawing.Point(4, 278);
			this.cellPreviewPanel.Name = "cellPreviewPanel";
			this.cellPreviewPanel.Size = new System.Drawing.Size(304, 242);
			this.cellPreviewPanel.TabIndex = 13;
			// 
			// darkTitle5
			// 
			this.darkTitle5.Dock = System.Windows.Forms.DockStyle.Top;
			this.darkTitle5.Location = new System.Drawing.Point(4, 255);
			this.darkTitle5.Name = "darkTitle5";
			this.darkTitle5.Size = new System.Drawing.Size(304, 23);
			this.darkTitle5.TabIndex = 12;
			this.darkTitle5.Text = "Cell image";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.darkLabel3);
			this.flowLayoutPanel2.Controls.Add(this.cellFilenameTextBox);
			this.flowLayoutPanel2.Controls.Add(this.darkLabel4);
			this.flowLayoutPanel2.Controls.Add(this.cellOffXTextBox);
			this.flowLayoutPanel2.Controls.Add(this.cellOffYTextBox);
			this.flowLayoutPanel2.Controls.Add(this.cellVisibleCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.cellOpacityLabel);
			this.flowLayoutPanel2.Controls.Add(this.cellOpacityTrackBar);
			this.flowLayoutPanel2.Controls.Add(this.darkTitle2);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(4, 57);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(304, 198);
			this.flowLayoutPanel2.TabIndex = 2;
			// 
			// darkLabel3
			// 
			this.darkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel3.Location = new System.Drawing.Point(3, 0);
			this.darkLabel3.Name = "darkLabel3";
			this.darkLabel3.Size = new System.Drawing.Size(120, 23);
			this.darkLabel3.TabIndex = 13;
			this.darkLabel3.Text = "Original filename";
			this.darkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cellFilenameTextBox
			// 
			this.cellFilenameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.cellFilenameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cellFilenameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.cellFilenameTextBox.Location = new System.Drawing.Point(129, 3);
			this.cellFilenameTextBox.Name = "cellFilenameTextBox";
			this.cellFilenameTextBox.ReadOnly = true;
			this.cellFilenameTextBox.Size = new System.Drawing.Size(170, 23);
			this.cellFilenameTextBox.TabIndex = 14;
			// 
			// darkLabel4
			// 
			this.darkLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel4.Location = new System.Drawing.Point(3, 29);
			this.darkLabel4.Name = "darkLabel4";
			this.darkLabel4.Size = new System.Drawing.Size(120, 23);
			this.darkLabel4.TabIndex = 15;
			this.darkLabel4.Text = "Offset";
			this.darkLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cellOffXTextBox
			// 
			this.cellOffXTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.cellOffXTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cellOffXTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.cellOffXTextBox.Location = new System.Drawing.Point(129, 32);
			this.cellOffXTextBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.cellOffXTextBox.Name = "cellOffXTextBox";
			this.cellOffXTextBox.Size = new System.Drawing.Size(82, 23);
			this.cellOffXTextBox.TabIndex = 16;
			this.cellOffXTextBox.ValueChanged += new System.EventHandler(this.cellOffTextBox_ValueChanged);
			// 
			// cellOffYTextBox
			// 
			this.cellOffYTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.cellOffYTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cellOffYTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.cellOffYTextBox.Location = new System.Drawing.Point(217, 32);
			this.cellOffYTextBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.cellOffYTextBox.Name = "cellOffYTextBox";
			this.cellOffYTextBox.Size = new System.Drawing.Size(82, 23);
			this.cellOffYTextBox.TabIndex = 17;
			this.cellOffYTextBox.ValueChanged += new System.EventHandler(this.cellOffTextBox_ValueChanged);
			// 
			// cellVisibleCheckBox
			// 
			this.cellVisibleCheckBox.AutoSize = true;
			this.flowLayoutPanel2.SetFlowBreak(this.cellVisibleCheckBox, true);
			this.cellVisibleCheckBox.Location = new System.Drawing.Point(3, 61);
			this.cellVisibleCheckBox.Name = "cellVisibleCheckBox";
			this.cellVisibleCheckBox.Size = new System.Drawing.Size(60, 19);
			this.cellVisibleCheckBox.TabIndex = 18;
			this.cellVisibleCheckBox.Text = "Visible";
			this.cellVisibleCheckBox.CheckedChanged += new System.EventHandler(this.cellVisibleCheckBox_CheckedChanged);
			// 
			// cellOpacityLabel
			// 
			this.cellOpacityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.cellOpacityLabel.Location = new System.Drawing.Point(3, 83);
			this.cellOpacityLabel.Name = "cellOpacityLabel";
			this.cellOpacityLabel.Size = new System.Drawing.Size(120, 23);
			this.cellOpacityLabel.TabIndex = 20;
			this.cellOpacityLabel.Text = "Opacity";
			this.cellOpacityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cellOpacityTrackBar
			// 
			this.cellOpacityTrackBar.LargeChange = 16;
			this.cellOpacityTrackBar.Location = new System.Drawing.Point(129, 86);
			this.cellOpacityTrackBar.Maximum = 255;
			this.cellOpacityTrackBar.Name = "cellOpacityTrackBar";
			this.cellOpacityTrackBar.Size = new System.Drawing.Size(170, 45);
			this.cellOpacityTrackBar.TabIndex = 21;
			this.cellOpacityTrackBar.TickFrequency = 16;
			this.cellOpacityTrackBar.ValueChanged += new System.EventHandler(this.cellOpacityTrackBar_ValueChanged);
			// 
			// darkTitle2
			// 
			this.darkTitle2.Location = new System.Drawing.Point(3, 134);
			this.darkTitle2.Name = "darkTitle2";
			this.darkTitle2.Size = new System.Drawing.Size(300, 15);
			this.darkTitle2.TabIndex = 19;
			this.darkTitle2.Text = "On sets";
			// 
			// darkTitle6
			// 
			this.darkTitle6.Dock = System.Windows.Forms.DockStyle.Top;
			this.darkTitle6.Location = new System.Drawing.Point(4, 34);
			this.darkTitle6.Name = "darkTitle6";
			this.darkTitle6.Size = new System.Drawing.Size(304, 23);
			this.darkTitle6.TabIndex = 14;
			this.darkTitle6.Text = "Cell properties";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.cells);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(4, 4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(304, 30);
			this.panel2.TabIndex = 1;
			// 
			// cells
			// 
			this.cells.Dock = System.Windows.Forms.DockStyle.Top;
			this.cells.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.cells.Location = new System.Drawing.Point(0, 0);
			this.cells.Name = "cells";
			this.cells.Size = new System.Drawing.Size(304, 24);
			this.cells.TabIndex = 0;
			this.cells.SelectedIndexChanged += new System.EventHandler(this.cells_SelectedItemChanged);
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabPage3.Controls.Add(this.events);
			this.tabPage3.Location = new System.Drawing.Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage3.Size = new System.Drawing.Size(312, 524);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Events";
			// 
			// events
			// 
			this.events.Dock = System.Windows.Forms.DockStyle.Fill;
			this.events.Location = new System.Drawing.Point(4, 4);
			this.events.MaxDragChange = 20;
			this.events.Name = "events";
			this.events.Size = new System.Drawing.Size(304, 516);
			this.events.TabIndex = 0;
			this.events.Text = "darkListView1";
			// 
			// Editor
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.Controls.Add(this.tabs);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Editor";
			this.Size = new System.Drawing.Size(320, 553);
			this.tabs.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.partPosXTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.partPosYTextBox)).EndInit();
			this.panel1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cellOffXTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cellOffYTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cellOpacityTrackBar)).EndInit();
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
		private DarkUI.Controls.DarkComboBox parts;
		private System.Windows.Forms.Panel panel2;
		private DarkUI.Controls.DarkComboBox cells;
		private DarkUI.Controls.DarkTreeView events;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private DarkUI.Controls.DarkLabel darkLabel3;
		private DarkUI.Controls.DarkTextBox cellFilenameTextBox;
		private DarkUI.Controls.DarkLabel darkLabel4;
		private DarkUI.Controls.DarkNumericUpDown cellOffXTextBox;
		private DarkUI.Controls.DarkNumericUpDown cellOffYTextBox;
		private DarkUI.Controls.DarkCheckBox cellVisibleCheckBox;
		private DarkUI.Controls.DarkTitle darkTitle2;
		private DarkUI.Controls.DarkLabel cellOpacityLabel;
		private System.Windows.Forms.TrackBar cellOpacityTrackBar;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private DarkUI.Controls.DarkLabel darkTitle1;
		private DarkUI.Controls.DarkTextBox partIDTextBox;
		private DarkUI.Controls.DarkLabel darkLabel1;
		private DarkUI.Controls.DarkNumericUpDown partPosXTextBox;
		private DarkUI.Controls.DarkNumericUpDown partPosYTextBox;
		private DarkUI.Controls.DarkLabel darkLabel2;
		private DarkUI.Controls.DarkTextBox partFixTextBox;
		private DarkUI.Controls.DarkCheckBox partVisibleCheckBox;
		private DarkUI.Controls.DarkTitle darkTitle3;
		private DarkUI.Controls.DarkListView partCellsListView;
		private DarkUI.Controls.DarkTitle darkTitle4;
		private DarkUI.Controls.DarkTitle darkTitle6;
		private System.Windows.Forms.Panel cellPreviewPanel;
		private DarkUI.Controls.DarkTitle darkTitle5;



	}
}