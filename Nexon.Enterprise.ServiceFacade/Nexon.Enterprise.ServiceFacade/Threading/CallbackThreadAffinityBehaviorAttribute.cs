using System;

namespace Nexon.Enterprise.ServiceFacade.Threading
{
	[AttributeUsage(AttributeTargets.Class)]
	public class CallbackThreadAffinityBehaviorAttribute : CallbackThreadPoolBehaviorAttribute
	{
		public CallbackThreadAffinityBehaviorAttribute(Type clientType) : this(clientType, "Callback Worker Thread")
		{
		}

		public CallbackThreadAffinityBehaviorAttribute(Type clientType, string threadName) : base(1u, clientType, threadName)
		{
		}
	}
}
