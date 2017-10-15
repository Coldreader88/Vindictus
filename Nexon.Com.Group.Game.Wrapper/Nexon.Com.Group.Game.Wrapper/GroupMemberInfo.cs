using System;

namespace Nexon.Com.Group.Game.Wrapper
{
	public class GroupMemberInfo
	{
		public int GuildSN { get; internal set; }

		public int NexonSN { get; internal set; }

		public string NexonID { get; internal set; }

		public string NameInGroup { get; internal set; }

		public long CharacterSN { get; internal set; }

		public string Intro { get; internal set; }

		public DateTime dtLastLoginDate { get; internal set; }

		public GroupUserType emGroupUserType { get; internal set; }

		public string CharacterName { get; internal set; }
	}
}
