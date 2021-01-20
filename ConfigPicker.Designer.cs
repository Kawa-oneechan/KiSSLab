namespace KiSSLab
{
	partial class ConfigPicker
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
			this.list = new DarkUI.Controls.DarkListView();
			this.panel = new System.Windows.Forms.Panel();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// list
			// 
			this.list.Dock = System.Windows.Forms.DockStyle.Fill;
			this.list.Location = new System.Drawing.Point(15, 15);
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(313, 199);
			this.list.TabIndex = 0;
			this.list.Text = "darkListView1";
			this.list.SelectedIndicesChanged += new System.EventHandler(this.list_SelectedIndicesChanged);
			this.list.DoubleClick += new System.EventHandler(this.list_DoubleClick);
			// 
			// panel
			// 
			this.panel.Controls.Add(this.list);
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Margin = new System.Windows.Forms.Padding(15);
			this.panel.Name = "panel";
			this.panel.Padding = new System.Windows.Forms.Padding(15);
			this.panel.Size = new System.Drawing.Size(343, 229);
			this.panel.TabIndex = 2;
			// 
			// ConfigPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.ClientSize = new System.Drawing.Size(343, 274);
			this.ControlBox = false;
			this.Controls.Add(this.panel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigPicker";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select a configuration";
			this.Controls.SetChildIndex(this.panel, 0);
			this.panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DarkUI.Controls.DarkListView list;
		private System.Windows.Forms.Panel panel;



	}
}