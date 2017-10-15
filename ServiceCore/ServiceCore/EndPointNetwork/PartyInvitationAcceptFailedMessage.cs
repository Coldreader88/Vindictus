using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyInvitationAcceptFailedMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("PartyInvitationAcceptFailedMessage[ ]", new object[0]);
		}
	}
}
