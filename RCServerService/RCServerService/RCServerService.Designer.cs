using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;

namespace RemoteControlSystem.Server
{
	public class RCServerService : ServiceBase
	{
		public bool IsElevated
		{
			get
			{
				return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
			}
		}

		public RCServerService()
		{
			this.InitializeComponent();
			base.ServiceName = BaseConfiguration.ServerServiceName;
		}

		private static void PrintHelp()
		{
			Console.WriteLine(BaseConfiguration.ServerServiceFullName);
			Console.WriteLine("{0} [-install|-uninstall]", Path.GetFileName(Assembly.GetExecutingAssembly().Location));
			Console.WriteLine("{0} -install [service:<serviceName>] [display:<displayName>] [account:<account>] [password:<password>]", Path.GetFileName(Assembly.GetExecutingAssembly().Location));
		}

		private static void Main(string[] Args)
		{
			RCServerService rcserverService = new RCServerService();
			if (Args.Length == 0)
			{
				RCServerService.PrintHelp();
			}
			string a;
			if (Args.Length != 0 && (a = Args[0].ToLower()) != null)
			{
				if (!(a == "-install"))
				{
					if (!(a == "-uninstall"))
					{
						if (a == "/?" || a == "-help")
						{
							RCServerService.PrintHelp();
							Environment.Exit(0);
						}
					}
					else
					{
						Environment.Exit(rcserverService.InstallService(false, Args.Skip(1).ToArray<string>()));
					}
				}
				else
				{
					Environment.Exit(rcserverService.InstallService(true, Args.Skip(1).ToArray<string>()));
				}
			}
			Thread.CurrentThread.Name = "MainThread";
			ServiceBase.Run(rcserverService);
		}

		private void InitializeComponent()
		{
			base.CanShutdown = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		protected override void OnStart(string[] args)
		{
			Base.StartUp();
			try
			{
				try
				{
					Base.GarbageCollector.Start();
				}
				catch
				{
					Log<RCServerService>.Logger.Error("Cannot start Auto garbage collector");
					throw;
				}
				try
				{
					Base.ClientServer.Start(Base.ServerClientPort);
				}
				catch
				{
					Log<RCServerService>.Logger.ErrorFormat("Cannot start RC Clients' Server on Port {0}", Base.ServerClientPort);
					throw;
				}
				try
				{
					Base.ControlServer.Start(Base.ServerControlPort);
				}
				catch
				{
					Log<RCServerService>.Logger.ErrorFormat("Cannot start RCS control server on Port {0} ", Base.ServerControlPort);
					throw;
				}
				try
				{
					Base.PingSender.Start();
				}
				catch
				{
					Log<RCServerService>.Logger.Error("Cannot start Ping sender");
					throw;
				}
			}
			catch
			{
				Base.CleanUp();
				throw;
			}
		}

		protected override void OnStop()
		{
			Base.SaveConfig(BaseConfiguration.WorkingDirectory + "\\" + BaseConfiguration.ServerConfigFile);
			Base.CleanUp();
			GC.Collect();
		}

		protected override void OnShutdown()
		{
			this.OnStop();
		}

		private int InstallService(bool bInstall, string[] args)
		{
			if (!this.IsElevated)
			{
				Console.WriteLine("Run as Administrator to {0} service", bInstall ? "true" : "false");
				return -1;
			}
			IDictionary dictionary = new Hashtable();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			AssemblyInstaller assemblyInstaller = new AssemblyInstaller(executingAssembly, null);
			assemblyInstaller.Context = new InstallContext();
			assemblyInstaller.Context.Parameters.Add("assemblypath", executingAssembly.Location);
			foreach (string text in args)
			{
				if (text.StartsWith("service:"))
				{
					assemblyInstaller.Context.Parameters.Add("service", text.Substring(8).Trim());
				}
				if (text.StartsWith("display:"))
				{
					assemblyInstaller.Context.Parameters.Add("display", text.Substring(8).Trim());
				}
				else if (text.StartsWith("account:"))
				{
					assemblyInstaller.Context.Parameters.Add("account", text.Substring(8).Trim());
				}
				else if (text.StartsWith("password:"))
				{
					assemblyInstaller.Context.Parameters.Add("password", text.Substring(9).Trim());
				}
			}
			int result;
			try
			{
				assemblyInstaller.UseNewContext = false;
				if (bInstall)
				{
					assemblyInstaller.Install(dictionary);
					assemblyInstaller.Commit(dictionary);
				}
				else
				{
					assemblyInstaller.Uninstall(dictionary);
				}
				result = 0;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				try
				{
					assemblyInstaller.Rollback(dictionary);
				}
				catch
				{
				}
				result = -1;
			}
			return result;
		}

		private Container components = null;
	}
}
