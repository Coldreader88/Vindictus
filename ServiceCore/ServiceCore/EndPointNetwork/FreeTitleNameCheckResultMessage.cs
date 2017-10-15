using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class FreeTitleNameCheckResultMessage : IMessage
	{
		public long ItemID { get; set; }

		public string FreeTitleName { get; set; }

		public bool IsSuccess { get; set; }

		public bool HasFreeTitle { get; set; }

		public FreeTitleNameCheckResultMessage(long itemID, string freeTitleName, bool isSuccess, bool hasFreeTitle)
		{
			this.ItemID = itemID;
			this.FreeTitleName = freeTitleName;
			this.IsSuccess = isSuccess;
			this.HasFreeTitle = hasFreeTitle;
		}
	}
}
