using System;

namespace Nexon.Com.Net
{
	public class NexonIPFilter : IPFilter
	{
		private NexonIPFilter()
		{
			base.AddIPs("192.168.*.*");
			base.AddIPs("221.186.67.*");
			base.AddIPs("220.90.205.*");
			base.AddIPs("210.101.84.*");
			base.AddIPs("210.101.85.*");
			base.AddIPs("211.218.230.*");
			base.AddIPs("211.218.232.*");
			base.AddIPs("211.218.234.*");
			base.AddIPs("211.218.235.*");
			base.AddIPs("211.218.236.*");
			base.AddIPs("211.218.237.*");
			base.AddIPs("112.175.187.*");
			base.AddIPs("10.254.74.*");
			base.AddIPs("218.145.45.102");
			base.AddIPs("211.219.11.199");
			base.AddIPs("211.219.50.147");
			base.AddIPs("211.218.231.*");
			base.AddIPs("127.0.0.1");
			base.AddIPs("220.80.249.48");
		}

		public static bool IsNexonIP()
		{
			string clientIP = Environment.ClientIP;
			return NexonIPFilter.IsNexonIP(clientIP);
		}

		public static bool IsNexonIP(string ip)
		{
			return NexonIPFilter._filter.CheckIP(ip);
		}

		private static NexonIPFilter _filter = new NexonIPFilter();
	}
}
