using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace HackShieldServiceCore.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[DefaultSettingValue("Data Source=.\\SQLEXPRESS;Initial Catalog=heroesLog;Integrated Security=True")]
		[SpecialSetting(SpecialSetting.ConnectionString)]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string heroesLogConnectionString
		{
			get
			{
				return (string)this["heroesLogConnectionString"];
			}
		}

		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
