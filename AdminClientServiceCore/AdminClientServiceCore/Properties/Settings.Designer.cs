using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdminClientServiceCore.Properties
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

		[SpecialSetting(SpecialSetting.ConnectionString)]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("Data Source=.\\SQLEXPRESS;Initial Catalog=heroesLog;Integrated Security=True")]
		public string heroesLogConnectionString
		{
			get
			{
				return (string)this["heroesLogConnectionString"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("Data Source=.\\SQLEXPRESS;Initial Catalog=heroes;Integrated Security=True")]
		[ApplicationScopedSetting]
		[SpecialSetting(SpecialSetting.ConnectionString)]
		public string heroesConnectionString
		{
			get
			{
				return (string)this["heroesConnectionString"];
			}
		}

		[DefaultSettingValue("False")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public bool ignoreDSServiceReady
		{
			get
			{
				return (bool)this["ignoreDSServiceReady"];
			}
		}

		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
