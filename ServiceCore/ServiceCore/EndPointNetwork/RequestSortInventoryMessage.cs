using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestSortInventoryMessage : IMessage
	{
		public int StorageNo { get; set; }
	}
}
