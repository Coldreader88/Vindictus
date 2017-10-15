using System;
using System.Collections.Generic;
using Utility;

namespace UnifiedNetwork.Cooperation.Test
{
	internal class TestOpProcessor : OperationProcessor
	{
		public int Value { get; set; }

		public TestOpProcessor(int value, TestOp op) : base(op)
		{
			this.Value = value;
		}

		public override IEnumerable<object> Run()
		{
			yield return null;
			Log<TestOp>.Logger.DebugFormat("processor got value {0}", base.Feedback);
			base.Finished = true;
			yield return (int)base.Feedback + this.Value;
			yield break;
		}
	}
}
