using System;

namespace UnifiedNetwork.Cooperation
{
	public abstract class OperationProcessor<TOperation> : OperationProcessor, IOperationProcessor<TOperation> where TOperation : Operation
	{
		public new TOperation Operation
		{
			get
			{
				return base.Operation as TOperation;
			}
		}

		public OperationProcessor(TOperation op) : base(op)
		{
		}
	}
}
