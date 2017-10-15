using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GRCService.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
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

		[SpecialSetting(SpecialSetting.ConnectionString)]
		[ApplicationScopedSetting]
		[DefaultSettingValue("Data Source=.\\sqlexpress;Initial Catalog=HeroesLog;Integrated Security=True")]
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
