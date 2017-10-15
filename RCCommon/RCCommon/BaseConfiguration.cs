using System;
using System.IO;

namespace RemoteControlSystem
{
	public class BaseConfiguration
	{
		public static readonly string WorkingDirectory = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

		public static readonly int ServerVersion = 1;

		public static readonly string ServerServiceName = "RCServer";

		public static readonly string ServerServiceFullName = "Remote Control Server Service";

		public static readonly string ServerConfigFile = "RCSconfig.xml";

		public static readonly string ServerScriptInfoFile = "ScriptInfo.xml";

		public static readonly int ServerClientPort = 10001;

		public static readonly int ServerControlPort = 10002;

		public static readonly string ClientServiceName = "RCClient";

		public static readonly string ClientServiceFullName = "Remote Control Client Service";

		public static string ClientConfigFile = "RCCconfig.xml";

		public static readonly string ServerAddress = "127.0.0.1";
	}
}
