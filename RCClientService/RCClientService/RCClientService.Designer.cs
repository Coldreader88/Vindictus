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

namespace RemoteControlSystem.Client
{
	public class RCClientService : ServiceBase
	{
		public bool IsElevated
		{
			get
			{
				return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
			}
		}

		public RCClientService()
		{
			this.InitializeComponent();
			base.ServiceName = BaseConfiguration.ClientServiceName;
		}

		private static void PrintHelp()
		{
			Console.WriteLine(BaseConfiguration.ClientServiceFullName);
			Console.WriteLine("{0} [-install|-uninstall]", Path.GetFileName(Assembly.GetExecutingAssembly().Location));
			Console.WriteLine("{0} -install [service:<serviceName>] [display:<displayName>] [account:<account>] [password:<password>] [config:<configFile>]", Path.GetFileName(Assembly.GetExecutingAssembly().Location));
		}

		[MTAThread]
		private static void Main(string[] Args)
		{
			RCClientService rcclientService = new RCClientService();
			if (Args.Length == 0)
			{
				RCClientService.PrintHelp();
			}
			string a;
			if (Args.Length != 0 && (a = Args[0].ToLower()) != null)
			{
				if (!(a == "-install"))
				{
					if (!(a == "-uninstall"))
					{
						if (!(a == "-embedding"))
						{
							if (a == "/?" || a == "-help")
							{
								RCClientService.PrintHelp();
								Environment.Exit(0);
							}
						}
					}
					else
					{
						Environment.Exit(rcclientService.InstallService(false, null));
					}
				}
				else
				{
					Environment.Exit(rcclientService.InstallService(true, Args.Skip(1).ToArray<string>()));
				}
			}
			if (Args.Length > 0)
			{
				BaseConfiguration.ClientConfigFile = Args[0];
			}
			Thread.CurrentThread.Name = "MainThread";
			ServiceBase.Run(rcclientService);
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

		protected override void OnStart(string[] Args)
		{
			Thread.CurrentThread.Name = "ServiceThread";
			if (!Directory.Exists(BaseConfiguration.WorkingDirectory))
			{
				try
				{
					Directory.CreateDirectory(BaseConfiguration.WorkingDirectory);
				}
				catch (Exception)
				{
					throw new IOException("Cannot create Working Directory \"" + BaseConfiguration.WorkingDirectory + "\"");
				}
			}
			Base.StartUp();
			try
			{
				Base.ClientControlManager.OnClientRegistered += this.Initialized;
				Base.ClientControlManager.Start(Base.Client);
				Base.ClientControlClient.Start(Base.ClientControlManager.Thread);
				Base.PingSender.Start();
			}
			catch
			{
				Base.CleanUp();
				throw;
			}
			base.OnStart(Args);
			try
			{
				ComObject.Regist();
			}
			catch (Exception ex)
			{
				Log<RCClientService>.Logger.ErrorFormat("Fail  to register com object.", ex);
			}
		}

		private void Initialized(object sender, EventArgs args)
		{
			Base.ClientControlManager.OnClientRegistered -= this.Initialized;
			Base.Client.StartAutomaticStartProcess();
		}

		protected override void OnStop()
		{
			Base.CleanUp();
			GC.Collect();
			base.OnStop();
			try
			{
				ComObject.Unregist();
			}
			catch (Exception ex)
			{
				Log<RCClientService>.Logger.ErrorFormat("Fail  to unregister com object.", ex);
			}
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
			if (args != null)
			{
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
					else if (text.StartsWith("config:"))
					{
						assemblyInstaller.Context.Parameters.Add("config", text.Substring(7).Trim());
					}
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

		private Container components =null;
	}
}
