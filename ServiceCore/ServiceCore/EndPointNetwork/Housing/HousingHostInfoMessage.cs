using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingHostInfoMessage : IMessage
	{
		public GameInfo GameInfo { get; set; }

		public GameJoinMemberInfo MemberInfo { get; set; }

		public HousingHostInfoMessage(GameInfo gameInfo, GameJoinMemberInfo memberInfo)
		{
			this.GameInfo = gameInfo;
			this.MemberInfo = memberInfo;
		}

		public override string ToString()
		{
			return string.Format("HousingHostInfoMessage [ GameInfo {0} MemberInfo {1} ]", this.GameInfo.HostID, this.MemberInfo.Name);
		}
	}
}
