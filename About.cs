using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Config;
using DarkUI.Controls;
using DarkUI.Forms;
using Kawa.Configuration;
using Kawa.Mix;
using Microsoft.Win32;

namespace KiSSLab
{
	public partial class About : DarkDialog
	{
		public string Choice;

		public About()
		{
			InitializeComponent();

			this.Text = string.Format("About {0}", Application.ProductName);

			header.Image = (new Icon(global::KiSSLab.Properties.Resources.app, new Size(64, 64))).ToBitmap();
			header.Text = Application.ProductName;
			rest.Text = string.Format("Because KiSS/GS is old and busted, I guess.\r\n\r\nVersion {0}\r\n\r\n© 2021 Firrhna Productions.\r\nDarkUI by Robin Perris, seasoned to taste.", Application.ProductVersion);
		}
	}
}
