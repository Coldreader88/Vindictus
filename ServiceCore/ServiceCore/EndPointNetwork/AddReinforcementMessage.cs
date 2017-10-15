using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AddReinforcementMessage : IMessage
	{
		public int UID { get; set; }

		public GameJoinMemberInfo MemberInfo { get; set; }

		public override string ToString()
		{
			return string.Format("AddReinforcementMessage[ UID = {0} MemberInfo = {1} ]", this.UID, this.MemberInfo);
		}
	}
}
