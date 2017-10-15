using System;
using System.Collections.Generic;

namespace UnifiedNetwork.Cooperation
{
	public class OneResultProcessor<TOperation, MessageType> : OperationProcessor<TOperation> where TOperation : Operation, IResultReceiver<MessageType> where MessageType : class
	{
		public MessageType ResultMessage { get; private set; }

		public OneResultProcessor(TOperation op) : base(op)
		{
		}

		public override IEnumerable<object> Run()
		{
			yield return null;
			this.ResultMessage = (base.Feedback as MessageType);
			if (this.ResultMessage == null)
			{
				base.Result = false;
			}
			IResultReceiver<MessageType> receiver = base.Operation;
			receiver.ResultMessage = this.ResultMessage;
			yield break;
		}
	}
}
