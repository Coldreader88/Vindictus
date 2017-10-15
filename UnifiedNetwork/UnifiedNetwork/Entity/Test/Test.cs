using System;
using System.IO;
using System.Linq;
using Utility;

namespace UnifiedNetwork.Entity.Test
{
	public class Test
	{
		private static void Execute(string[] args)
		{
			object[] parameters = args.Skip(4).ToArray<string>();
			Invoker invoker = new Invoker
			{
				Assembly = args[0],
				Type = args[1],
				Method = args[2],
				AppName = args[0],
				BinPath = Path.GetDirectoryName(args[0]),
				DomainName = args[3],
				Parameters = parameters
			};
			invoker.Execute();
		}

		public Test()
		{
			Test.Execute(Test.locationArgs);
			AService.StartService();
			BService.StartService();
		}

		private static readonly string[] locationArgs = new string[]
		{
			"Z:\\Heroes\\game\\server\\bin\\UnifiedNetwork.dll",
			"UnifiedNetwork.LocationService.LocationService",
			"StartService",
			"LocationService",
			"3000"
		};
	}
}
