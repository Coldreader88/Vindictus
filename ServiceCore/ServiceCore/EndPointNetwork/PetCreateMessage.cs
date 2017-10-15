using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetCreateMessage : IMessage
	{
		public long ItemID { get; set; }

		public string PetName { get; set; }
	}
}
