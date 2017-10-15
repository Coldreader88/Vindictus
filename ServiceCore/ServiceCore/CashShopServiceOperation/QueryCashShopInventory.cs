using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class QueryCashShopInventory : Operation
	{
		public ICollection<CashShopInventoryElement> ItemList
		{
			get
			{
				return this.returnList;
			}
			set
			{
				this.returnList = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryCashShopInventory.Request(this);
		}

		[NonSerialized]
		private ICollection<CashShopInventoryElement> returnList;

		private class Request : OperationProcessor<QueryCashShopInventory>
		{
			public Request(QueryCashShopInventory op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				base.Operation.returnList = (base.Feedback as IList<CashShopInventoryElement>);
				if (base.Operation.returnList == null)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
