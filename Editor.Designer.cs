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
			this.cels = new DarkUI.Controls.DarkComboBox();
			this.darkLabel3 = new DarkUI.Controls.DarkLabel();
			this.celFilenameTextBox = new DarkUI.Controls.DarkTextBox();
			this.darkLabel4 = new DarkUI.Controls.DarkLabel();
			this.celOffXTextBox = new DarkUI.Controls.DarkNumericUpDown();
			this.celOffYTextBox = new DarkUI.Controls.DarkNumericUpDown();
			this.celVisibleCheckBox = new DarkUI.Controls.DarkCheckBox();
			this.celGhostedCheckBox = new DarkUI.Controls.DarkCheckBox();
			this.celOpacityLabel = new DarkUI.Controls.DarkLabel();
			this.celOpacityTrackBar = new System.Windows.Forms.TrackBar();
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
			this.partSectionPanel = new DarkUI.Controls.DarkSectionPanel();
			this.celSectionPanel = new DarkUI.Controls.DarkSectionPanel();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.celOffXTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.celOffYTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.celOpacityTrackBar)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.partPosXTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.partPosYTextBox)).BeginInit();
			this.partSectionPanel.SuspendLayout();
			this.celSectionPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.cels);
			this.flowLayoutPanel2.Controls.Add(this.darkLabel3);
			this.flowLayoutPanel2.Controls.Add(this.celFilenameTextBox);
			this.flowLayoutPanel2.Controls.Add(this.darkLabel4);
			this.flowLayoutPanel2.Controls.Add(this.celOffXTextBox);
			this.flowLayoutPanel2.Controls.Add(this.celOffYTextBox);
			this.flowLayoutPanel2.Controls.Add(this.celVisibleCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.celGhostedCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.celOpacityLabel);
			this.flowLayoutPanel2.Controls.Add(this.celOpacityTrackBar);
			this.flowLayoutPanel2.Controls.Add(this.darkTitle2);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(1, 25);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(4);
			this.flowLayoutPanel2.Size = new System.Drawing.Size(318, 240);
			this.flowLayoutPanel2.TabIndex = 2;
			// 
			// cels
			// 
			this.cels.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.cels.Location = new System.Drawing.Point(7, 7);
			this.cels.Name = "cels";
			this.cels.Size = new System.Drawing.Size(303, 24);
			this.cels.TabIndex = 0;
			this.cels.SelectedIndexChanged += new System.EventHandler(this.cels_SelectedItemChanged);
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
			// celFilenameTextBox
			// 
			this.celFilenameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.celFilenameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.celFilenameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.celFilenameTextBox.Location = new System.Drawing.Point(140, 37);
			this.celFilenameTextBox.Name = "celFilenameTextBox";
			this.celFilenameTextBox.ReadOnly = true;
			this.celFilenameTextBox.Size = new System.Drawing.Size(170, 23);
			this.celFilenameTextBox.TabIndex = 14;
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
			// celOffXTextBox
			// 
			this.celOffXTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.celOffXTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.celOffXTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.celOffXTextBox.Location = new System.Drawing.Point(140, 66);
			this.celOffXTextBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.celOffXTextBox.Name = "celOffXTextBox";
			this.celOffXTextBox.Size = new System.Drawing.Size(82, 23);
			this.celOffXTextBox.TabIndex = 16;
			this.celOffXTextBox.ValueChanged += new System.EventHandler(this.celOffTextBox_ValueChanged);
			// 
			// celOffYTextBox
			// 
			this.celOffYTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
			this.celOffYTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.celOffYTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.celOffYTextBox.Location = new System.Drawing.Point(228, 66);
			this.celOffYTextBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.celOffYTextBox.Name = "celOffYTextBox";
			this.celOffYTextBox.Size = new System.Drawing.Size(82, 23);
			this.celOffYTextBox.TabIndex = 17;
			this.celOffYTextBox.ValueChanged += new System.EventHandler(this.celOffTextBox_ValueChanged);
			// 
			// celVisibleCheckBox
			// 
			this.celVisibleCheckBox.Location = new System.Drawing.Point(7, 95);
			this.celVisibleCheckBox.Name = "celVisibleCheckBox";
			this.celVisibleCheckBox.Size = new System.Drawing.Size(127, 23);
			this.celVisibleCheckBox.TabIndex = 18;
			this.celVisibleCheckBox.Text = "Visible";
			this.celVisibleCheckBox.CheckedChanged += new System.EventHandler(this.celVisibleCheckBox_CheckedChanged);
			// 
			// celGhostedCheckBox
			// 
			this.flowLayoutPanel2.SetFlowBreak(this.celGhostedCheckBox, true);
			this.celGhostedCheckBox.Location = new System.Drawing.Point(140, 95);
			this.celGhostedCheckBox.Name = "celGhostedCheckBox";
			this.celGhostedCheckBox.Size = new System.Drawing.Size(127, 23);
			this.celGhostedCheckBox.TabIndex = 22;
			this.celGhostedCheckBox.Text = "Ghost";
			this.celGhostedCheckBox.CheckedChanged += new System.EventHandler(this.celGhostedCheckBox_CheckedChanged);
			// 
			// celOpacityLabel
			// 
			this.celOpacityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.celOpacityLabel.Location = new System.Drawing.Point(7, 121);
			this.celOpacityLabel.Name = "celOpacityLabel";
			this.celOpacityLabel.Size = new System.Drawing.Size(127, 23);
			this.celOpacityLabel.TabIndex = 20;
			this.celOpacityLabel.Text = "Opacity";
			this.celOpacityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// celOpacityTrackBar
			// 
			this.celOpacityTrackBar.LargeChange = 16;
			this.celOpacityTrackBar.Location = new System.Drawing.Point(140, 124);
			this.celOpacityTrackBar.Maximum = 255;
			this.celOpacityTrackBar.Name = "celOpacityTrackBar";
			this.celOpacityTrackBar.Size = new System.Drawing.Size(170, 45);
			this.celOpacityTrackBar.TabIndex = 21;
			this.celOpacityTrackBar.TickFrequency = 16;
			this.celOpacityTrackBar.ValueChanged += new System.EventHandler(this.celOpacityTrackBar_ValueChanged);
			// 
			// darkTitle2
			// 
			this.darkTitle2.Location = new System.Drawing.Point(7, 172);
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
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 25);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(4);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(318, 130);
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
			this.partFixTextBox.TextChanged += new System.EventHandler(this.partFixTextBox_Change);
			this.partFixTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.partFixTextBox_KeyPress);
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
			this.partSectionPanel.Size = new System.Drawing.Size(320, 156);
			this.partSectionPanel.TabIndex = 1;
			// 
			// celSectionPanel
			// 
			this.celSectionPanel.AlwaysBlue = true;
			this.celSectionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.celSectionPanel.Controls.Add(this.flowLayoutPanel2);
			this.celSectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.celSectionPanel.Location = new System.Drawing.Point(0, 156);
			this.celSectionPanel.Name = "celSectionPanel";
			this.celSectionPanel.SectionHeader = "Cel properties";
			this.celSectionPanel.Size = new System.Drawing.Size(320, 266);
			this.celSectionPanel.TabIndex = 2;
			// 
			// Editor
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
			this.Controls.Add(this.celSectionPanel);
			this.Controls.Add(this.partSectionPanel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Editor";
			this.Size = new System.Drawing.Size(320, 553);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.celOffXTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.celOffYTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.celOpacityTrackBar)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.partPosXTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.partPosYTextBox)).EndInit();
			this.partSectionPanel.ResumeLayout(false);
			this.celSectionPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DarkUI.Controls.DarkComboBox parts;
		private DarkUI.Controls.DarkComboBox cels;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private DarkUI.Controls.DarkLabel darkLabel3;
		private DarkUI.Controls.DarkTextBox celFilenameTextBox;
		private DarkUI.Controls.DarkLabel darkLabel4;
		private DarkUI.Controls.DarkNumericUpDown celOffXTextBox;
		private DarkUI.Controls.DarkNumericUpDown celOffYTextBox;
		private DarkUI.Controls.DarkCheckBox celVisibleCheckBox;
		private DarkUI.Controls.DarkTitle darkTitle2;
		private DarkUI.Controls.DarkLabel celOpacityLabel;
		private System.Windows.Forms.TrackBar celOpacityTrackBar;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private DarkUI.Controls.DarkLabel darkTitle1;
		private DarkUI.Controls.DarkTextBox partIDTextBox;
		private DarkUI.Controls.DarkLabel darkLabel1;
		private DarkUI.Controls.DarkNumericUpDown partPosXTextBox;
		private DarkUI.Controls.DarkNumericUpDown partPosYTextBox;
		private DarkUI.Controls.DarkLabel darkLabel2;
		private DarkUI.Controls.DarkTextBox partFixTextBox;
		private DarkUI.Controls.DarkSectionPanel partSectionPanel;
		private DarkUI.Controls.DarkSectionPanel celSectionPanel;
		private DarkUI.Controls.DarkCheckBox celGhostedCheckBox;



	}
}