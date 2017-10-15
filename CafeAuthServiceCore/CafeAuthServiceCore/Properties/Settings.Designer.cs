using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CafeAuthServiceCore.Properties
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

		[ApplicationScopedSetting]
		[SpecialSetting(SpecialSetting.ConnectionString)]
		[DebuggerNonUserCode]
		[DefaultSettingValue("Data Source=.\\SQLEXPRESS;Initial Catalog=heroes;Integrated Security=True")]
		public string heroesConnectionString
		{
			get
			{
				return (string)this["heroesConnectionString"];
			}
		}

		[DefaultSettingValue("Data Source=.\\SQLEXPRESS;Initial Catalog=heroesLOG;Integrated Security=True")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[SpecialSetting(SpecialSetting.ConnectionString)]
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
