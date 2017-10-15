using System;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace ServiceCore
{
	public abstract class ReadProcessor<TOperation, TEntity> : EntityProcessor<TOperation, TEntity> where TOperation : Operation where TEntity : class, IPermission
	{
		public ReadProcessor(TOperation op) : base(op)
		{
		}

		protected override void OnConnectionChanging(IEntityAdapter connection)
		{
			base.OnConnectionChanging(connection);
		}
	}
}
