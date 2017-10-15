using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.OperationService.Test
{
	internal class TestOpProcessor : OperationProcessor<TestOp>
	{
		public TestOpProcessor(TestService service, TestOp op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			yield return this.service.MakeResult(base.Operation.InValue);
			yield break;
		}

		private TestService service;
	}
}
