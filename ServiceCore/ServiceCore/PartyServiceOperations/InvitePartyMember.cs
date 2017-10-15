using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class InvitePartyMember : Operation
	{
		public long InvitingCID { get; set; }

		public long InvitedCID { get; set; }

		public string InvitedName { get; set; }

		public InvitePartyMember(long InvitingCID, long InvitedCID, string InvitedName)
		{
			this.InvitingCID = InvitingCID;
			this.InvitedCID = InvitedCID;
			this.InvitedName = InvitedName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
