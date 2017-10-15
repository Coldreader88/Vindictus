using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BuyItemMessage : IMessage
	{
		public string ShopID { get; set; }

		public int Order { get; set; }

		public int Count { get; set; }

		public override string ToString()
		{
			return string.Format("BuyItemMessage [ {0} {1} {2} ]", this.ShopID, this.Order, this.Count);
		}
	}
}
