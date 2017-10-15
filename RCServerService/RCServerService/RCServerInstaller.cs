using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.ServiceProcess;

namespace RemoteControlSystem.Server
{
	[RunInstaller(true)]
	public class RCServerInstaller : Installer
	{
		protected override void OnBeforeInstall(IDictionary savedState)
		{
			ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
			if (!base.Context.Parameters.ContainsKey("account"))
			{
				serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			}
			else
			{
				serviceProcessInstaller.Account = ServiceAccount.User;
				serviceProcessInstaller.Username = base.Context.Parameters["account"];
				if (base.Context.Parameters.ContainsKey("password"))
				{
					serviceProcessInstaller.Password = base.Context.Parameters["password"];
				}
			}
			ServiceInstaller serviceInstaller = new ServiceInstaller();
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceInstaller.Installers.Clear();
			EventLogInstaller eventLogInstaller = new EventLogInstaller();
			if (base.Context.Parameters.ContainsKey("service"))
			{
				serviceInstaller.ServiceName = base.Context.Parameters["service"];
				eventLogInstaller.Source = base.Context.Parameters["service"];
				eventLogInstaller.Log = base.Context.Parameters["service"] + "Log";
			}
			else
			{
				serviceInstaller.ServiceName = BaseConfiguration.ServerServiceName;
				eventLogInstaller.Source = BaseConfiguration.ServerServiceName;
				eventLogInstaller.Log = BaseConfiguration.ServerServiceName + "Log";
			}
			if (base.Context.Parameters.ContainsKey("display"))
			{
				serviceInstaller.DisplayName = base.Context.Parameters["display"];
			}
			else
			{
				serviceInstaller.DisplayName = BaseConfiguration.ServerServiceFullName;
			}
			base.Installers.Add(serviceInstaller);
			base.Installers.Add(serviceProcessInstaller);
			base.Installers.Add(eventLogInstaller);
			base.OnBeforeInstall(savedState);
		}
	}
}
