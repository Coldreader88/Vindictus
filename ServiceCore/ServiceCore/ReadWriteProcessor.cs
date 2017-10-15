using System;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace ServiceCore
{
	public abstract class ReadWriteProcessor<TOperation, TEntity> : EntityProcessor<TOperation, TEntity> where TOperation : Operation where TEntity : class, IPermission
	{
		public ReadWriteProcessor(TOperation op) : base(op)
		{
		}

		protected override void OnConnectionChanging(IEntityAdapter connection)
		{
			base.OnConnectionChanging(connection);
		}
	}
}
