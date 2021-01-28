using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;

namespace Kawa.Configuration
{
	/// <summary>
	/// The base class for a Configuration that does not include any particular storage method.
	/// </summary>
	public abstract class Config
	{
		public abstract object GetValue(string name, object defaultVal);
		public abstract void SetValue(string name, object value);
		
		public void Load()
		{
			var myType = this.GetType();
			var settings = myType.GetProperties();
			foreach (var setting in settings)
			{
				var attribs = setting.GetCustomAttributes(true).OfType<SettingAttribute>().ToArray();
				if (attribs.Length == 0)
					continue;
				var defaultVal = ((SettingAttribute)attribs[0]).Default;
				var val = GetValue(setting.Name, defaultVal);
				setting.SetValue(this, val, null);
			}
		}

		public void Save()
		{
			var myType = this.GetType();
			var settings = myType.GetProperties();
			foreach (var setting in settings)
			{
				var attribs = setting.GetCustomAttributes(true).OfType<SettingAttribute>().ToArray();
				if (attribs.Length == 0)
					continue;
				var val = setting.GetValue(this, null);
				SetValue(setting.Name, val);
			}
		}
	}

	/// <summary>
	/// A Kawa.Configuration that stores its values in the Windows Registry.
	/// </summary>
	public class RegistryConfig : Config
	{
		public string Path { get; set; }

		public RegistryConfig() : base() { }

		public RegistryConfig(string path)
		{
			Path = path;
			Load();
		}

		public override object GetValue(string name, object defaultVal)
		{
			return Registry.GetValue(string.Format(@"HKEY_CURRENT_USER\SOFTWARE\{0}", Path), name, defaultVal);
		}

		public override void SetValue(string name, object value)
		{
			Registry.SetValue(string.Format(@"HKEY_CURRENT_USER\SOFTWARE\{0}", Path), name, value);
		}
	}

	/// <summary>
	/// Specifies that a field of the <see cref="Config"/> class should be stored.
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
