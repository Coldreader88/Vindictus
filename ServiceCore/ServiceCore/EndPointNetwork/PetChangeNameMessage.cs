using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetChangeNameMessage : IMessage
	{
		public long ItemID { get; set; }

		public long PetID { get; set; }

		public string PetName { get; set; }
	}
}
