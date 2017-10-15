using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingMemberInfoMessage : IMessage
	{
		public GameJoinMemberInfo MemberInfo { get; set; }

		public HousingMemberInfoMessage(GameJoinMemberInfo memberInfo)
		{
			this.MemberInfo = memberInfo;
		}

		public override string ToString()
		{
			return string.Format("HousingMemberInfoMessage [ Memger {0} ]", this.MemberInfo.Name);
		}
	}
}
