using System;

namespace Nexon.Enterprise.ServiceFacade.Threading
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ThreadAffinityBehaviorAttribute : ThreadPoolBehaviorAttribute
	{
		public ThreadAffinityBehaviorAttribute(Type serviceType) : this(serviceType, "Affinity Worker Thread")
		{
		}

		public ThreadAffinityBehaviorAttribute(Type serviceType, string threadName) : base(1u, serviceType, threadName)
		{
		}
	}
}
