using System;
using System.Collections.Generic;
using System.ServiceProcess;

namespace Utility
{
	public abstract class ServiceHostBase : ServiceBase
	{
		public abstract string DefaultServiceName { get; }

		public abstract IEnumerable<Invoker> AppDomainInvokers { get; }

		public ServiceHostBase()
		{
			base.ServiceName = this.DefaultServiceName;
			Log<ServiceHostBase>.Logger.Info(base.ServiceName);
		}

		protected override void OnStart(string[] args)
		{
			foreach (Invoker invoker in this.AppDomainInvokers)
			{
				try
				{
					this.stack.Push(invoker.Execute());
				}
				catch (Exception ex)
				{
					Log<ServiceHostBase>.Logger.Error(string.Format("OnStart : {0}", base.ServiceName), ex);
				}
			}
			base.ExitCode = 0;
		}

		protected override void OnStop()
		{
			Log<ServiceHostBase>.Logger.InfoFormat("OnStop : {0}", base.ServiceName);
			while (0 < this.stack.Count)
			{
				AppDomain.Unload(this.stack.Pop());
			}
			GC.Collect();
			GC.WaitForPendingFinalizers();
			base.RequestAdditionalTime(4000);
			base.ExitCode = 0;
		}

		protected override void OnShutdown()
		{
			this.OnStop();
		}

		protected static void Run(ServiceHostBase service)
		{
			ServiceBase.Run(service);
		}

		private Stack<AppDomain> stack = new Stack<AppDomain>();
	}
}
