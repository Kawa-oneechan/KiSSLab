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

			if (!Visible)
				return;

			e.Graphics.Clear(this.Parent.BackColor);

			for (var i = 0; i < this.TabCount; i++)
				DrawTab(e.Graphics, this.TabPages[i], i);
		}

		internal void DrawTab(Graphics g, TabPage tabPage, int nIndex)
		{
			var recBounds = this.GetTabRect(nIndex);
			var tabTextArea = (RectangleF)this.GetTabRect(nIndex);
			recBounds.Inflate(0, 1);

			var bSelected = (this.SelectedIndex == nIndex);

			if (bSelected)
			{
				g.FillRectangle(new SolidBrush(DarkUI.Config.Colors.BlueBackground), recBounds);
				g.DrawLine(new Pen(DarkUI.Config.Colors.DarkBlueBorder), recBounds.Right, recBounds.Top, recBounds.Right, recBounds.Bottom);
				g.DrawLine(new Pen(DarkUI.Config.Colors.LightBlueBorder), recBounds.Left, recBounds.Top, recBounds.Right, recBounds.Top);
				g.DrawLine(new Pen(DarkUI.Config.Colors.LightBlueBorder), recBounds.Left, recBounds.Top, recBounds.Left, recBounds.Bottom);
			}

			var stringFormat = new StringFormat()
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center,
			};
			g.DrawString(tabPage.Text, Font, new SolidBrush(DarkUI.Config.Colors.LightText), tabTextArea, stringFormat);
		}
	}

	class MyPanel : Panel
	{
		public MyPanel() : base()
		{
			this.DoubleBuffered = true;
		}
	}
}