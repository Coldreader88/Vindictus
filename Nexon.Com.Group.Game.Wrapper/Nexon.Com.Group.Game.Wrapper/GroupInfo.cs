using System;

namespace Nexon.Com.Group.Game.Wrapper
{
	public class GroupInfo
	{
		public int GuildSN { get; internal set; }

		public string GuildName { get; internal set; }

		public string Intro { get; internal set; }

		public string NameInGroup_Master { get; internal set; }

		public string NexonID_Master { get; internal set; }

		public string GuildID { get; internal set; }

		public int NexonSN_Master { get; internal set; }

		public int RealUserCount { get; internal set; }

		public DateTime dtCreateDate { get; internal set; }

		public int CharacterSN_Master { get; internal set; }
	}
}
