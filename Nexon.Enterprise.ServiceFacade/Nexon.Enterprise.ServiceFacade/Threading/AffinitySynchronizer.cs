using System;
using System.Security.Permissions;
using System.Threading;

namespace Nexon.Enterprise.ServiceFacade.Threading
{
	[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
	public class AffinitySynchronizer : ThreadPoolSynchronizer
	{
		public AffinitySynchronizer() : this("AffinitySynchronizer Worker Thread")
		{
		}

		public AffinitySynchronizer(string threadName) : base(1u, threadName)
		{
		}

		public override SynchronizationContext CreateCopy()
		{
			return this;
		}
	}
}
