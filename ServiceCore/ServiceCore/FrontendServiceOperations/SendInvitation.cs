using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class SendInvitation : Operation
	{
		public string HostName { get; set; }

		public long PartyID { get; set; }

		public long TargetCID { get; set; }

		public SendInvitation(string HostName, long PartyID, long TargetCID)
		{
			this.HostName = HostName;
			this.PartyID = PartyID;
			this.TargetCID = TargetCID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
