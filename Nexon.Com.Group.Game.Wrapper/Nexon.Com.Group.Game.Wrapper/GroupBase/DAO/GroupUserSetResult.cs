using System;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO
{
	internal class GroupUserSetResult : GroupResultBase
	{
		public GroupUserSetResult()
		{
			this.UserInfo = new GroupUserInfo();
		}

		public GroupUserInfo UserInfo { get; internal set; }
	}
}
