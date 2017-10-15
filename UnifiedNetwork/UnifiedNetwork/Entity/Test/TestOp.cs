using System;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.Entity.Test
{
	[Serializable]
	internal sealed class TestOp : Operation, IResultReceiver<Box<long>>
	{
		public Box<long> ResultMessage
		{
			get
			{
				return this.resultMessage;
			}
			set
			{
				this.resultMessage = value;
			}
		}

		public int Param { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OneResultProcessor<TestOp, Box<long>>(this);
		}

		[NonSerialized]
		private Box<long> resultMessage;
	}
}
