using System;

namespace Nexon.Com.Group.Game.Wrapper
{
	public class GroupUserInfo
	{
		internal string Name { get; set; }

		internal byte isNewMember { get; set; }

		public int NexonSN { get; internal set; }

		public string NameInGroup_User { get; internal set; }

		public int GuildSN { get; internal set; }

		public string GuildID { get; internal set; }

		public string GuildName { get; internal set; }

		public string GuildIntro { get; internal set; }

		public int NexonSN_Master { get; internal set; }

		public string NameInGroup_Master { get; internal set; }

		public GroupUserType emGroupUserType { get; internal set; }

		public int RealUserCount { get; internal set; }

		public DateTime dtLastLoginTimeDate { get; internal set; }

		public string CharacterName { get; internal set; }
	}
}
