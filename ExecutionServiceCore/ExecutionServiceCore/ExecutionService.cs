using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Devcat.Core.Threading;
using ExecutionServiceCore.Properties;
using mscoree;
using ServiceCore;
using ServiceCore.Configuration;
using ServiceCore.ExecutionServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.OperationService;
using Utility;

namespace ExecutionServiceCore
{
	public class ExecutionService : Service
	{
		private static IEnumerable<AppDomain> EnumDomains()
		{
			CorRuntimeHostClass hostClass = new CorRuntimeHostClass();
			try
			{
				IntPtr hEnum;
				hostClass.EnumDomains(out hEnum);
				try
				{
					for (;;)
					{
						object domain;
						hostClass.NextDomain(hEnum, out domain);
						if (domain == null)
						{
							break;
						}
						yield return domain as AppDomain;
					}
				}
				finally
				{
					hostClass.CloseEnum(hEnum);
				}
			}
			finally
			{
				Marshal.ReleaseComObject(hostClass);
			}
			yield break;
		}

		public ExecutionService(string addr, string port)
		{
			this.parameters = new object[]
			{
				addr,
				port
			};
			this.serviceAvailability = ExecutionService.Domain.Do(this.parameters);
		}

		private Configuration Configuration
		{
			get
			{
				Configuration result;
				try
				{
					Assembly assembly = typeof(ExecutionService).Assembly;
					Configuration configuration = ConfigurationManager.OpenExeConfiguration(assembly.Location);
					result = configuration;
				}
				catch (ConfigurationErrorsException)
				{
					result = null;
				}
				return result;
			}
		}

		public override void Initialize(JobProcessor thread)
		{
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			base.Initialize(thread, ExecutionServiceOperations.TypeConverters);
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterProcessor(typeof(StartService), (Operation op) => new StartServiceProcessor(this, op as StartService));
			base.RegisterProcessor(typeof(QueryService), (Operation op) => new QueryServiceProcessor(this, op as QueryService));
			base.RegisterProcessor(typeof(ExecAppDomain), (Operation op) => new ExecAppDomainProcessor(this, op as ExecAppDomain));
			LocalServiceConfig localServiceConfig = this.Configuration.GetSection("LocalServiceConfig") as LocalServiceConfig;
			foreach (object obj in localServiceConfig.ServiceInfos)
			{
				LocalServiceConfig.Service service = (LocalServiceConfig.Service)obj;
				if (service.AutoStart)
				{
					Scheduler.Schedule(base.Thread, Job.Create<string>(delegate(string serviceClass)
					{
						Log<ExecutionService>.Logger.DebugFormat("<Initialize : [{0}]> auto start...", serviceClass);
						string text;
						this.StartAppDomain(serviceClass, out text);
						Log<ExecutionService>.Logger.DebugFormat("<Initialize : [{0}]> auto start invoked : {1}", serviceClass, text);
					}, service.ServiceClass), 0);
				}
			}
			base.Disposed += delegate(object sender, EventArgs e)
			{
				foreach (string domainName in new List<string>(this.DomainList))
				{
					this.StopAppDomain(domainName);
				}
			};
		}

		public IEnumerable<string> Services
		{
			get
			{
				return this.serviceAvailability.Keys;
			}
		}

		public IEnumerable<string> DomainList
		{
			get
			{
				return from domain in ExecutionService.EnumDomains()
				where this.appDomains.ContainsKey(domain.FriendlyName)
				select domain.FriendlyName;
			}
		}

		public bool StartService(string serviceClass, out string message)
		{
			return this.StartAppDomain(serviceClass, out message);
		}

		public bool StartAppDomain(string serviceClass, out string message)
		{
			message = "";
			Invoker invoker;
			if (!this.serviceAvailability.TryGetValue(serviceClass, out invoker))
			{
				Log<ExecutionService>.Logger.DebugFormat("<StartAppDomain> [{0}] 도메인은 시작 대상이 아닙니다.", serviceClass);
				return false;
			}
			int num = 0;
			string text = serviceClass;
			while (this.appDomains.ContainsKey(text))
			{
				text = string.Format("{0}[{1}]", serviceClass, ++num);
			}
			Log<ExecutionService>.Logger.DebugFormat("<StartAppDomain> [{0}] 도메인 시작", serviceClass);
			invoker.DomainName = text;
			try
			{
				AppDomain appDomain = invoker.Execute();
				this.appDomains.Add(appDomain.FriendlyName, appDomain);
			}
			catch (Exception ex)
			{
				Log<ExecutionService>.Logger.Fatal(string.Format("StartAppDomain[serviceClass = {0}]", serviceClass), ex);
				return false;
			}
			message = serviceClass;
			return true;
		}

		public bool StopAppDomain(string domainName)
		{
			Log<ExecutionService>.Logger.DebugFormat("<StopAppDomain> [{0}] 도메인 중지", domainName);
			AppDomain domain;
			if (this.appDomains.TryGetValue(domainName, out domain))
			{
				try
				{
					AppDomain.Unload(domain);
				}
				catch (CannotUnloadAppDomainException ex)
				{
					Log<ExecutionService>.Logger.Error(string.Format("StopAppDomain[domainName = {0}]", domainName), ex);
					return false;
				}
				this.appDomains.Remove(domainName);
				return true;
			}
			Log<ExecutionService>.Logger.DebugFormat("<StopAppDomain> [{0}] 도메인은 중지 대상이 아닙니다.", domainName);
			return false;
		}

		public static void StartService(string ip, string portstr)
		{
			ServiceInvoker.StartService(ip, portstr, new ExecutionService(ip, portstr));
		}

		private object[] parameters;

		private IDictionary<string, Invoker> serviceAvailability = new Dictionary<string, Invoker>();

		private IDictionary<string, AppDomain> appDomains = new Dictionary<string, AppDomain>();

		private class Domain : IDisposable
		{
			private Domain()
			{
				AppDomainSetup info = new AppDomainSetup
				{
					ApplicationName = typeof(ExecutionService.Domain).Name,
					PrivateBinPath = ExecutionService.Domain.privateBinPath
				};
				this.domain = AppDomain.CreateDomain(typeof(ExecutionService.Domain).FullName, AppDomain.CurrentDomain.Evidence, info);
				this.domain.UnhandledException += ExecutionService.Domain.domain_UnhandledException;
			}

			public static IDictionary<string, Invoker> Do(object[] parameters)
			{
				IDictionary<string, Invoker> result;
				using (ExecutionService.Domain domain = new ExecutionService.Domain())
				{
					domain.domain.SetData("param", parameters);
					domain.domain.DoCallBack(new CrossAppDomainDelegate(ExecutionService.Domain.CallBack));
					result = (domain.domain.GetData("returns") as IDictionary<string, Invoker>);
				}
				return result;
			}

			private static void domain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
			{
				Log<ExecutionService.Domain>.Logger.Fatal(sender, e.ExceptionObject as Exception);
			}

			private static void CallBack()
			{
				object[] parameters = AppDomain.CurrentDomain.GetData("param") as object[];
				Dictionary<string, Invoker> dictionary = new Dictionary<string, Invoker>();
				foreach (string assemblyFile in Directory.GetFiles(Environment.CurrentDirectory, "*.dll"))
				{
					try
					{
						Assembly assembly = Assembly.LoadFrom(assemblyFile);
						foreach (Type type in assembly.GetTypes())
						{
							try
							{
								MethodInfo method = type.GetMethod("StartService", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Standard, new Type[]
								{
									typeof(string),
									typeof(string)
								}, null);
								if (!(method == null))
								{
									Invoker value = new Invoker
									{
										Assembly = assembly.Location,
										Type = type.FullName,
										Method = method.Name,
										AppName = type.Name,
										BinPath = ExecutionService.Domain.privateBinPath,
										Parameters = parameters,
										ConfigurationFile = "ServiceCore.dll.config"
									};
									dictionary.Add(type.Name, value);
								}
							}
							catch (AmbiguousMatchException)
							{
							}
						}
					}
					catch
					{
					}
				}
				AppDomain.CurrentDomain.SetData("returns", dictionary);
			}

			~Domain()
			{
				this.Dispose(false);
			}

			public void Dispose()
			{
				this.Dispose(true);
			}

			private void Dispose(bool disposing)
			{
				if (this.disposed)
				{
					return;
				}
				this.disposed = true;
				try
				{
					AppDomain.Unload(this.domain);
				}
				catch (CannotUnloadAppDomainException)
				{
				}
			}

			private static readonly string privateBinPath = string.Format("{0};{1}", Path.Combine(Environment.CurrentDirectory, (IntPtr.Size == 4) ? "x86" : "x64"), Environment.CurrentDirectory);

			private AppDomain domain;

			private bool disposed;
		}
	}
}
