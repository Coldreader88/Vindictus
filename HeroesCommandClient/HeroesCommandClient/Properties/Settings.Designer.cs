using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace HeroesCommandClient.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("localhost")]
		public string RCServerIP
		{
			get
			{
				return (string)this["RCServerIP"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("10002")]
		public int RCServerPort
		{
			get
			{
				return (int)this["RCServerPort"];
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("_usermonitor")]
		[DebuggerNonUserCode]
		public string ID
		{
			get
			{
				return (string)this["ID"];
			}
		}

		[DefaultSettingValue("dbwjAHSLXJ!123")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string Password
		{
			get
			{
				return (string)this["Password"];
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("localhost")]
		[DebuggerNonUserCode]
		public string EchoServerIP
		{
			get
			{
				return (string)this["EchoServerIP"];
			}
		}

		[DefaultSettingValue("8085")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public int EchoServerPort
		{
			get
			{
				return (int)this["EchoServerPort"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[ApplicationScopedSetting]
		public bool EchoServerUse
		{
			get
			{
				return (bool)this["EchoServerUse"];
			}
		}

		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
