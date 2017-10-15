using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class AddTradeItem : Operation
	{
		public long ItemID { get; set; }

		public int Num { get; set; }

		public int DurationMin { get; set; }

		public int UnitPrice { get; set; }

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

		public override OperationProcessor RequestProcessor()
		{
			return new AddTradeItem.Request(this);
		}

		[NonSerialized]
		private TradeResultCode rc;

		private class Request : OperationProcessor<AddTradeItem>
		{
			public Request(AddTradeItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.RC = (TradeResultCode)base.Feedback;
				if (base.Operation.RC != TradeResultCode.Success)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
