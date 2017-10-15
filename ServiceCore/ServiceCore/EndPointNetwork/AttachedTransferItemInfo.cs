using System;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AttachedTransferItemInfo
	{
		public long ItemID { get; set; }

		public string ItemClass { get; set; }

		public int ItemCount { get; set; }

		public TransferItemInfo ToTransferItemInfo()
		{
			return new TransferItemInfo(this.ItemID, this.ItemClass, this.ItemCount);
		}
	}
}
