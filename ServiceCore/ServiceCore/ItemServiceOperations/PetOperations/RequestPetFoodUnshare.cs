using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class RequestPetFoodUnshare : Operation
	{
		public string ItemClass { get; set; }

		public RequestPetFoodUnshare(string itemClass)
		{
			this.ItemClass = itemClass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
