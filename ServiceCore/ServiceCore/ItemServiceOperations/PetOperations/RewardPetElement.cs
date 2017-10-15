using System;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public class RewardPetElement
	{
		public long PetID { get; set; }

		public int PlayTime { get; set; }

		public int RewardExp { get; set; }
	}
}
