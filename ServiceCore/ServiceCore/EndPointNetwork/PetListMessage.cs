using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetListMessage : IMessage
	{
		public ICollection<PetStatusInfo> PetList { get; set; }

		public bool IsTotalPetList { get; set; }

		public PetListMessage(ICollection<PetStatusInfo> pet, bool isTotal)
		{
			this.PetList = pet;
			this.IsTotalPetList = isTotal;
		}

		public override string ToString()
		{
			return string.Format("PetListMessage[ Pet x {0} ]", this.PetList.Count);
		}
	}
}
