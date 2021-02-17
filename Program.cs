using System;
using System.Windows.Forms;

namespace KiSSLab
{
	static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.AddMessageFilter(new ControlScrollFilter());
			Application.Run(new Viewer(args));
		}
	}
}
