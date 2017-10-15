using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestPetFoodShareMessage : IMessage
	{
		public long ItemID { get; set; }

		public RequestPetFoodShareMessage(long itemID)
		{
			this.ItemID = itemID;
		}

		public override string ToString()
		{
			return string.Format("RequestPetFoodShareMessage[ itemID = {0} ]", this.ItemID);
		}
	}
}
