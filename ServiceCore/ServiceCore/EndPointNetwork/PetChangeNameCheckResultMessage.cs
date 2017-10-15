using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetChangeNameCheckResultMessage : IMessage
	{
		public bool IsSuccess { get; set; }

		public long ItemID { get; set; }

		public long PetID { get; set; }

		public string PetName { get; set; }

		public string ResultType { get; set; }

		public PetChangeNameCheckResultMessage(bool isSuccess, long itemID, long petID, string petName, string resultType)
		{
			this.IsSuccess = isSuccess;
			this.ItemID = itemID;
			this.PetID = petID;
			this.PetName = petName;
			this.ResultType = resultType;
		}
	}
}
