using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CashShopService.Properties
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

		[DefaultSettingValue("Data Source=.\\SQLEXPRESS;Initial Catalog=heroesLOG;Integrated Security=True")]
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

		[SpecialSetting(SpecialSetting.ConnectionString)]
		[DefaultSettingValue("Data Source=.\\sqlexpress;Initial Catalog=heroesWeb;Integrated Security=True")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string heroesShopConnectionString
		{
			get
			{
				return (string)this["heroesShopConnectionString"];
			}
		}

		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
