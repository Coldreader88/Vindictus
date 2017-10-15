using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyInviteMessage : IMessage
	{
		public string InvitedName { get; set; }

		public PartyInviteMessage(string Name)
		{
			this.InvitedName = Name;
		}

		public override string ToString()
		{
			return string.Format("PartyInviteMessage[ InvitedName = {0}]", this.InvitedName);
		}
	}
}
