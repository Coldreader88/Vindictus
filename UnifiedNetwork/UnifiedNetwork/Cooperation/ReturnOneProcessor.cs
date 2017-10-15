using System;
using System.Collections.Generic;

namespace UnifiedNetwork.Cooperation
{
	public abstract class ReturnOneProcessor<TOperation, MessageType> : OperationProcessor<TOperation> where TOperation : Operation, IResultReceiver<MessageType> where MessageType : class
	{
		public ReturnOneProcessor(TOperation op) : base(op)
		{
		}

		public abstract MessageType Process();

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			yield return this.Process();
			yield break;
		}
	}
}
