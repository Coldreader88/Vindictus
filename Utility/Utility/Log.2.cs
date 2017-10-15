using System;
using log4net;

namespace Utility
{
	public class Log<T> : Log
	{
		static Log()
		{
			Log.Initialize();
		}

		public static ILog Logger
		{
			get
			{
				return Log<T>.logger;
			}
		}

		public static void Fatal(string caller, string message, params object[] args)
		{
			Log<T>.logger.FatalFormat(string.Format("{0} <{1}>", message, caller), args);
		}

		public static void Error(string caller, string message, Exception ex)
		{
			Log<T>.logger.Error(string.Format("{0} <{1}>", message, caller), ex);
		}

		public static void Error(string caller, string message, params object[] args)
		{
			Log<T>.logger.ErrorFormat(string.Format("{0} <{1}>", message, caller), args);
		}

		public static void Warn(string message, params object[] args)
		{
			Log<T>.logger.WarnFormat(message, args);
		}

		public static void Info(string message, params object[] args)
		{
			Log<T>.logger.InfoFormat(message, args);
		}

		public static void Debug(string message, params object[] args)
		{
			Log<T>.logger.DebugFormat(message, args);
		}

		private static readonly ILog logger = LogManager.GetLogger(typeof(T));
	}
}
