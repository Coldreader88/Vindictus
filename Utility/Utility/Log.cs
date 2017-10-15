using System;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace Utility
{
	public class Log
	{
		public static Level VerboseLevel
		{
			set
			{
				ILoggerRepository repository = LogManager.GetRepository();
				Hierarchy hierarchy = repository as Hierarchy;
				if (hierarchy != null)
				{
					hierarchy.Root.Level = value;
				}
			}
		}

		static Log()
		{
			XmlConfigurator.ConfigureAndWatch(new FileInfo(string.Format("{0}.config", typeof(XmlConfigurator).Assembly.Location)));
		}

		internal static void Initialize()
		{
		}

		public static Level ParseLevel(string strlevel)
		{
			ILoggerRepository repository = LogManager.GetRepository();
			if (repository != null)
			{
				Level level = repository.LevelMap[strlevel.ToUpper()];
				if (level != null)
				{
					return level;
				}
			}
			return Level.Off;
		}
	}
}
