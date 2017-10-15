using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetFeedElement
	{
		public PetFeedElement(byte type, int count)
		{
			this.Type = type;
			this.Count = count;
		}

		public byte Type { get; set; }

		public int Count { get; set; }
	}
}
