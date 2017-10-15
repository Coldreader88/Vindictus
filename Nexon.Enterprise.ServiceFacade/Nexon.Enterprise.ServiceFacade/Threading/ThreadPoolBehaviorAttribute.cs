using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Nexon.Enterprise.ServiceFacade.Threading
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ThreadPoolBehaviorAttribute : Attribute, IContractBehavior, IServiceBehavior
	{
		protected string PoolName
		{
			get
			{
				return this.m_PoolName;
			}
			set
			{
				this.m_PoolName = value;
			}
		}

		protected uint PoolSize
		{
			get
			{
				return this.m_PoolSize;
			}
			set
			{
				this.m_PoolSize = value;
			}
		}

		protected Type ServiceType
		{
			get
			{
				return this.m_ServiceType;
			}
			set
			{
				this.m_ServiceType = value;
			}
		}

		public ThreadPoolBehaviorAttribute(uint poolSize, Type serviceType) : this(poolSize, serviceType, null)
		{
		}

		public ThreadPoolBehaviorAttribute(uint poolSize, Type serviceType, string poolName)
		{
			this.PoolName = poolName;
			this.ServiceType = serviceType;
			this.PoolSize = poolSize;
		}

		protected virtual ThreadPoolSynchronizer ProvideSynchronizer()
		{
			if (!ThreadPoolHelper.HasSynchronizer(this.ServiceType))
			{
				return new ThreadPoolSynchronizer(this.PoolSize, this.PoolName);
			}
			return ThreadPoolHelper.GetSynchronizer(this.ServiceType);
		}

		void IContractBehavior.AddBindingParameters(ContractDescription description, ServiceEndpoint endpoint, BindingParameterCollection parameters)
		{
		}

		void IContractBehavior.ApplyClientBehavior(ContractDescription description, ServiceEndpoint endpoint, ClientRuntime proxy)
		{
		}

		void IContractBehavior.ApplyDispatchBehavior(ContractDescription description, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
			this.PoolName = (this.PoolName ?? ("Pool executing endpoints of " + this.ServiceType));
			ThreadPoolHelper.ApplyDispatchBehavior(this.ProvideSynchronizer(), this.PoolSize, this.ServiceType, this.PoolName, dispatchRuntime);
		}

		void IContractBehavior.Validate(ContractDescription description, ServiceEndpoint endpoint)
		{
		}

		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			serviceHostBase.Closed += delegate
			{
				ThreadPoolHelper.CloseThreads(this.ServiceType);
			};
		}

		private string m_PoolName;

		private uint m_PoolSize;

		private Type m_ServiceType;
	}
}
