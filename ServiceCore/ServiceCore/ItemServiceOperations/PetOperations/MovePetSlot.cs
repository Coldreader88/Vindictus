using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class MovePetSlot : Operation
	{
		public long PetID { get; set; }

		public int DestSlot { get; set; }

		public MovePetSlot(long petID, int destSlot)
		{
			this.PetID = petID;
			this.DestSlot = destSlot;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
