using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TradeInsertTirOrder : Operation
	{
		public int Num { get; set; }

		public int DurationMin { get; set; }

		public int UnitPrice { get; set; }

		public int ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new TradeInsertTirOrder.Request(this);
		}

		[NonSerialized]
		private int resultCode = -1;

		private class Request : OperationProcessor<TradeInsertTirOrder>
		{
			public Request(TradeInsertTirOrder op) : base(op)
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
