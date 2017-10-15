using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MarbleGetItemMessage : IMessage
	{
		public string ItemClassEx { get; set; }

		public int ItemCount { get; set; }

		public string Type { get; set; }

		public MarbleGetItemMessage(string itemClassEx, int itemCount, string type)
		{
			this.ItemClassEx = itemClassEx;
			this.ItemCount = itemCount;
			this.Type = type;
		}
	}
}
