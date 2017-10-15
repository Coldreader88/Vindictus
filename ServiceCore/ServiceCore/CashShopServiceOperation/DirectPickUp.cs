using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class DirectPickUp : Operation
	{
		public List<CashShopItem> QueryList { get; set; }

		public int TotalPrice { get; set; }

		public bool IsCredit { get; set; }

		public int OrderNo
		{
			get
			{
				return this.orderno;
			}
		}

		public List<int> ProductNoList
		{
			get
			{
				return this.productNos;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new DirectPickUp.Request(this);
		}

		[NonSerialized]
		private int orderno;

		[NonSerialized]
		private List<int> productNos;

		private class Request : OperationProcessor<DirectPickUp>
		{
			public Request(DirectPickUp op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.orderno = (int)base.Feedback;
					yield return null;
					base.Operation.productNos = (base.Feedback as List<int>);
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
