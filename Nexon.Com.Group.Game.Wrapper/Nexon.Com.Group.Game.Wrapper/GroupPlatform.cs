using System;

namespace Nexon.Com.Group.Game.Wrapper
{
	public static class GroupPlatform
	{
		static GroupPlatform()
		{
			GroupConfigSection section = GroupConfig.GetSection();
			if (section == null)
			{
				throw new Exception("Configuration Excpetion : Fail Load Config");
			}
			GroupPlatform._ServiceMode = section.Service.Mode.ToUpper().Parse(GroupPlatform.Mode.SERVICE);
			GroupPlatform._isOverSea = section.Service.isOverSea;
			GroupPlatform._GameCode = section.Service.Gamecode;
			GroupPlatform._ConnectionTimeout = section.Service.ConnectionTimeout;
			GroupPlatform._Logging = section.Service.isDataLogging;
			switch (GroupPlatform._ServiceMode)
			{
			case GroupPlatform.Mode.SERVICE:
				GroupPlatform._GuildConnectionString = section.Service.DataBase_SERVICE;
				GroupPlatform._GuildMasterConnectionString = section.Service.DataBase_GuildMaster_SERVICE;
				break;
			case GroupPlatform.Mode.TEST:
				GroupPlatform._GuildConnectionString = section.Service.DataBase_TEST;
				GroupPlatform._GuildMasterConnectionString = section.Service.DataBase_GuildMaster_TEST;
				break;
			default:
				GroupPlatform._GuildConnectionString = section.Service.DataBase_WORK;
				GroupPlatform._GuildMasterConnectionString = section.Service.DataBase_GuildMaster_WORK;
				break;
			}
		}

		public static void GetConfigTest(out GroupPlatform.Mode ServiceMode, out int GameCode, out bool isOverSea, out string GuildConnectionString, out string GuildMasterConnectionString)
		{
			ServiceMode = GroupPlatform.ServiceMode;
			GameCode = GroupPlatform.GameCode;
			isOverSea = GroupPlatform.isOverSea;
			GuildConnectionString = GroupPlatform.GuildConnectionString;
			GuildMasterConnectionString = GroupPlatform.GuildMasterConnectionString;
		}

		public static int ConnectionTimeout
		{
			get
			{
				return GroupPlatform._ConnectionTimeout;
			}
		}

		internal static int GameCode
		{
			get
			{
				return GroupPlatform._GameCode;
			}
		}

		internal static string GuildConnectionString
		{
			get
			{
				return GroupPlatform._GuildConnectionString;
			}
		}

		public static string GuildMasterConnectionString
		{
			get
			{
				return GroupPlatform._GuildMasterConnectionString;
			}
		}

		internal static bool isOverSea
		{
			get
			{
				return GroupPlatform._isOverSea;
			}
		}

		public static bool Logging
		{
			get
			{
				return GroupPlatform._Logging;
			}
		}

		internal static GroupPlatform.Mode ServiceMode
		{
			get
			{
				return GroupPlatform._ServiceMode;
			}
		}

		private static int _ConnectionTimeout = 3;

		private static int _GameCode = 0;

		private static string _GuildConnectionString = string.Empty;

		private static string _GuildMasterConnectionString = string.Empty;

		private static bool _isOverSea = false;

		private static bool _Logging = false;

		private static GroupPlatform.Mode _ServiceMode = GroupPlatform.Mode.WORK;

		public enum Mode
		{
			SERVICE,
			TEST,
			WORK
		}
	}
}
