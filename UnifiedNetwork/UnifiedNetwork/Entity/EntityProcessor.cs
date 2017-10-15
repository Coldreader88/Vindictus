using System;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.Entity
{
	public abstract class EntityProcessor : OperationProcessor
	{
		public EntityProcessor(Operation op) : base(op)
		{
		}

		public IEntityAdapter Connection
		{
			get
			{
				return this.connection;
			}
			internal set
			{
				this.OnConnectionChanging(value);
				this.connection = value;
			}
		}

		protected abstract void OnConnectionChanging(IEntityAdapter connection);

		private IEntityAdapter connection;
	}
}
