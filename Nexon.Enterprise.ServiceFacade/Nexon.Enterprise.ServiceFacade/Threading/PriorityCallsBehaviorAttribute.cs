using System;

namespace Nexon.Enterprise.ServiceFacade.Threading
{
	[AttributeUsage(AttributeTargets.Class)]
	public class PriorityCallsBehaviorAttribute : ThreadPoolBehaviorAttribute
	{
		public PriorityCallsBehaviorAttribute(uint poolSize, Type serviceType) : this(poolSize, serviceType, null)
		{
		}

		public PriorityCallsBehaviorAttribute(uint poolSize, Type serviceType, string poolName) : base(poolSize, serviceType, poolName)
		{
		}

		protected override ThreadPoolSynchronizer ProvideSynchronizer()
		{
			if (!ThreadPoolHelper.HasSynchronizer(base.ServiceType))
			{
				return new PrioritySynchronizer(base.PoolSize, base.PoolName);
			}
			return ThreadPoolHelper.GetSynchronizer(base.ServiceType);
		}
	}
}
