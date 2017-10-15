using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Utility;

namespace Executer
{
	internal class Program
	{
		private static void RunService(string[] args)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.SetData("APP_CONFIG_FILE", "ServiceCore.dll.config");
			bool flag = true;
			if (args[0] == "/force")
			{
				flag = false;
				args = args.Skip(1).ToArray<string>();
			}
			bool flag2 = false;
			int periodMilis = 0;
			if (args[0].ToLower() == "/process_performance".ToLower())
			{
				flag2 = true;
				periodMilis = int.Parse(args[1]) * 1000;
				args = args.Skip(2).ToArray<string>();
			}
			string privateBinPath = string.Format("{0}", Path.Combine(Path.GetDirectoryName(args[0]), (IntPtr.Size == 4) ? "x86" : "x64"));
			object[] parameters = args.Skip(4).ToArray<string>();
			string text = args[0];
			string text2 = args[1];
			string text3 = args[2];
			string title = args[3];
			currentDomain.AssemblyResolve += delegate(object s, ResolveEventArgs e)
			{
				string assemblyFile = string.Format("{0}\\{1}.dll", privateBinPath, e.Name.Substring(0, e.Name.IndexOf(",")));
				return Assembly.LoadFrom(assemblyFile);
			};
			Assembly assembly = Assembly.LoadFrom(text);
			if (assembly == null)
			{
				throw new ApplicationException(string.Format("Invalid Assembly Name {0}", text));
			}
			Type type = assembly.GetType(text2, false, true);
			if (type == null)
			{
				throw new ApplicationException(string.Format("Invalid Type Name {0}", text2));
			}
			MethodInfo method = type.GetMethod(text3, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new ApplicationException(string.Format("Invalid Method Name {0}", text3));
			}
			Console.Title = title;
			object obj = method.Invoke(null, parameters);
			if (flag2)
			{
				IQueryPerformance[] perfArray = null;
				if (obj is IQueryPerformance)
				{
					perfArray = new IQueryPerformance[]
					{
						obj as IQueryPerformance
					};
				}
				else if (obj is IQueryPerformance[])
				{
					perfArray = (obj as IQueryPerformance[]);
				}
				Program.performanceWriter = new ProcessPerformanceWriter(perfArray, periodMilis);
				Program.performanceWriter.Initialize();
			}
			IBootSequence bootSequence = (obj is IBootSequence) ? (obj as IBootSequence) : null;
			if (bootSequence != null && obj != null && flag)
			{
				return;
			}
			Console.WriteLine("launched");
			Program.launched.Set();
		}

		public static void SetLaunched()
		{
			Program.launched.Set();
		}

		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

		private static void Main(string[] args)
		{
			if (args.Length < 4)
			{
				Console.WriteLine("{0} Assembly Type Method DomainName [params]", Environment.GetCommandLineArgs()[0]);
				Environment.ExitCode = 1;
				return;
			}
			try
			{
				Program.RunService(args);
				Environment.ExitCode = 0;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception Occurred : {0}\r\n{1}\r\n", ex.Message, ex.StackTrace);
				if (ex.InnerException != null)
				{
					Console.WriteLine("InnerException : {0}\r\n{1}\r\n", ex.InnerException.Message, ex.InnerException.StackTrace);
				}
				Environment.ExitCode = 2;
			}
			Program.commandManger = new Commands();
			Program.launched.WaitOne();
			for (;;)
			{
				string text = Console.ReadLine();
				if (text.ToLower() == "shutdown")
				{
					Program.GenerateConsoleCtrlEvent(0u, 0u);
				}
				else
				{
					Program.commandManger.Handle(text);
				}
			}
		}

		private const string ConfigurationFile = "ServiceCore.dll.config";

		private static Commands commandManger;

		private static AutoResetEvent launched = new AutoResetEvent(false);

		private static ProcessPerformanceWriter performanceWriter = null;
	}
}
