using System;

namespace UnifiedNetwork.Entity
{
	public interface IEntityProcessor<TEntity> where TEntity : class
	{
		TEntity Entity { get; }
	}
}
