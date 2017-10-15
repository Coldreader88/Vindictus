using System;
using System.Collections.Generic;

namespace UnifiedNetwork.Cooperation
{
	public abstract class ReturnOkProcessor : OperationProcessor
	{
		public ReturnOkProcessor(Operation op) : base(op)
		{
		}

		public abstract bool Process();

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (this.Process())
			{
				yield return new OkMessage();
			}
			else
			{
				yield return new FailMessage("[ReturnOkProcessor] Run")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			yield break;
		}
	}
}
