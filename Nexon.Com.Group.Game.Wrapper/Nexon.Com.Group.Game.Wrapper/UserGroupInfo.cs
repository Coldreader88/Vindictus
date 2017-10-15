using System;

namespace Nexon.Com.Group.Game.Wrapper
{
	public class UserGroupInfo
	{
		public int GuildSN { get; internal set; }

		public string GuildID { get; internal set; }

		public string GuildName { get; internal set; }

		public string Intro { get; internal set; }

		public int NeoxnSN_Master { get; internal set; }

		public string NexonID_Master { get; internal set; }

		public string NameInGroup_Master { get; internal set; }

		public GroupUserType GroupUserType { get; internal set; }

		public int RealUserCOunt { get; internal set; }

		public DateTime dtLastContentUpdateDate { get; internal set; }

		public string NameInGroup { get; internal set; }

		public string CharacterName { get; internal set; }

		public DateTime dateCreate { get; internal set; }

		public int CharacterSN { get; internal set; }
	}
}
