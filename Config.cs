using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;

namespace Kawa.Configuration
{
	public static class Config
	{
		[Setting(-1)]
		public static int WindowLeft { get; set; }
		[Setting(-1)]
		public static int WindowTop { get; set; }
		[Setting(816)]
		public static int WindowWidth { get; set; }
		[Setting(484), System.ComponentModel.Description("Hi mom!")]
		public static int WindowHeight { get; set; }
		[Setting(0)]
		public static int WindowState { get; set; }
		[Setting(1)]
		public static int ZoomLevel { get; set; }
		[Setting("")]
		public static string AutoLoad { get; set; }

		public static string Path { get; set; }

		public static void Load()
		{
			var myType = typeof(Config);
			var settings = myType.GetProperties();
			foreach (var setting in settings)
			{
				var attribs = setting.GetCustomAttributes(true).OfType<SettingAttribute>().ToArray();
				if (attribs.Length == 0)
					continue;
				var defaultVal = ((SettingAttribute)attribs[0]).Default;
				var val = Registry.GetValue(string.Format(@"HKEY_CURRENT_USER\SOFTWARE\{0}", Path), setting.Name, defaultVal);
				setting.SetValue(null, val, null);
			}
		}

		public static void Save()
		{
			var myType = typeof(Config);
			var settings = myType.GetProperties();
			foreach (var setting in settings)
			{
				var attribs = setting.GetCustomAttributes(true).OfType<SettingAttribute>().ToArray();
				if (attribs.Length == 0)
					continue;
				var val = setting.GetValue(null, null);
				Registry.SetValue(string.Format(@"HKEY_CURRENT_USER\SOFTWARE\{0}", Path), setting.Name, val);
			}
		}

		/// <summary>
		/// Specifies that a field of the <see cref="Config"/> class should be stored in the Registry.
		/// </summary>
		public class SettingAttribute : Attribute
		{
			/// <summary>
			/// Gets a value indicating the default for this setting.
			/// </summary>
			public object Default { get; private set; }
			/// <summary>
			/// Initializes a new instance of the SettingAttribute class.
			/// </summary>
			/// <param name="defaultValue">the default value to return if the setting is not found in the Registry.</param>
			public SettingAttribute(object defaultValue)
			{
				Default = defaultValue;
			}
		}
	}
}
