using System;
using System.Configuration;
using Nexon.Com.DAO;

namespace Nexon.Com.Log
{
	internal class ErrorLogConnectionStringProvider : ISQLConnectionStringProvider
	{
		internal ErrorLogConnectionStringProvider()
		{
		}

		public string GetConnectionString()
		{
			ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["ErrorLogConnectionString"];
			if (connectionStringSettings != null)
			{
				return connectionStringSettings.ConnectionString;
			}
			if (Platform.IsServicePlatform)
			{
				return "server=192.168.1.10; uid=nx_service; pwd=$tlfcjswjr%cjfgkr!@#; database=NX_LOG";
			}
			if (Platform.IsTestPlatform)
			{
				return "server=211.218.231.222,10100; uid=nx_service; pwd=$tlfcjswjr%cjfgkr!@#; database=NX_LOG";
			}
			return "server=211.218.231.52,1433; uid=dev_service; pwd=roqkf%tjqltm$open^1101; database=NX_LOG";
		}
	}
}
