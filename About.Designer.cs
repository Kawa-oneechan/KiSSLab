namespace KiSSLab
{
	partial class About
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
			this.panel = new System.Windows.Forms.Panel();
			this.rest = new DarkUI.Controls.DarkLabel();
			this.header = new DarkUI.Controls.DarkLabel();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.Controls.Add(this.rest);
			this.panel.Controls.Add(this.header);
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Padding = new System.Windows.Forms.Padding(15, 15, 15, 56);
			this.panel.Size = new System.Drawing.Size(343, 229);
			this.panel.TabIndex = 2;
			// 
			// rest
			// 
			this.rest.Dock = System.Windows.Forms.DockStyle.Top;
			this.rest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.rest.Location = new System.Drawing.Point(15, 115);
			this.rest.Name = "rest";
			this.rest.Size = new System.Drawing.Size(313, 111);
			this.rest.TabIndex = 1;
			this.rest.Text = "...";
			this.rest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// header
			// 
			this.header.Dock = System.Windows.Forms.DockStyle.Top;
			this.header.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.header.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.header.Location = new System.Drawing.Point(15, 15);
			this.header.Name = "header";
			this.header.Size = new System.Drawing.Size(313, 100);
			this.header.TabIndex = 0;
			this.header.Text = "...";
			this.header.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// About
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.ClientSize = new System.Drawing.Size(343, 274);
			this.ControlBox = false;
			this.Controls.Add(this.panel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.Controls.SetChildIndex(this.panel, 0);
			this.panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel;
		private DarkUI.Controls.DarkLabel header;
		private DarkUI.Controls.DarkLabel rest;


	}
}