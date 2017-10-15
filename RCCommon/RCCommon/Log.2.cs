using System;
using log4net;

namespace RemoteControlSystem
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

		private static readonly ILog logger = LogManager.GetLogger(typeof(T));
	}
}
