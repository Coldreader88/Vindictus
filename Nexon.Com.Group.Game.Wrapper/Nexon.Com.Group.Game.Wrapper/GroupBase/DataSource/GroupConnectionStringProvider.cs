using System;
using Nexon.Com.DAO;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DataSource
{
	internal class GroupConnectionStringProvider : ISQLConnectionStringProvider
	{
		static GroupConnectionStringProvider()
		{
			if (GroupConnectionStringProvider._ConnectionStringProvider == null)
			{
				GroupConnectionStringProvider._ConnectionStringProvider = new GroupConnectionStringProvider();
			}
		}

		string ISQLConnectionStringProvider.GetConnectionString()
		{
			this._GroupConnectionString = GroupPlatform.GuildConnectionString;
			if (this._GroupConnectionString == null || this._GroupConnectionString == string.Empty)
			{
				throw new NotImplementedException("Check App.Conifg's appSettings key Platform");
			}
			return this._GroupConnectionString;
		}

		public static GroupConnectionStringProvider GetInstalce
		{
			get
			{
				return GroupConnectionStringProvider._ConnectionStringProvider;
			}
		}

		private static GroupConnectionStringProvider _ConnectionStringProvider;

		private string _GroupConnectionString = string.Empty;
	}
}
