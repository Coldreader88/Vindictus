using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class SetPetLevelEXP : Operation
	{
		public int PetLevel { get; set; }

		public int PetEXP { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
