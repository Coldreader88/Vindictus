using System;
using System.Collections.ObjectModel;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO
{
	internal class GroupUserGetListResult : GroupResultBase
	{
		public GroupUserGetListResult()
		{
			this.UserInfoList = new Collection<GroupMemberInfo>();
		}

		internal int RowCount { get; set; }

		internal int TotalRowCount { get; set; }

		internal Collection<GroupMemberInfo> UserInfoList { get; set; }
	}
}
