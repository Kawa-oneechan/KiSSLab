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
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.cells = new DarkUI.Controls.DarkComboBox();
			this.darkLabel3 = new DarkUI.Controls.DarkLabel();
			this.cellFilenameTextBox = new DarkUI.Controls.DarkTextBox();
			this.darkLabel4 = new DarkUI.Controls.DarkLabel();
			this.cellOffXTextBox = new DarkUI.Controls.DarkNumericUpDown();
			this.cellOffYTextBox = new DarkUI.Controls.DarkNumericUpDown();
			this.cellVisibleCheckBox = new DarkUI.Controls.DarkCheckBox();
			this.cellOpacityLabel = new DarkUI.Controls.DarkLabel();
			this.cellOpacityTrackBar = new System.Windows.Forms.TrackBar();
			this.darkTitle2 = new DarkUI.Controls.DarkTitle();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.parts = new DarkUI.Controls.DarkComboBox();
			this.darkTitle1 = new DarkUI.Controls.DarkLabel();
			this.partIDTextBox = new DarkUI.Controls.DarkTextBox();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.partPosXTextBox = new DarkUI.Controls.DarkNumericUpDown();
			this.partPosYTextBox = new DarkUI.Controls.DarkNumericUpDown();
			this.darkLabel2 = new DarkUI.Controls.DarkLabel();
			this.partFixTextBox = new DarkUI.Controls.DarkTextBox();
			this.partVisibleCheckBox = new DarkUI.Controls.DarkCheckBox();
			this.partSectionPanel = new DarkUI.Controls.DarkSectionPanel();
			this.cellSectionPanel = new DarkUI.Controls.DarkSectionPanel();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cellOffXTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cellOffYTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cellOpacityTrackBar)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.partPosXTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.partPosYTextBox)).BeginInit();
			this.partSectionPanel.SuspendLayout();
			this.cellSectionPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.cells);
			this.flowLayoutPanel2.Controls.Add(this.darkLabel3);
			this.flowLayoutPanel2.Controls.Add(this.cellFilenameTextBox);
			this.flowLayoutPanel2.Controls.Add(this.darkLabel4);
			this.flowLayoutPanel2.Controls.Add(this.cellOffXTextBox);
			this.flowLayoutPanel2.Controls.Add(this.cellOffYTextBox);
			this.flowLayoutPanel2.Controls.Add(this.cellVisibleCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.cellOpacityLabel);
			this.flowLayoutPanel2.Controls.Add(this.cellOpacityTrackBar);
			this.flowLayoutPanel2.Controls.Add(this.darkTitle2);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(1, 25);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(4);
			this.flowLayoutPanel2.Size = new System.Drawing.Size(318, 235);
			this.flowLayoutPanel2.TabIndex = 2;
			// 
			// cells
			// 
			this.cells.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.cells.Location = new System.Drawing.Point(7, 7);
			this.cells.Name = "cells";
			this.cells.Size = new System.Drawing.Size(303, 24);
			this.cells.TabIndex = 0;
			this.cells.SelectedIndexChanged += new System.EventHandler(this.cells_SelectedItemChanged);
			// 
			// darkLabel3
			// 
			this.darkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel3.Location = new System.Drawing.Point(7, 34);
			this.darkLabel3.Name = "darkLabel3";
			this.darkLabel3.Size = new System.Drawing.Size(127, 23);
			this.darkLabel3.TabIndex = 13;
			this.darkLabel3.Text = "Original filename";
			this.darkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cellFilenameTextBox
			// 
			this.cellFilenameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.cellFilenameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cellFilenameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.cellFilenameTextBox.Location = new System.Drawing.Point(140, 37);
			this.cellFilenameTextBox.Name = "cellFilenameTextBox";
			this.cellFilenameTextBox.ReadOnly = true;
			this.cellFilenameTextBox.Size = new System.Drawing.Size(170, 23);
			this.cellFilenameTextBox.TabIndex = 14;
			// 
			// darkLabel4
			// 
			this.darkLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel4.Location = new System.Drawing.Point(7, 63);
			this.darkLabel4.Name = "darkLabel4";
			this.darkLabel4.Size = new System.Drawing.Size(127, 23);
			this.darkLabel4.TabIndex = 15;
			this.darkLabel4.Text = "Offset";
			this.darkLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cellOffXTextBox
			// 
			this.cellOffXTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.cellOffXTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cellOffXTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.cellOffXTextBox.Location = new System.Drawing.Point(140, 66);
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
			this.cellOffYTextBox.Location = new System.Drawing.Point(228, 66);
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
			this.cellVisibleCheckBox.Location = new System.Drawing.Point(7, 95);
			this.cellVisibleCheckBox.Name = "cellVisibleCheckBox";
			this.cellVisibleCheckBox.Size = new System.Drawing.Size(60, 19);
			this.cellVisibleCheckBox.TabIndex = 18;
			this.cellVisibleCheckBox.Text = "Visible";
			this.cellVisibleCheckBox.CheckedChanged += new System.EventHandler(this.cellVisibleCheckBox_CheckedChanged);
			// 
			// cellOpacityLabel
			// 
			this.cellOpacityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.cellOpacityLabel.Location = new System.Drawing.Point(7, 117);
			this.cellOpacityLabel.Name = "cellOpacityLabel";
			this.cellOpacityLabel.Size = new System.Drawing.Size(127, 23);
			this.cellOpacityLabel.TabIndex = 20;
			this.cellOpacityLabel.Text = "Opacity";
			this.cellOpacityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cellOpacityTrackBar
			// 
			this.cellOpacityTrackBar.LargeChange = 16;
			this.cellOpacityTrackBar.Location = new System.Drawing.Point(140, 120);
			this.cellOpacityTrackBar.Maximum = 255;
			this.cellOpacityTrackBar.Name = "cellOpacityTrackBar";
			this.cellOpacityTrackBar.Size = new System.Drawing.Size(170, 45);
			this.cellOpacityTrackBar.TabIndex = 21;
			this.cellOpacityTrackBar.TickFrequency = 16;
			this.cellOpacityTrackBar.ValueChanged += new System.EventHandler(this.cellOpacityTrackBar_ValueChanged);
			// 
			// darkTitle2
			// 
			this.darkTitle2.Location = new System.Drawing.Point(7, 168);
			this.darkTitle2.Name = "darkTitle2";
			this.darkTitle2.Size = new System.Drawing.Size(300, 15);
			this.darkTitle2.TabIndex = 19;
			this.darkTitle2.Text = "On sets";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.parts);
			this.flowLayoutPanel1.Controls.Add(this.darkTitle1);
			this.flowLayoutPanel1.Controls.Add(this.partIDTextBox);
			this.flowLayoutPanel1.Controls.Add(this.darkLabel1);
			this.flowLayoutPanel1.Controls.Add(this.partPosXTextBox);
			this.flowLayoutPanel1.Controls.Add(this.partPosYTextBox);
			this.flowLayoutPanel1.Controls.Add(this.darkLabel2);
			this.flowLayoutPanel1.Controls.Add(this.partFixTextBox);
			this.flowLayoutPanel1.Controls.Add(this.partVisibleCheckBox);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 25);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(4);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(318, 160);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// parts
			// 
			this.parts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.parts.Location = new System.Drawing.Point(7, 7);
			this.parts.Name = "parts";
			this.parts.Size = new System.Drawing.Size(303, 24);
			this.parts.TabIndex = 0;
			this.parts.SelectedIndexChanged += new System.EventHandler(this.parts_SelectedItemChanged);
			// 
			// darkTitle1
			// 
			this.darkTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkTitle1.Location = new System.Drawing.Point(7, 34);
			this.darkTitle1.Name = "darkTitle1";
			this.darkTitle1.Size = new System.Drawing.Size(127, 23);
			this.darkTitle1.TabIndex = 0;
			this.darkTitle1.Text = "ID";
			this.darkTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// partIDTextBox
			// 
			this.partIDTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.partIDTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.partIDTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.partIDTextBox.Location = new System.Drawing.Point(140, 37);
			this.partIDTextBox.Name = "partIDTextBox";
			this.partIDTextBox.Size = new System.Drawing.Size(170, 23);
			this.partIDTextBox.TabIndex = 1;
			// 
			// darkLabel1
			// 
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(7, 63);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(127, 23);
			this.darkLabel1.TabIndex = 2;
			this.darkLabel1.Text = "Position";
			this.darkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// partPosXTextBox
			// 
			this.partPosXTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.partPosXTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.partPosXTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.partPosXTextBox.Location = new System.Drawing.Point(140, 66);
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
			this.partPosYTextBox.Location = new System.Drawing.Point(228, 66);
			this.partPosYTextBox.Name = "partPosYTextBox";
			this.partPosYTextBox.Size = new System.Drawing.Size(82, 23);
			this.partPosYTextBox.TabIndex = 4;
			this.partPosYTextBox.ValueChanged += new System.EventHandler(this.partPosTextBox_ValueChanged);
			// 
			// darkLabel2
			// 
			this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel2.Location = new System.Drawing.Point(7, 92);
			this.darkLabel2.Name = "darkLabel2";
			this.darkLabel2.Size = new System.Drawing.Size(127, 23);
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
			this.partFixTextBox.Location = new System.Drawing.Point(140, 95);
			this.partFixTextBox.Name = "partFixTextBox";
			this.partFixTextBox.Size = new System.Drawing.Size(82, 23);
			this.partFixTextBox.TabIndex = 9;
			this.partFixTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.partFixTextBox_KeyPress);
			// 
			// partVisibleCheckBox
			// 
			this.partVisibleCheckBox.AutoSize = true;
			this.partVisibleCheckBox.Location = new System.Drawing.Point(7, 124);
			this.partVisibleCheckBox.Name = "partVisibleCheckBox";
			this.partVisibleCheckBox.Size = new System.Drawing.Size(60, 19);
			this.partVisibleCheckBox.TabIndex = 6;
			this.partVisibleCheckBox.Text = "Visible";
			this.partVisibleCheckBox.CheckedChanged += new System.EventHandler(this.partVisibleCheckBox_CheckedChanged);
			// 
			// partSectionPanel
			// 
			this.partSectionPanel.AlwaysBlue = true;
			this.partSectionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.partSectionPanel.Controls.Add(this.flowLayoutPanel1);
			this.partSectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.partSectionPanel.Location = new System.Drawing.Point(0, 0);
			this.partSectionPanel.Name = "partSectionPanel";
			this.partSectionPanel.SectionHeader = "Part properties";
			this.partSectionPanel.Size = new System.Drawing.Size(320, 186);
			this.partSectionPanel.TabIndex = 1;
			// 
			// cellSectionPanel
			// 
			this.cellSectionPanel.AlwaysBlue = true;
			this.cellSectionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.cellSectionPanel.Controls.Add(this.flowLayoutPanel2);
			this.cellSectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.cellSectionPanel.Location = new System.Drawing.Point(0, 186);
			this.cellSectionPanel.Name = "cellSectionPanel";
			this.cellSectionPanel.SectionHeader = "Cell properties";
			this.cellSectionPanel.Size = new System.Drawing.Size(320, 261);
			this.cellSectionPanel.TabIndex = 2;
			// 
			// Editor
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
			this.Controls.Add(this.cellSectionPanel);
			this.Controls.Add(this.partSectionPanel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Editor";
			this.Size = new System.Drawing.Size(320, 553);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cellOffXTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cellOffYTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cellOpacityTrackBar)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.partPosXTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.partPosYTextBox)).EndInit();
			this.partSectionPanel.ResumeLayout(false);
			this.cellSectionPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DarkUI.Controls.DarkComboBox parts;
		private DarkUI.Controls.DarkComboBox cells;
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
		private DarkUI.Controls.DarkSectionPanel partSectionPanel;
		private DarkUI.Controls.DarkSectionPanel cellSectionPanel;



	}
}