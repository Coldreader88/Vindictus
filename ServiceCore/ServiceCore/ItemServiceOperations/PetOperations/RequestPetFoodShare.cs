using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class RequestPetFoodShare : Operation
	{
		public long ItemID { get; set; }

		public RequestPetFoodShare(long itemID)
		{
			this.ItemID = itemID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
