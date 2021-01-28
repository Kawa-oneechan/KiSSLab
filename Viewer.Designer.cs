namespace KiSSLab
{
	partial class Viewer
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

		private System.Windows.Forms.ToolStripButton openToolStripButton;

		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem copyCellToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetPositionToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reopenToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;

		private DarkUI.Controls.DarkMenuStrip menu;
		private DarkUI.Controls.DarkToolStrip tools;

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.menu = new DarkUI.Controls.DarkMenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reopenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tools = new DarkUI.Controls.DarkToolStrip();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.highlightToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.gridToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.nextSetToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.nextPalToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.editorToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.status = new DarkUI.Controls.DarkStatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.screenContainer = new System.Windows.Forms.Panel();
			this.screen = new System.Windows.Forms.PictureBox();
			this.cellContextMenu = new DarkUI.Controls.DarkContextMenu();
			this.cellMenuHeader = new System.Windows.Forms.ToolStripMenuItem();
			this.unfixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetPositionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.editor = new KiSSLab.Editor();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.menu.SuspendLayout();
			this.tools.SuspendLayout();
			this.status.SuspendLayout();
			this.screenContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
			this.cellContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// menu
			// 
			this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.menu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
			this.menu.Size = new System.Drawing.Size(860, 24);
			this.menu.TabIndex = 0;
			this.menu.Text = "darkMenuStrip2";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.reopenToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.openToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.openToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Open;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.Open_Click);
			// 
			// reopenToolStripMenuItem
			// 
			this.reopenToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.reopenToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.reopenToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Reset;
			this.reopenToolStripMenuItem.Name = "reopenToolStripMenuItem";
			this.reopenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.reopenToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.reopenToolStripMenuItem.Text = "&Reopen";
			this.reopenToolStripMenuItem.Click += new System.EventHandler(this.ReOpen_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.exitToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Exit;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCellToolStripMenuItem,
			this.unfixToolStripMenuItem,
            this.refixToolStripMenuItem,
            this.resetPositionToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// copyCellToolStripMenuItem
			// 
			this.copyCellToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.copyCellToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.copyCellToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Copy;
			this.copyCellToolStripMenuItem.Name = "copyCellToolStripMenuItem";
			this.copyCellToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyCellToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.copyCellToolStripMenuItem.Text = "&Copy cell";
			this.copyCellToolStripMenuItem.Click += new System.EventHandler(this.CopyCell_Click);
			// 
			// resetPositionToolStripMenuItem
			// 
			this.resetPositionToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.resetPositionToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.resetPositionToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Reset;
			this.resetPositionToolStripMenuItem.Name = "resetPositionToolStripMenuItem";
			this.resetPositionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
						| System.Windows.Forms.Keys.R)));
			this.resetPositionToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.resetPositionToolStripMenuItem.Text = "&Reset position";
			this.resetPositionToolStripMenuItem.Click += new System.EventHandler(this.Reset_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.About_Click);
			// 
			// tools
			// 
			this.tools.AutoSize = false;
			this.tools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.toolStripSeparator2,
            this.highlightToolStripButton,
            this.gridToolStripButton,
            this.toolStripSeparator3,
            this.nextSetToolStripButton,
            this.toolStripSeparator4,
            this.nextPalToolStripButton,
            this.toolStripSeparator5,
            this.toolStripSeparator6,
            this.editorToolStripButton});
			this.tools.Location = new System.Drawing.Point(0, 24);
			this.tools.Name = "tools";
			this.tools.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
			this.tools.Size = new System.Drawing.Size(860, 26);
			this.tools.TabIndex = 1;
			this.tools.Text = "darkToolStrip1";
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.openToolStripButton.Image = global::KiSSLab.Properties.Resources.Open;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.openToolStripButton.Text = "Open";
			this.openToolStripButton.Click += new System.EventHandler(this.Open_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
			// 
			// highlightToolStripButton
			// 
			this.highlightToolStripButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.highlightToolStripButton.CheckOnClick = true;
			this.highlightToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.highlightToolStripButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.highlightToolStripButton.Image = global::KiSSLab.Properties.Resources.Highlight;
			this.highlightToolStripButton.Name = "highlightToolStripButton";
			this.highlightToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.highlightToolStripButton.Text = "Highlight current cell";
			this.highlightToolStripButton.Click += new System.EventHandler(this.Highlight_Click);
			// 
			// gridToolStripButton
			// 
			this.gridToolStripButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.gridToolStripButton.CheckOnClick = true;
			this.gridToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.gridToolStripButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.gridToolStripButton.Image = global::KiSSLab.Properties.Resources.Grid;
			this.gridToolStripButton.Name = "gridToolStripButton";
			this.gridToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.gridToolStripButton.Text = "Snap to Grid";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
			// 
			// nextSetToolStripButton
			// 
			this.nextSetToolStripButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.nextSetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.nextSetToolStripButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.nextSetToolStripButton.Image = global::KiSSLab.Properties.Resources.CycleSets;
			this.nextSetToolStripButton.Name = "nextSetToolStripButton";
			this.nextSetToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.nextSetToolStripButton.Text = "Next set";
			this.nextSetToolStripButton.Click += new System.EventHandler(this.NextSet_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.toolStripSeparator4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
			// 
			// nextPalToolStripButton
			// 
			this.nextPalToolStripButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.nextPalToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.nextPalToolStripButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.nextPalToolStripButton.Image = global::KiSSLab.Properties.Resources.Colors;
			this.nextPalToolStripButton.Name = "nextPalToolStripButton";
			this.nextPalToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.nextPalToolStripButton.Text = "Next palette";
			this.nextPalToolStripButton.Click += new System.EventHandler(this.NextPal_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.toolStripSeparator5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.toolStripSeparator5.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.toolStripSeparator6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.toolStripSeparator6.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 26);
			// 
			// editorToolStripButton
			// 
			this.editorToolStripButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.editorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.editorToolStripButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.editorToolStripButton.Image = global::KiSSLab.Properties.Resources.Properties;
			this.editorToolStripButton.Name = "editorToolStripButton";
			this.editorToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.editorToolStripButton.Text = "Show editor";
			this.editorToolStripButton.Click += new System.EventHandler(this.ShowEditor_Click);
			// 
			// status
			// 
			this.status.AutoSize = false;
			this.status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
			this.status.Location = new System.Drawing.Point(0, 525);
			this.status.Name = "status";
			this.status.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
			this.status.Size = new System.Drawing.Size(860, 35);
			this.status.SizingGrip = false;
			this.status.TabIndex = 2;
			this.status.Text = "darkStatusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.AutoSize = false;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(240, 22);
			this.toolStripStatusLabel1.Text = " ";
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.AutoSize = false;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(80, 22);
			this.toolStripStatusLabel2.Text = " ";
			this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.AutoSize = false;
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(32, 22);
			this.toolStripStatusLabel3.Text = " ";
			this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.AutoSize = false;
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(40, 22);
			this.toolStripStatusLabel4.Text = " ";
			this.toolStripStatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// screenContainer
			// 
			this.screenContainer.AutoScroll = true;
			this.screenContainer.Controls.Add(this.screen);
			this.screenContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.screenContainer.Location = new System.Drawing.Point(0, 50);
			this.screenContainer.Name = "screenContainer";
			this.screenContainer.Size = new System.Drawing.Size(540, 475);
			this.screenContainer.TabIndex = 4;
			// 
			// screen
			// 
			this.screen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.screen.Location = new System.Drawing.Point(23, 22);
			this.screen.Name = "screen";
			this.screen.Size = new System.Drawing.Size(167, 121);
			this.screen.TabIndex = 0;
			this.screen.TabStop = false;
			this.screen.Paint += new System.Windows.Forms.PaintEventHandler(this.Screen_Paint);
			this.screen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseClick);
			this.screen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseDown);
			this.screen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseMove);
			this.screen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseUp);
			// 
			// cellContextMenu
			// 
			this.cellContextMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.cellContextMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.cellContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cellMenuHeader,
            this.toolStripSeparator7});
			this.cellContextMenu.Name = "cellContextMenu";
			this.cellContextMenu.Size = new System.Drawing.Size(169, 99);
			// 
			// cellMenuHeader
			// 
			this.cellMenuHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.cellMenuHeader.Enabled = false;
			this.cellMenuHeader.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
			this.cellMenuHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.cellMenuHeader.Name = "cellMenuHeader";
			this.cellMenuHeader.Size = new System.Drawing.Size(168, 22);
			this.cellMenuHeader.Text = "toolStripMenuItem1";
			// 
			// unfixToolStripMenuItem
			// 
			this.unfixToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.unfixToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.unfixToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Unlock;
			this.unfixToolStripMenuItem.Name = "unfixToolStripMenuItem";
			this.unfixToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.unfixToolStripMenuItem.Text = "Unfix";
			this.unfixToolStripMenuItem.Click += new System.EventHandler(this.Unfix_Click);
			// 
			// refixToolStripMenuItem
			// 
			this.refixToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.refixToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.refixToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Lock;
			this.refixToolStripMenuItem.Name = "refixToolStripMenuItem";
			this.refixToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.refixToolStripMenuItem.Text = "Refix";
			this.refixToolStripMenuItem.Click += new System.EventHandler(this.Refix_Click);
			// 
			// resetPositionToolStripMenuItem1
			// 
			this.resetPositionToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.resetPositionToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.resetPositionToolStripMenuItem1.Image = global::KiSSLab.Properties.Resources.Reset;
			this.resetPositionToolStripMenuItem1.Name = "resetPositionToolStripMenuItem1";
			this.resetPositionToolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
			this.resetPositionToolStripMenuItem1.Text = "Reset position";
			this.resetPositionToolStripMenuItem1.Click += new System.EventHandler(this.Reset_Click);
			// 
			// editor
			// 
			this.editor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
			this.editor.Dock = System.Windows.Forms.DockStyle.Right;
			this.editor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.editor.Location = new System.Drawing.Point(540, 50);
			this.editor.Name = "editor";
			this.editor.Size = new System.Drawing.Size(320, 475);
			this.editor.TabIndex = 3;
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.toolStripSeparator7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.toolStripSeparator7.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(165, 6);
			// 
			// Viewer
			// 
			this.ClientSize = new System.Drawing.Size(860, 560);
			this.Controls.Add(this.screenContainer);
			this.Controls.Add(this.editor);
			this.Controls.Add(this.tools);
			this.Controls.Add(this.menu);
			this.Controls.Add(this.status);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menu;
			this.MinimumSize = new System.Drawing.Size(0, 570);
			this.Name = "Viewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Viewer_FormClosing);
			this.ResizeEnd += new System.EventHandler(this.Viewer_ResizeEnd);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Viewer_KeyUp);
			this.Move += new System.EventHandler(this.Viewer_Move);
			this.Resize += new System.EventHandler(this.Viewer_Resize);
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			this.status.ResumeLayout(false);
			this.status.PerformLayout();
			this.screenContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
			this.cellContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton highlightToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton nextSetToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton nextPalToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton editorToolStripButton;
		private DarkUI.Controls.DarkStatusStrip status;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
		private Editor editor;
		private System.Windows.Forms.Panel screenContainer;
		private System.Windows.Forms.PictureBox screen;
		private System.Windows.Forms.ToolStripButton gridToolStripButton;
		private DarkUI.Controls.DarkContextMenu cellContextMenu;
		private System.Windows.Forms.ToolStripMenuItem unfixToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refixToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetPositionToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem cellMenuHeader;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
	}
}