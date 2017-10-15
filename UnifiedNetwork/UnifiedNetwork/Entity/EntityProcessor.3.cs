using System;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.Entity
{
	public abstract class EntityProcessor<TOperation, TEntity> : EntityProcessor<TOperation>, IEntityProcessor<TEntity> where TOperation : Operation where TEntity : class
	{
		public TEntity Entity
		{
			get
			{
				if (base.Connection == null || base.Connection.LocalEntity == null)
				{
					return default(TEntity);
				}
				return base.Connection.LocalEntity.GetTag<TEntity>();
			}
		}

		public EntityProcessor(TOperation op) : base(op)
		{
		}

		protected override void OnConnectionChanging(IEntityAdapter connection)
		{
			base.OnConnectionChanging(connection);
		}
	}
}
