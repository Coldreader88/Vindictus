using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using HeroesServerHost.Properties;
using Utility;

namespace HeroesServerHost
{
	public class HeroesServiceHost : ServiceHostBase
    {
		public override IEnumerable<Invoker> AppDomainInvokers
		{
			get
			{
				Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				string path = Path.GetDirectoryName(configuration.FilePath);
				Environment.CurrentDirectory = path;
				if (Settings.Default.ExecuteLocationService)
				{
					yield return this.MakeLocationServiceInvoker(path);
				}
				if (Settings.Default.ExecuteReportService)
				{
					yield return this.MakeReportServiceInvoker(path);
				}
				if (Settings.Default.ExecuteExecutionService)
				{
					yield return this.MakeExecutionServiceInvoker(path);
				}
				yield break;
			}
		}

		public override string DefaultServiceName
		{
			get
			{
				return "HeroesServer";
			}
		}

		private Invoker MakeLocationServiceInvoker(string path)
		{
			string binPath = string.Format("{0};{1}", Path.Combine(path, (IntPtr.Size == 4) ? "x86" : "x64"), path);
			return new Invoker
			{
				Assembly = Path.Combine(path, "UnifiedNetwork.dll"),
				Type = "UnifiedNetwork.LocationService.LocationService",
				Method = "StartService",
				BinPath = binPath,
				DomainName = "LocationService",
				Parameters = new object[]
				{
					Settings.Default.LocationServicePort
				},
				ConfigurationFile = "ServiceCore.dll.config"
			};
		}

		private Invoker MakeReportServiceInvoker(string path)
		{
			return new Invoker
			{
				Assembly = Path.Combine(path, "UnifiedNetwork.dll"),
				Type = "UnifiedNetwork.ReportService.ReportService",
				Method = "StartService",
				BinPath = path,
				DomainName = "ReportService",
				Parameters = new object[]
				{
					Settings.Default.LocationServiceIP,
					Settings.Default.LocationServicePort
				},
				ConfigurationFile = "ServiceCore.dll.config"
			};
		}

		private Invoker MakeExecutionServiceInvoker(string path)
		{
			string binPath = string.Format("{0};{1}", Path.Combine(path, (IntPtr.Size == 4) ? "x86" : "x64"), path);
			return new Invoker
			{
				Assembly = Path.Combine(path, "ExecutionServiceCore.dll"),
				Type = "ExecutionServiceCore.ExecutionService",
				Method = "StartService",
				BinPath = binPath,
				DomainName = "ExecutionService",
				Parameters = new object[]
				{
					Settings.Default.LocationServiceIP,
					Settings.Default.LocationServicePort
				},
				ConfigurationFile = "ServiceCore.dll.config"
			};
		}

		public static void Main(string[] args)
		{
			Environment.ExitCode = -1;
			Environment.SetEnvironmentVariable("logfile.name", "defaultLogFile");
			ServiceBase[] array = new ServiceBase[]
			{
				new HeroesServiceHost()
			};
			if (!Environment.UserInteractive && args.FirstOrDefault<string>() != "/Console")
			{
				ServiceBase.Run(array);
			}
			else
			{
				AutoResetEvent autoEvent = new AutoResetEvent(false);
				Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e)
				{
					autoEvent.Set();
					e.Cancel = true;
				};
				MethodInfo method = typeof(ServiceBase).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);
				object[] parameters = new object[]
				{
					args
				};
				foreach (ServiceBase obj in array)
				{
					method.Invoke(obj, parameters);
				}
				autoEvent.WaitOne();
				foreach (ServiceBase serviceBase in array)
				{
					serviceBase.Stop();
				}
			}
			Environment.ExitCode = 0;
		}
	}
}
