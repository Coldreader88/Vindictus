using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Nexon.Enterprise.ServiceFacade
{
	public abstract class HeaderClientBase<T, H> : ClientBase<T> where T : class
	{
		public HeaderClientBase() : this(default(H))
		{
		}

		public HeaderClientBase(string endpointConfigurationName) : this(default(H), endpointConfigurationName)
		{
		}

		public HeaderClientBase(string endpointConfigurationName, string remoteAddress) : this(default(H), endpointConfigurationName, remoteAddress)
		{
		}

		public HeaderClientBase(string endpointConfigurationName, EndpointAddress remoteAddress) : this(default(H), endpointConfigurationName, remoteAddress)
		{
		}

		public HeaderClientBase(Binding binding, EndpointAddress remoteAddress) : this(default(H), binding, remoteAddress)
		{
		}

		public HeaderClientBase(H header)
		{
			this.Header = header;
		}

		public HeaderClientBase(H header, string endpointConfigurationName) : base(endpointConfigurationName)
		{
			this.Header = header;
		}

		public HeaderClientBase(H header, string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
			this.Header = header;
		}

		public HeaderClientBase(H header, string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
			this.Header = header;
		}

		public HeaderClientBase(H header, Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
			this.Header = header;
		}

		protected virtual object Invoke(string operation, params object[] args)
		{
			object result;
			using (new OperationContextScope(base.InnerChannel))
			{
				GenericContext<H>.Current = new GenericContext<H>(this.Header);
				Type typeFromHandle = typeof(T);
				MethodInfo method = typeFromHandle.GetMethod(operation);
				result = method.Invoke(base.Channel, args);
			}
			return result;
		}

		protected H Header;
	}
}
