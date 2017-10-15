using System;
using System.IO;
using log4net.Config;

namespace RemoteControlSystem
{
	public class Log
	{
		static Log()
		{
			XmlConfigurator.ConfigureAndWatch(new FileInfo(string.Format("{0}.config", typeof(XmlConfigurator).Assembly.Location)));
		}

		internal static void Initialize()
		{
		}
	}
}
