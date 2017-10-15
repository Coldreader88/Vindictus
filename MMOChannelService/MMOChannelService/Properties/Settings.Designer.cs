using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MMOChannelService.Properties
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

		[DefaultSettingValue("Data Source=.\\SQLEXPRESS;Initial Catalog=heroes;Integrated Security=True")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[SpecialSetting(SpecialSetting.ConnectionString)]
		public string heroesConnectionString
		{
			get
			{
				return (string)this["heroesConnectionString"];
			}
		}

		[SpecialSetting(SpecialSetting.ConnectionString)]
		[DefaultSettingValue("Data Source=.\\sqlexpress;Initial Catalog=heroesShare;Integrated Security=True")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string heroesShareConnectionString
		{
			get
			{
				return (string)this["heroesShareConnectionString"];
			}
		}

		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
