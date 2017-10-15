using System;

namespace UnifiedNetwork.Cooperation
{
	public interface IOperationProcessor<TOperation> where TOperation : Operation
	{
		TOperation Operation { get; }
	}
}
