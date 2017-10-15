using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MemberInfoMessage : IMessage
	{
		public int UID { get; set; }

		public GameJoinMemberInfo MemberInfo { get; set; }

		public MemberInfoMessage(int tag, GameJoinMemberInfo info)
		{
			this.UID = tag;
			this.MemberInfo = info;
		}

		public override string ToString()
		{
			return string.Format("MemberInfoMessage[ UID = {0} MemberInfo = {1} ]", this.UID, this.MemberInfo);
		}
	}
}
