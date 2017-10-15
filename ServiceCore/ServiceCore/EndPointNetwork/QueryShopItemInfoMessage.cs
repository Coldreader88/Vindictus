using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryShopItemInfoMessage : IMessage
	{
		public string ShopID { get; set; }

		public override string ToString()
		{
			return string.Format("QueryShopItemInfoMessage[ result = {0} ]", this.ShopID);
		}
	}
}
