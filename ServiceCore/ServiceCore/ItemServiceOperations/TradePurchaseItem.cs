using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TradePurchaseItem : Operation
	{
		public long TID { get; set; }

		public int PurchaseCount { get; set; }

		public TradeResultCode RC
		{
			get
			{
				return this.rc;
			}
			set
			{
				this.rc = value;
			}
		}

		public long LeftCount
		{
			get
			{
				return this.leftCount;
			}
			set
			{
				this.leftCount = value;
			}
		}

		public TradePurchaseItem(long tid, int purchaseCount)
		{
			this.TID = tid;
			this.PurchaseCount = purchaseCount;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new TradePurchaseItem.Request(this);
		}

		[NonSerialized]
		private TradeResultCode rc;

		[NonSerialized]
		private long leftCount;

		private class Request : OperationProcessor<TradePurchaseItem>
		{
			public Request(TradePurchaseItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.RC = (TradeResultCode)base.Feedback;
				yield return null;
				base.Operation.LeftCount = (long)base.Feedback;
				yield break;
			}
		}
	}
}
