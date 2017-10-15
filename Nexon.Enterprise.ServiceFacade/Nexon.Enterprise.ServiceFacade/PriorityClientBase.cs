using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Nexon.Enterprise.ServiceFacade.Threading;

namespace Nexon.Enterprise.ServiceFacade
{
	public abstract class PriorityClientBase<T> : HeaderClientBase<T, CallPriority> where T : class
	{
		public PriorityClientBase() : this(PrioritySynchronizer.Priority)
		{
		}

		public PriorityClientBase(string endpointConfigurationName) : this(PrioritySynchronizer.Priority, endpointConfigurationName)
		{
		}

		public PriorityClientBase(string endpointConfigurationName, string remoteAddress) : this(PrioritySynchronizer.Priority, endpointConfigurationName, remoteAddress)
		{
		}

		public PriorityClientBase(string endpointConfigurationName, EndpointAddress remoteAddress) : this(PrioritySynchronizer.Priority, endpointConfigurationName, remoteAddress)
		{
		}

		public PriorityClientBase(Binding binding, EndpointAddress remoteAddress) : this(PrioritySynchronizer.Priority, binding, remoteAddress)
		{
		}

		public PriorityClientBase(CallPriority priority) : base(priority)
		{
		}

		public PriorityClientBase(CallPriority priority, string endpointConfigurationName) : base(priority, endpointConfigurationName)
		{
		}

		public PriorityClientBase(CallPriority priority, string endpointConfigurationName, string remoteAddress) : base(priority, endpointConfigurationName, remoteAddress)
		{
		}

		public PriorityClientBase(CallPriority priority, string endpointConfigurationName, EndpointAddress remoteAddress) : base(priority, endpointConfigurationName, remoteAddress)
		{
		}

		public PriorityClientBase(CallPriority priority, Binding binding, EndpointAddress remoteAddress) : base(priority, binding, remoteAddress)
		{
		}
	}
}
