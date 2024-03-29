﻿namespace KiSSLab
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenuStrip = new DarkUI.Controls.DarkMenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openExpansionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openInNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reopenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyCelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.unfixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showHighlightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.alignToGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.xRayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainToolStrip = new DarkUI.Controls.DarkToolStrip();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.highlightToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.gridToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.propertiesToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.nextSetToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.nextPalToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.zoomToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripZoomBar = new KiSSLab.ToolStripZoomBarItem();
			this.mainStatusStrip = new DarkUI.Controls.DarkStatusStrip();
			this.currentToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.positionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.lockToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.fixToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.screenContainerPanel = new KiSSLab.MyPanel();
			this.viewersTabControl = new KiSSLab.MyTab();
			this.celContextMenu = new DarkUI.Controls.DarkContextMenu();
			this.celMenuHeader = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.resetPositionContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyCelContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.unfixContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refixContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editor = new KiSSLab.Editor();
			this.mainMenuStrip.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.mainStatusStrip.SuspendLayout();
			this.celContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
			this.mainMenuStrip.Size = new System.Drawing.Size(860, 24);
			this.mainMenuStrip.TabIndex = 0;
			this.mainMenuStrip.Text = "darkMenuStrip2";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openExpansionToolStripMenuItem,
            this.openInNewToolStripMenuItem,
            this.reopenToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Open_Dark;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.openToolStripMenuItem.Text = "&Open…";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.Open_Click);
			// 
			// openExpansionToolStripMenuItem
			// 
			this.openExpansionToolStripMenuItem.Name = "openExpansionToolStripMenuItem";
			this.openExpansionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.openExpansionToolStripMenuItem.Text = "Open Expansion…";
			this.openExpansionToolStripMenuItem.Click += new System.EventHandler(this.Open_Click);
			// 
			// openInNewToolStripMenuItem
			// 
			this.openInNewToolStripMenuItem.Name = "openInNewToolStripMenuItem";
			this.openInNewToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.openInNewToolStripMenuItem.Text = "Open in &New…";
			this.openInNewToolStripMenuItem.Click += new System.EventHandler(this.Open_Click);
			// 
			// reopenToolStripMenuItem
			// 
			this.reopenToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Reset_Dark;
			this.reopenToolStripMenuItem.Name = "reopenToolStripMenuItem";
			this.reopenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.reopenToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.reopenToolStripMenuItem.Text = "&Reopen";
			this.reopenToolStripMenuItem.Click += new System.EventHandler(this.ReOpen_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Exit_Dark;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetPositionToolStripMenuItem,
            this.copyCelToolStripMenuItem,
            this.unfixToolStripMenuItem,
            this.refixToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// resetPositionToolStripMenuItem
			// 
			this.resetPositionToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Undo_Dark;
			this.resetPositionToolStripMenuItem.Name = "resetPositionToolStripMenuItem";
			this.resetPositionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.resetPositionToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.resetPositionToolStripMenuItem.Text = "&Reset Position";
			this.resetPositionToolStripMenuItem.Click += new System.EventHandler(this.Reset_Click);
			// 
			// copyCelToolStripMenuItem
			// 
			this.copyCelToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Copy_Dark;
			this.copyCelToolStripMenuItem.Name = "copyCelToolStripMenuItem";
			this.copyCelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyCelToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.copyCelToolStripMenuItem.Text = "&Copy cel";
			this.copyCelToolStripMenuItem.Click += new System.EventHandler(this.CopyCel_Click);
			// 
			// unfixToolStripMenuItem
			// 
			this.unfixToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Unlock_Dark;
			this.unfixToolStripMenuItem.Name = "unfixToolStripMenuItem";
			this.unfixToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.unfixToolStripMenuItem.Text = "Unfix";
			this.unfixToolStripMenuItem.Click += new System.EventHandler(this.Unfix_Click);
			// 
			// refixToolStripMenuItem
			// 
			this.refixToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Lock_Dark;
			this.refixToolStripMenuItem.Name = "refixToolStripMenuItem";
			this.refixToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.refixToolStripMenuItem.Text = "Refix";
			this.refixToolStripMenuItem.Click += new System.EventHandler(this.Refix_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHighlightToolStripMenuItem,
            this.alignToGridToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.xRayToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// showHighlightToolStripMenuItem
			// 
			this.showHighlightToolStripMenuItem.CheckOnClick = true;
			this.showHighlightToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Highlight_Dark;
			this.showHighlightToolStripMenuItem.Name = "showHighlightToolStripMenuItem";
			this.showHighlightToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.showHighlightToolStripMenuItem.Text = "Highlight Current Cel";
			this.showHighlightToolStripMenuItem.Click += new System.EventHandler(this.Highlight_Click);
			// 
			// alignToGridToolStripMenuItem
			// 
			this.alignToGridToolStripMenuItem.CheckOnClick = true;
			this.alignToGridToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Grid_Dark;
			this.alignToGridToolStripMenuItem.Name = "alignToGridToolStripMenuItem";
			this.alignToGridToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.alignToGridToolStripMenuItem.Text = "Align to Grid";
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.CheckOnClick = true;
			this.propertiesToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Properties_Dark;
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.propertiesToolStripMenuItem.Text = "Properties";
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.ShowEditor_Click);
			// 
			// xRayToolStripMenuItem
			// 
			this.xRayToolStripMenuItem.CheckOnClick = true; 
			this.xRayToolStripMenuItem.Image = global::KiSSLab.Properties.Resources.Xray_Dark;
			this.xRayToolStripMenuItem.Name = "xRayToolStripMenuItem";
			this.xRayToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.xRayToolStripMenuItem.Text = "X-Ray";
			this.xRayToolStripMenuItem.Click += new System.EventHandler(this.xRayToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.About_Click);
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.AutoSize = false;
			this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.toolStripSeparator2,
            this.highlightToolStripButton,
            this.gridToolStripButton,
            this.propertiesToolStripButton,
            this.toolStripSeparator3,
            this.nextSetToolStripButton,
            this.toolStripSeparator4,
            this.nextPalToolStripButton,
            this.toolStripSeparator5,
            this.zoomToolStripLabel,
            this.toolStripZoomBar});
			this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
			this.mainToolStrip.Size = new System.Drawing.Size(860, 26);
			this.mainToolStrip.TabIndex = 1;
			this.mainToolStrip.Text = "darkToolStrip1";
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = global::KiSSLab.Properties.Resources.Open_Dark;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.openToolStripButton.Text = "Open";
			this.openToolStripButton.Click += new System.EventHandler(this.Open_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
			// 
			// highlightToolStripButton
			// 
			this.highlightToolStripButton.CheckOnClick = true;
			this.highlightToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.highlightToolStripButton.Image = global::KiSSLab.Properties.Resources.Highlight_Dark;
			this.highlightToolStripButton.Name = "highlightToolStripButton";
			this.highlightToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.highlightToolStripButton.Text = "Highlight current cel";
			this.highlightToolStripButton.Click += new System.EventHandler(this.Highlight_Click);
			// 
			// gridToolStripButton
			// 
			this.gridToolStripButton.CheckOnClick = true;
			this.gridToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.gridToolStripButton.Image = global::KiSSLab.Properties.Resources.Grid_Dark;
			this.gridToolStripButton.Name = "gridToolStripButton";
			this.gridToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.gridToolStripButton.Text = "Snap to Grid";
			this.gridToolStripButton.CheckedChanged += new System.EventHandler(this.gridToolStripButton_CheckedChanged);
			// 
			// propertiesToolStripButton
			// 
			this.propertiesToolStripButton.CheckOnClick = true;
			this.propertiesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.propertiesToolStripButton.Image = global::KiSSLab.Properties.Resources.Properties_Dark;
			this.propertiesToolStripButton.Name = "propertiesToolStripButton";
			this.propertiesToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.propertiesToolStripButton.Text = "Show editor";
			this.propertiesToolStripButton.Click += new System.EventHandler(this.ShowEditor_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
			// 
			// nextSetToolStripButton
			// 
			this.nextSetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.nextSetToolStripButton.Image = global::KiSSLab.Properties.Resources.CycleSets_Dark;
			this.nextSetToolStripButton.Name = "nextSetToolStripButton";
			this.nextSetToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.nextSetToolStripButton.Text = "Next set";
			this.nextSetToolStripButton.Click += new System.EventHandler(this.NextSet_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
			// 
			// nextPalToolStripButton
			// 
			this.nextPalToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.nextPalToolStripButton.Image = global::KiSSLab.Properties.Resources.Colors_Dark;
			this.nextPalToolStripButton.Name = "nextPalToolStripButton";
			this.nextPalToolStripButton.Size = new System.Drawing.Size(23, 23);
			this.nextPalToolStripButton.Text = "Next palette";
			this.nextPalToolStripButton.Click += new System.EventHandler(this.NextPal_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
			// 
			// zoomToolStripLabel
			// 
			this.zoomToolStripLabel.Image = global::KiSSLab.Properties.Resources.Zoom_Light;
			this.zoomToolStripLabel.Name = "zoomToolStripLabel";
			this.zoomToolStripLabel.Size = new System.Drawing.Size(16, 23);
			// 
			// toolStripZoomBar
			// 
			this.toolStripZoomBar.Name = "toolStripZoomBar";
			this.toolStripZoomBar.Size = new System.Drawing.Size(104, 23);
			this.toolStripZoomBar.ToolTipText = "Zoom level";
			this.toolStripZoomBar.Value = 1;
			this.toolStripZoomBar.ValueChanged += new System.EventHandler(this.toolStripZoomBar_ValueChanged);
			// 
			// mainStatusStrip
			// 
			this.mainStatusStrip.AutoSize = false;
			this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentToolStripStatusLabel,
            this.positionToolStripStatusLabel,
            this.lockToolStripStatusLabel,
            this.fixToolStripStatusLabel});
			this.mainStatusStrip.Location = new System.Drawing.Point(0, 525);
			this.mainStatusStrip.Name = "mainStatusStrip";
			this.mainStatusStrip.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
			this.mainStatusStrip.Size = new System.Drawing.Size(860, 35);
			this.mainStatusStrip.SizingGrip = false;
			this.mainStatusStrip.TabIndex = 2;
			this.mainStatusStrip.Text = "darkStatusStrip1";
			// 
			// currentToolStripStatusLabel
			// 
			this.currentToolStripStatusLabel.AutoSize = false;
			this.currentToolStripStatusLabel.Name = "currentToolStripStatusLabel";
			this.currentToolStripStatusLabel.Size = new System.Drawing.Size(240, 22);
			this.currentToolStripStatusLabel.Text = " ";
			this.currentToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// positionToolStripStatusLabel
			// 
			this.positionToolStripStatusLabel.AutoSize = false;
			this.positionToolStripStatusLabel.Name = "positionToolStripStatusLabel";
			this.positionToolStripStatusLabel.Size = new System.Drawing.Size(80, 22);
			this.positionToolStripStatusLabel.Text = " ";
			this.positionToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lockToolStripStatusLabel
			// 
			this.lockToolStripStatusLabel.AutoSize = false;
			this.lockToolStripStatusLabel.Name = "lockToolStripStatusLabel";
			this.lockToolStripStatusLabel.Size = new System.Drawing.Size(32, 22);
			this.lockToolStripStatusLabel.Text = " ";
			this.lockToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fixToolStripStatusLabel
			// 
			this.fixToolStripStatusLabel.AutoSize = false;
			this.fixToolStripStatusLabel.Name = "fixToolStripStatusLabel";
			this.fixToolStripStatusLabel.Size = new System.Drawing.Size(40, 22);
			this.fixToolStripStatusLabel.Text = " ";
			this.fixToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// screenContainerPanel
			// 
			this.screenContainerPanel.AutoScroll = true;
			this.screenContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.screenContainerPanel.Location = new System.Drawing.Point(0, 74);
			this.screenContainerPanel.Name = "screenContainerPanel";
			this.screenContainerPanel.Size = new System.Drawing.Size(540, 451);
			this.screenContainerPanel.TabIndex = 4;
			this.screenContainerPanel.TabStop = true;
			// 
			// viewersTabControl
			// 
			this.viewersTabControl.CloseButtons = true;
			this.viewersTabControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.viewersTabControl.Location = new System.Drawing.Point(0, 50);
			this.viewersTabControl.Name = "viewersTabControl";
			this.viewersTabControl.SelectedIndex = 0;
			this.viewersTabControl.Size = new System.Drawing.Size(540, 24);
			this.viewersTabControl.TabIndex = 0;
			this.viewersTabControl.CloseClicked += new System.Windows.Forms.TabControlEventHandler(this.viewersTabControl_CloseClicked);
			this.viewersTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.viewersTabControl_Selected);
			// 
			// celContextMenu
			// 
			this.celContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.celMenuHeader,
            this.toolStripSeparator6,
            this.resetPositionContextMenuItem,
            this.copyCelContextMenuItem,
            this.unfixContextMenuItem,
            this.refixContextMenuItem});
			this.celContextMenu.Name = "celContextMenu";
			this.celContextMenu.Size = new System.Drawing.Size(190, 121);
			// 
			// celMenuHeader
			// 
			this.celMenuHeader.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.celMenuHeader.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
			this.celMenuHeader.Name = "celMenuHeader";
			this.celMenuHeader.Size = new System.Drawing.Size(189, 22);
			this.celMenuHeader.Tag = "header";
			this.celMenuHeader.Text = "toolStripMenuItem1";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(186, 6);
			// 
			// resetPositionContextMenuItem
			// 
			this.resetPositionContextMenuItem.Image = global::KiSSLab.Properties.Resources.Undo_Dark;
			this.resetPositionContextMenuItem.Name = "resetPositionContextMenuItem";
			this.resetPositionContextMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.resetPositionContextMenuItem.Size = new System.Drawing.Size(189, 22);
			this.resetPositionContextMenuItem.Text = "&Reset Position";
			this.resetPositionContextMenuItem.Click += new System.EventHandler(this.Reset_Click);
			// 
			// copyCelContextMenuItem
			// 
			this.copyCelContextMenuItem.Image = global::KiSSLab.Properties.Resources.Copy_Dark;
			this.copyCelContextMenuItem.Name = "copyCelContextMenuItem";
			this.copyCelContextMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyCelContextMenuItem.Size = new System.Drawing.Size(189, 22);
			this.copyCelContextMenuItem.Text = "&Copy cel";
			this.copyCelContextMenuItem.Click += new System.EventHandler(this.CopyCel_Click);
			// 
			// unfixContextMenuItem
			// 
			this.unfixContextMenuItem.Image = global::KiSSLab.Properties.Resources.Unlock_Dark;
			this.unfixContextMenuItem.Name = "unfixContextMenuItem";
			this.unfixContextMenuItem.Size = new System.Drawing.Size(189, 22);
			this.unfixContextMenuItem.Text = "Unfix";
			this.unfixContextMenuItem.Click += new System.EventHandler(this.Unfix_Click);
			// 
			// refixContextMenuItem
			// 
			this.refixContextMenuItem.Image = global::KiSSLab.Properties.Resources.Lock_Dark;
			this.refixContextMenuItem.Name = "refixContextMenuItem";
			this.refixContextMenuItem.Size = new System.Drawing.Size(189, 22);
			this.refixContextMenuItem.Text = "Refix";
			this.refixContextMenuItem.Click += new System.EventHandler(this.Refix_Click);
			// 
			// editor
			// 
			this.editor.Dock = System.Windows.Forms.DockStyle.Right;
			this.editor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.editor.Location = new System.Drawing.Point(540, 50);
			this.editor.Name = "editor";
			this.editor.Size = new System.Drawing.Size(320, 475);
			this.editor.TabIndex = 3;
			// 
			// Viewer
			// 
			this.ClientSize = new System.Drawing.Size(860, 560);
			this.Controls.Add(this.screenContainerPanel);
			this.Controls.Add(this.viewersTabControl);
			this.Controls.Add(this.editor);
			this.Controls.Add(this.mainToolStrip);
			this.Controls.Add(this.mainMenuStrip);
			this.Controls.Add(this.mainStatusStrip);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.KeyPreview = true;
			this.MainMenuStrip = this.mainMenuStrip;
			this.MinimumSize = new System.Drawing.Size(0, 570);
			this.Name = "Viewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Viewer_FormClosing);
			this.ResizeEnd += new System.EventHandler(this.Viewer_ResizeEnd);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Viewer_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Viewer_KeyDown);
			this.Move += new System.EventHandler(this.Viewer_Move);
			this.Resize += new System.EventHandler(this.Viewer_Resize);
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.mainStatusStrip.ResumeLayout(false);
			this.mainStatusStrip.PerformLayout();
			this.celContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DarkUI.Controls.DarkMenuStrip mainMenuStrip;
	
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openExpansionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openInNewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reopenToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyCelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem unfixToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refixToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetPositionToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showHighlightToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem alignToGridToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

		private DarkUI.Controls.DarkContextMenu celContextMenu;
		private System.Windows.Forms.ToolStripMenuItem celMenuHeader;

		private DarkUI.Controls.DarkToolStrip mainToolStrip;
		private System.Windows.Forms.ToolStripButton openToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton gridToolStripButton;
		private System.Windows.Forms.ToolStripButton highlightToolStripButton;
		private System.Windows.Forms.ToolStripButton propertiesToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton nextSetToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton nextPalToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

		private DarkUI.Controls.DarkStatusStrip mainStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel currentToolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel positionToolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel lockToolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel fixToolStripStatusLabel;

		private MyPanel screenContainerPanel;
		//private System.Windows.Forms.PictureBox screenPictureBox;
		private Editor editor;
		private System.Windows.Forms.ToolStripMenuItem copyCelContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem unfixContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refixContextMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetPositionContextMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private ToolStripZoomBarItem toolStripZoomBar;
		private System.Windows.Forms.ToolStripLabel zoomToolStripLabel;
		private MyTab viewersTabControl;
		private System.Windows.Forms.ToolStripMenuItem xRayToolStripMenuItem;
	}
}