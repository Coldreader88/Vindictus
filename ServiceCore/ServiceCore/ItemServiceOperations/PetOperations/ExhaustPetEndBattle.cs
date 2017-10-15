using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class ExhaustPetEndBattle : Operation
	{
		public long PetID { get; set; }

		public int PlayTime { get; set; }

		public int RewardExp { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
