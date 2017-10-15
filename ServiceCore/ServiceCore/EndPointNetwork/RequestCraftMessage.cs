using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestCraftMessage : IMessage
	{
		public string ShopID { get; set; }

		public int Order { get; set; }

		public int Count { get; set; }

		public override string ToString()
		{
			return string.Format("RequestCraftMessage[ {0} {1} {2} ]", this.ShopID, this.Order, this.Count);
		}
	}
}
