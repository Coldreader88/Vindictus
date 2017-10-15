using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BuybackListResultMessage : IMessage
	{
		public BuybackListResultMessage(ICollection<BuybackInfo> list)
		{
			this.BuybackList = list;
		}

		public override string ToString()
		{
			return string.Format("QueryBuybackListResultMessage Count = {0}", this.BuybackList.Count);
		}

		public ICollection<BuybackInfo> BuybackList;
	}
}
