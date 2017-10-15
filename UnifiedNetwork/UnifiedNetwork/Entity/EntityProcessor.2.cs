using System;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.Entity
{
	public abstract class EntityProcessor<TOperation> : EntityProcessor, IOperationProcessor<TOperation> where TOperation : Operation
	{
		public new TOperation Operation
		{
			get
			{
				return base.Operation as TOperation;
			}
		}

		public EntityProcessor(TOperation op) : base(op)
		{
		}

		protected override void OnConnectionChanging(IEntityAdapter connection)
		{
		}
	}
}
