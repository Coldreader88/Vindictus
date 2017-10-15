using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TradeInsertOrder : Operation
	{
		public long ItemID { get; set; }

		public int Num { get; set; }

		public int DurationMin { get; set; }

		public int UnitPrice { get; set; }

		public byte TradeType { get; set; }

		public int ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new TradeInsertOrder.Request(this);
		}

		[NonSerialized]
		private int resultCode = -1;

		private class Request : OperationProcessor<TradeInsertOrder>
		{
			public Request(TradeInsertOrder op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.resultCode = (int)base.Feedback;
				if (base.Operation.resultCode != 0)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
