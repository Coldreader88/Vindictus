using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateCurrentPetMessage : IMessage
	{
		public int UID { get; set; }

		public PetStatusInfo CurrentPet { get; set; }

		public UpdateCurrentPetMessage(int uid, PetStatusInfo currentPet)
		{
			this.UID = uid;
			this.CurrentPet = currentPet;
		}

		public override string ToString()
		{
			if (this.CurrentPet == null)
			{
				return string.Format("UpdateCurrentPetMessage [CurrentPet is NULL]", new object[0]);
			}
			return string.Format("UpdateCurrentPetMessage [PetID: {0}, Name: {1}]", this.CurrentPet.PetID, this.CurrentPet.PetName);
		}
	}
}
