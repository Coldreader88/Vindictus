using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetKilledMessage : IMessage
	{
		public int Tag { get; set; }

		public long PetID { get; set; }

		public PetKilledMessage(int tag, long petID)
		{
			this.Tag = tag;
			this.PetID = petID;
		}
	}
}
