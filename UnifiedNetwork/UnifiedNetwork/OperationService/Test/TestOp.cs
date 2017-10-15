using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.OperationService.Test
{
	[Serializable]
	internal sealed class TestOp : Operation
	{
		public int InValue
		{
			get
			{
				return this.inv;
			}
		}

		public string OutValue
		{
			get
			{
				return this.outv;
			}
		}

		public TestOp()
		{
		}

		public TestOp(int i)
		{
			this.inv = i;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new TestOp.Request(this);
		}

		private int inv;

		[NonSerialized]
		private string outv;

		private class Request : OperationProcessor<TestOp>
		{
			public Request(TestOp op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is string)
				{
					base.Operation.outv = (base.Feedback as string);
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
