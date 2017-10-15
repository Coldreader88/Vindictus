using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TradeCancelMyItem : Operation
	{
		public long TID { get; set; }

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

		public TradeCancelMyItem(long tid)
		{
			this.TID = tid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new TradeCancelMyItem.Request(this);
		}

		[NonSerialized]
		private TradeResultCode rc;

		private class Request : OperationProcessor<TradeCancelMyItem>
		{
			public Request(TradeCancelMyItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.RC = (TradeResultCode)base.Feedback;
				yield break;
			}
		}
	}
}
