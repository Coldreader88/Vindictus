using System;
using Nexon.Com.DAO;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DataSource
{
	internal class GroupMasterConnectionStringProvider : ISQLConnectionStringProvider
	{
		static GroupMasterConnectionStringProvider()
		{
			if (GroupMasterConnectionStringProvider._ConnectionStringProvider == null)
			{
				GroupMasterConnectionStringProvider._ConnectionStringProvider = new GroupMasterConnectionStringProvider();
			}
		}

		string ISQLConnectionStringProvider.GetConnectionString()
		{
			this._GroupConnectionString = GroupPlatform.GuildMasterConnectionString;
			if (this._GroupConnectionString == null || this._GroupConnectionString == string.Empty)
			{
				throw new NotImplementedException("Check App.Conifg's appSettings key Platform");
			}
			return this._GroupConnectionString;
		}

		public static GroupMasterConnectionStringProvider GetInstalce
		{
			get
			{
				return GroupMasterConnectionStringProvider._ConnectionStringProvider;
			}
		}

		private static GroupMasterConnectionStringProvider _ConnectionStringProvider;

		private string _GroupConnectionString = string.Empty;
	}
}
