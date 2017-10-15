using System;
using Nexon.Com.Group.Game.Wrapper.GroupBase.DAO;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase
{
	internal class GroupCreateResult : GroupResultBase
	{
		public int AlreadyJoinGroupSN { get; internal set; }

		public int GuildSN { get; internal set; }
	}
}
