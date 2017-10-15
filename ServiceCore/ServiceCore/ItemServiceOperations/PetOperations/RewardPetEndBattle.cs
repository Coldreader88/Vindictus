using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class RewardPetEndBattle : Operation
	{
		public ICollection<RewardPetElement> RewardPetList { get; set; }

		public RewardPetEndBattle(ICollection<RewardPetElement> list)
		{
			this.RewardPetList = list;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
