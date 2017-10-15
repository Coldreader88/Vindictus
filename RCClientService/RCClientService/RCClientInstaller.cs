using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace RemoteControlSystem.Client
{
	[RunInstaller(true)]
	public class RCClientInstaller : Installer
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
			if (base.Context.Parameters.ContainsKey("service"))
			{
				serviceInstaller.ServiceName = base.Context.Parameters["service"];
			}
			else
			{
				serviceInstaller.ServiceName = BaseConfiguration.ClientServiceName;
			}
			serviceInstaller.DisplayName = BaseConfiguration.ClientServiceFullName;
			if (base.Context.Parameters.ContainsKey("display"))
			{
				serviceInstaller.DisplayName = base.Context.Parameters["display"];
			}
			else
			{
				serviceInstaller.DisplayName = BaseConfiguration.ClientServiceFullName;
			}
			if (base.Context.Parameters.ContainsKey("config"))
			{
				string text = base.Context.Parameters["config"];
				if (!Path.IsPathRooted(text))
				{
					text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), text);
				}
				StringDictionary parameters;
				(parameters = base.Context.Parameters)["assemblypath"] = parameters["assemblypath"] + "\" /service";
			}
			base.Installers.Add(serviceInstaller);
			base.Installers.Add(serviceProcessInstaller);
			base.OnBeforeInstall(savedState);
		}

		protected override void OnCommitted(IDictionary savedState)
		{
			base.OnCommitted(savedState);
			Assembly assembly = Assembly.LoadFile(Assembly.GetExecutingAssembly().Location);
			RegistrationServices registrationServices = new RegistrationServices();
			registrationServices.RegisterAssembly(assembly, AssemblyRegistrationFlags.SetCodeBase);
		}

		protected override void OnAfterUninstall(IDictionary savedState)
		{
			base.OnAfterUninstall(savedState);
			Assembly assembly = Assembly.LoadFile(Assembly.GetExecutingAssembly().Location);
			RegistrationServices registrationServices = new RegistrationServices();
			registrationServices.UnregisterAssembly(assembly);
		}
	}
}
