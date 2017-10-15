using System;
using System.Collections.Generic;
using Utility;

namespace UnifiedNetwork.Cooperation.Test
{
	internal class TestOp : Operation
	{
		public int Value { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new TestOp.Request(this);
		}

		private class Request : OperationProcessor<TestOp>
		{
			public Request(TestOp op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return base.Operation.Value;
				Log<TestOp>.Logger.DebugFormat("request got value {0}", base.Feedback);
				base.Operation.Value = (int)base.Feedback;
				yield break;
			}
		}
	}
}
