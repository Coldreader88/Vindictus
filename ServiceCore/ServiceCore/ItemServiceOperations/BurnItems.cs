using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class BurnItems : Operation
	{
		public List<BurnItemInfo> BurnItemList { get; set; }

		public BurnItems(List<BurnItemInfo> burnItemList)
		{
			this.BurnItemList = burnItemList;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
