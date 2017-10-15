using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetFeedListMessage : IMessage
	{
		public ICollection<PetFeedElement> PetFeedList { get; set; }

		public bool IsTotalPetList { get; set; }

		public PetFeedListMessage(ICollection<PetFeedElement> petFeed, bool isTotal)
		{
			this.PetFeedList = petFeed;
			this.IsTotalPetList = isTotal;
		}

		public override string ToString()
		{
			return string.Format("PetFeedListMessage[ PetFeed x {0} ]", this.PetFeedList.Count);
		}
	}
}
