using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnifiedNetwork.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	public sealed partial class Settings : ApplicationSettingsBase
	{
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[DefaultSettingValue("27020")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public ushort ReportServiceListenPort
		{
			get
			{
				return (ushort)this["ReportServiceListenPort"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("14400")]
		public int LocationPortMin
		{
			get
			{
				return (int)this["LocationPortMin"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("126991")]
		[ApplicationScopedSetting]
		public int GameCode
		{
			get
			{
				return (int)this["GameCode"];
			}
		}

		[DefaultSettingValue("1")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public int ServerCode
		{
			get
			{
				return (int)this["ServerCode"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("False")]
		public bool PrintPerformanceInfo
		{
			get
			{
				return (bool)this["PrintPerformanceInfo"];
			}
		}

		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
