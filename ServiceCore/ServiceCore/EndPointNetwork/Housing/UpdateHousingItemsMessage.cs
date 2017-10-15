using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class UpdateHousingItemsMessage : IMessage
	{
		public List<HousingItemInfo> ItemList { get; set; }

		public bool ClearInven { get; set; }

		public UpdateHousingItemsMessage(List<HousingItemInfo> itemList, bool clearInven)
		{
			this.ItemList = itemList;
			this.ClearInven = clearInven;
		}

		public override string ToString()
		{
			return string.Format("UpdateHousingItemsMessage [ x {0}]", this.ItemList.Count);
		}
	}
}
