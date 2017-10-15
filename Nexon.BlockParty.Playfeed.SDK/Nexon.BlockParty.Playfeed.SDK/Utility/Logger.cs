using System;
using System.Diagnostics;
using System.Reflection;
using log4net;

namespace Nexon.BlockParty.Utility
{
	public class Logger
	{
		private static string CallingMethodFullName
		{
			get
			{
				MethodBase method = new StackTrace().GetFrame(2).GetMethod();
				return method.DeclaringType.FullName + "." + method.Name;
			}
		}

		private static string CallingMethodName
		{
			get
			{
				MethodBase method = new StackTrace().GetFrame(2).GetMethod();
				return method.Name;
			}
		}

		public static void Warn(string message)
		{
			Logger._logger.Warn(Logger.CallingMethodName + "," + message);
		}

		public static void Log(string message)
		{
			Logger._logger.Info(Logger.CallingMethodName + "," + message);
		}

		public static void Log(string message, Exception exception)
		{
			Logger._logger.Error(Logger.CallingMethodName + "," + message, exception);
		}

		public static void Log(Exception exception)
		{
			Logger._logger.Error(Logger.CallingMethodName + "," + exception.Message, exception);
		}

		private static readonly ILog _logger = LogManager.GetLogger("Nexon.BlockParty.Playfeed.SDK.PlayfeedWriter");
	}
}
