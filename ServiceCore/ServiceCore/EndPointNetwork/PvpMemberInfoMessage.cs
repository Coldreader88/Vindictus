using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpMemberInfoMessage : IMessage
	{
		public GameJoinMemberInfo MemberInfo { get; set; }

		public int TeamID { get; set; }

		public PvpMemberInfoMessage(GameJoinMemberInfo memberinfo, PvpTeamID teamID)
		{
			this.MemberInfo = memberinfo;
			this.TeamID = (int)teamID;
		}

		public override string ToString()
		{
			return string.Format("PvpMemberInfoMessage[ Member {0} team = {1} ]", this.MemberInfo.Name, (PvpTeamID)this.TeamID);
		}
	}
}
