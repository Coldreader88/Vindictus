using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class InviteHousingMember : Operation
	{
		public long InvitingCID { get; set; }

		public long InvitedCID { get; set; }

		public string InvitedName { get; set; }

		public InviteHousingMember(long invitingCID, long invitedCID, string InvitedName)
		{
			this.InvitingCID = invitingCID;
			this.InvitedCID = invitedCID;
			this.InvitedName = InvitedName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
