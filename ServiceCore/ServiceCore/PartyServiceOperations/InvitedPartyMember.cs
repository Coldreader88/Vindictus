using System;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class InvitedPartyMember
	{
		public int SlotNum { get; set; }

		public InvitedPartyMember(int slotNum)
		{
			this.SlotNum = slotNum;
		}
	}
}
