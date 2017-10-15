using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetCreateNameCheckMessage : IMessage
	{
		public long ItemID { get; set; }

		public string PetName { get; set; }
	}
}
