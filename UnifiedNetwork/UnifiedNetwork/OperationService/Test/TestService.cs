using System;
using Devcat.Core.Threading;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.OperationService.Test
{
	internal class TestService : Service
	{
		public TestService()
		{
			base.Initializing += this.OnInitializeHandler;
		}

		protected void OnInitializeHandler(object sender, EventArgs e)
		{
			base.ProcessorBuilder.Add(typeof(TestOp), (Operation op) => new TestOpProcessor(this, op as TestOp));
		}

		internal object MakeResult(int p)
		{
			return p.ToString();
		}

		public override void Initialize(JobProcessor thread)
		{
			throw new NotImplementedException();
		}
	}
}
