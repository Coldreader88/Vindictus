using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryBuybackList : Operation
	{
		public QueryBuybackList(BuybackType buybackType)
		{
			this.type = buybackType;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		public BuybackType type;
	}
}
