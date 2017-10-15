using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetChangeNameCheckMessage : IMessage
	{
		public long ItemID { get; set; }

		public long PetID { get; set; }

		public string PetName { get; set; }
	}
}
