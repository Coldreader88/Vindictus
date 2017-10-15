using System;
using System.Collections.ObjectModel;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO
{
	internal class UserGroupGetListResult : GroupResultBase
	{
		public UserGroupGetListResult()
		{
			this.InfoList = new Collection<UserGroupInfo>();
		}

		public Collection<UserGroupInfo> InfoList { get; internal set; }

		public int RowCount { get; internal set; }

		public int TotalRowCount { get; internal set; }
	}
}
