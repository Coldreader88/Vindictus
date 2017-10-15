using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GuildService.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	[CompilerGenerated]
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
		[DefaultSettingValue("Data Source=.\\SQLEXPRESS;Initial Catalog=heroes;Integrated Security=True")]
		[DebuggerNonUserCode]
		public string heroesConnectionString
		{
			get
			{
				return (string)this["heroesConnectionString"];
			}
		}

		[DefaultSettingValue("Data Source=.\\sqlexpress;Initial Catalog=heroesLog;Integrated Security=True")]
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
