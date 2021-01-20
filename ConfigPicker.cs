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
	public partial class ConfigPicker : DarkDialog
	{
		public string Choice;

		public ConfigPicker(string[] configs)
		{
			InitializeComponent();
		
			Choice = configs[0]; //default

			foreach (var config in configs)
			{
				list.Items.Add(new DarkListItem() { Text = config });
			}
			list.SelectItem(0);
			//list.Items.AddRange(configs);
			//list.SelectedIndex = 0;
			//list.Click += (s, e) => { Choice = list.SelectedItem.ToString(); };
			//list.DoubleClick += (s, e) => { Choice = list.SelectedItem.ToString(); Close();	};
		}

		private void list_SelectedIndicesChanged(object sender, EventArgs e)
		{
			Choice = list.Items[list.SelectedIndices[0]].Text;
		}

		private void list_DoubleClick(object sender, EventArgs e)
		{
			Choice = list.Items[list.SelectedIndices[0]].Text;
			Close();
		}
	}
}
