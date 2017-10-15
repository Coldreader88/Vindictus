using System;
using System.Reflection;

namespace Utility
{
	[Serializable]
	public class Invoker
	{
		public string Assembly { get; set; }

		public string Type { get; set; }

		public string Method { get; set; }

		public string AppName { get; set; }

		public string BinPath { get; set; }

		public string DomainName { get; set; }

		public object[] Parameters { get; set; }

		public string ConfigurationFile { get; set; }

		public AppDomain Execute()
		{
			AppDomainSetup info = new AppDomainSetup
			{
				ApplicationName = this.AppName,
				PrivateBinPath = this.BinPath,
				ConfigurationFile = this.ConfigurationFile
			};
			AppDomain appDomain = AppDomain.CreateDomain(this.DomainName, AppDomain.CurrentDomain.Evidence, info);
			appDomain.UnhandledException += this.CurrentDomain_UnhandledException;
			appDomain.DoCallBack(new CrossAppDomainDelegate(this.Callback));
			return appDomain;
		}

		private void Callback()
		{
			System.Reflection.Assembly.LoadFrom(this.Assembly);
			Type type = null;
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				type = assembly.GetType(this.Type, false, true);
				if (type != null)
				{
					break;
				}
			}
			MethodInfo method = type.GetMethod(this.Method, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			method.Invoke(null, this.Parameters);
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Log<Invoker>.Logger.Fatal(sender, e.ExceptionObject as Exception);
		}
	}
}
