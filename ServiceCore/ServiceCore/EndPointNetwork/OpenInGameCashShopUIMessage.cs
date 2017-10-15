using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class OpenInGameCashShopUIMessage : IMessage
	{
		public long ItemID { get; set; }

		public override string ToString()
		{
			return string.Format("OpenInGameCashShopUIMessage[ itemID = {0}]", this.ItemID);
		}
	}
}
