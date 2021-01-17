//Custom tab control because DarkUI doesn't have one
using System.Drawing;
using System.Windows.Forms;

namespace KiSSLab
{
	class MyTab : TabControl
	{
		private const int nMargin = 5;
		
		public MyTab()
		{
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			DrawControl(e.Graphics);
		}

		internal void DrawControl(Graphics g)
		{
			if (!Visible)
				return;

			g.Clear(this.Parent.BackColor);

			var TabArea = this.DisplayRectangle;

			var br = new SolidBrush(DarkUI.Config.Colors.LightBlueBorder);
			var TabControlArea = new Rectangle(this.ClientRectangle.Left, this.ClientRectangle.Top + 21, this.ClientRectangle.Width, this.ClientRectangle.Height - 21);
			g.FillRectangle(br, TabControlArea);
			TabControlArea = this.ClientRectangle;

			var rsaved = g.Clip;

			var nWidth = TabArea.Width + nMargin;

			var rreg = new Rectangle(TabArea.Left, TabControlArea.Top, nWidth - nMargin, TabControlArea.Height);

			g.SetClip(rreg);

			for (var i = 0; i < this.TabCount; i++)
				DrawTab(g, this.TabPages[i], i);

			g.Clip = rsaved;
		}

		internal void DrawTab(Graphics g, TabPage tabPage, int nIndex)
		{
			var recBounds = this.GetTabRect(nIndex);
			var tabTextArea = (RectangleF)this.GetTabRect(nIndex);
			recBounds.Inflate(0, -2);

			var bSelected = (this.SelectedIndex == nIndex);

			var br = new SolidBrush(bSelected ? DarkUI.Config.Colors.LightBlueBorder : this.Parent.BackColor);
			g.FillRectangle(br, recBounds);
			
			if ((tabPage.ImageIndex >= 0) && (ImageList != null) && (ImageList.Images[tabPage.ImageIndex] != null))
			{
				var nLeftMargin = 8;
				var nRightMargin = 2;

				var img = ImageList.Images[tabPage.ImageIndex];

				var rimage = new Rectangle(recBounds.X + nLeftMargin, recBounds.Y + 1, img.Width, img.Height);

				var nAdj = (float)(nLeftMargin + img.Width + nRightMargin);

				rimage.Y += (recBounds.Height - img.Height) / 2;
				tabTextArea.X += nAdj;
				tabTextArea.Width -= nAdj;

				g.DrawImage(img, rimage);
			}

			var stringFormat = new StringFormat()
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center,
			};
			br = new SolidBrush(DarkUI.Config.Colors.LightText);
			g.DrawString(tabPage.Text, Font, br, tabTextArea, stringFormat);
		}
	}
}
