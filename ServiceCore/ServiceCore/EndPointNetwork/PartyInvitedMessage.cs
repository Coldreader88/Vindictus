using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyInvitedMessage : IMessage
	{
		public string HostName { get; set; }

		public long PartyID { get; set; }

		public PartyInvitedMessage(string HostName, long PartyID)
		{
			this.HostName = HostName;
			this.PartyID = PartyID;
		}

		public override string ToString()
		{
			return string.Format("PartyInvitedMessage[ HostName = {0} PartyID = {1} ]", this.HostName, this.PartyID);
		}
	}
}
