using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ExecutionSupporter
{
	public static class ProcessCommandHelper
	{
		public static List<string> CommandList { get; set; }

		public static string HelpText { get; set; }

		static ProcessCommandHelper()
		{
			ProcessCommandHelper.processFunc = new Dictionary<string, MethodInfo>();
			ProcessCommandHelper.CommandList = new List<string>();
			Type typeFromHandle = typeof(ProcessCommandFunc);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MethodInfo methodInfo in typeFromHandle.GetMethods())
			{
				if (methodInfo.Name.StartsWith("Cmd_"))
				{
					string text = methodInfo.Name.Substring("Cmd_".Length);
					ProcessCommandHelper.processFunc.Add(text, methodInfo);
					ProcessCommandHelper.CommandList.Add(text);
					foreach (object obj in methodInfo.GetCustomAttributes(false))
					{
						if (obj is Help)
						{
							Help help = obj as Help;
							stringBuilder.AppendFormat("{0}\r\n - {1}", help.Usage ?? text, help.HelpText).AppendLine();
						}
					}
				}
			}
			ProcessCommandHelper.HelpText = stringBuilder.ToString();
		}

		public static void Initialize()
		{
		}

		public static bool Process(ExecutionSupportCore core, string cmd, string arg)
		{
			if (ProcessCommandHelper.processFunc.ContainsKey(cmd))
			{
				object[] parameters = new object[]
				{
					core,
					arg
				};
				try
				{
					ProcessCommandHelper.processFunc[cmd].Invoke(ProcessCommandHelper.Func, parameters);
					return true;
				}
				catch (Exception ex)
				{
					core.LogManager.AddLog(LogType.ERROR, "exception occurred - {0}", new object[]
					{
						ex.Message
					});
					return false;
				}
			}
			core.LogManager.AddLog(LogType.ERROR, "unknown command - {0}", new object[]
			{
				cmd
			});
			return false;
		}

		private static Dictionary<string, MethodInfo> processFunc;

		private static ProcessCommandFunc Func = new ProcessCommandFunc();
	}
}
