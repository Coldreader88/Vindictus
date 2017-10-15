using System;
using System.Collections.ObjectModel;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO
{
	internal class GroupGetListResult : GroupResultBase
	{
		public GroupGetListResult()
		{
			this.GroupList = new Collection<GroupInfo>();
		}

		public Collection<GroupInfo> GroupList { get; internal set; }

		public int TotalRowCount { get; internal set; }
	}
}
