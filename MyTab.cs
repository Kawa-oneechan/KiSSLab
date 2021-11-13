//Custom tab control because DarkUI doesn't have one
using System.Drawing;
using System.Windows.Forms;

namespace KiSSLab
{
	class MyTab : TabControl
	{
		public event TabControlEventHandler CloseClicked;
		public bool CloseButtons { get; set; }

		public MyTab()
		{
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);

			//if (CloseButtons)
			{
				this.Padding = new Point(32, 0);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (!Visible)
				return;

			e.Graphics.Clear(DarkUI.Config.Colors.GreyBackground);

			for (var i = 0; i < this.TabCount; i++)
				DrawTab(e.Graphics, this.TabPages[i], i);
		}

		internal void DrawTab(Graphics g, TabPage tabPage, int nIndex)
		{
			var recBounds = this.GetTabRect(nIndex);
			var tabTextArea = (RectangleF)this.GetTabRect(nIndex);
			recBounds.Inflate(0, 1);
			
			var bSelected = (this.SelectedIndex == nIndex);

			var fill = new SolidBrush(DarkUI.Config.Colors.BlueBackground);
			var dark = new Pen(DarkUI.Config.Colors.DarkBlueBorder);
			var light = new Pen(DarkUI.Config.Colors.LightBlueBorder);
			var text = new SolidBrush(DarkUI.Config.Colors.LightText);

			if (bSelected)
			{
				g.FillRectangle(fill, recBounds);
				g.DrawLine(dark, recBounds.Right, recBounds.Top, recBounds.Right, recBounds.Bottom);
				g.DrawLine(light, recBounds.Left, recBounds.Top, recBounds.Right, recBounds.Top);
				g.DrawLine(light, recBounds.Left, recBounds.Top, recBounds.Left, recBounds.Bottom);
			}

			tabTextArea.Offset(8, 0);
			var stringFormat = new StringFormat()
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center,
			};
			g.DrawString(tabPage.Text, Font, text, tabTextArea, stringFormat);
			if (bSelected && CloseButtons)
			{
				stringFormat.Alignment = StringAlignment.Far;
				tabTextArea.Offset(-10, 0);
				g.DrawString("×", Font, text, tabTextArea, stringFormat);
			}
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);

			if (!CloseButtons)
				return;
			if (CloseClicked == null)
				return;
			var recBounds = this.GetTabRect(this.SelectedIndex);
			var closeBox = new Rectangle(recBounds.Right - 10, recBounds.Top, 10, recBounds.Height);
			if (closeBox.Contains(e.Location))
				CloseClicked(this, new TabControlEventArgs(this.SelectedTab, this.SelectedIndex, TabControlAction.Deselecting));
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