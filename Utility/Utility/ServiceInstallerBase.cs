using System;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Utility
{
	public abstract class ServiceInstallerBase : Installer
	{
		protected ServiceInstallerBase(string serviceName)
		{
			this.installer.ServiceName = serviceName;
			this.installer.StartType = ServiceStartMode.Automatic;
			this.processInstaller.Account = ServiceAccount.User;
			base.Installers.Add(this.installer);
			base.Installers.Add(this.processInstaller);
		}

		private ServiceInstaller installer = new ServiceInstaller();

		private ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
	}
}
