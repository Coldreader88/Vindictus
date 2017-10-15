using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingStartGrantedMessage : IMessage
	{
		public int NewSlot { get; set; }

		public int NewKey { get; set; }
	}
}
