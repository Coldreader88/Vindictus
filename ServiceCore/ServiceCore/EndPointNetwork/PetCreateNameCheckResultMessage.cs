using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetCreateNameCheckResultMessage : IMessage
	{
		public long ItemID { get; set; }

		public string PetName { get; set; }

		public bool IsSuccess { get; set; }

		public PetCreateNameCheckResultMessage(long itemID, string petName, bool isSuccess)
		{
			this.ItemID = itemID;
			this.PetName = petName;
			this.IsSuccess = isSuccess;
		}
	}
}
