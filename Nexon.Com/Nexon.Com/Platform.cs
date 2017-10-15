using System;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Nexon.Com
{
	public static class Platform
	{
		internal static emPlatform GetPlatformForErrorLog()
		{
			string text;
			if (HttpContext.Current != null)
			{
				text = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
			}
			else
			{
				text = Environment.LocalIP;
			}
			if (text == "127.0.0.1")
			{
				return emPlatform.work;
			}
			if (Platform.PrePIs.Contains(text))
			{
				return emPlatform.preservice;
			}
			if (text.IndexOf("211.218.231") >= 0)
			{
				return emPlatform.test;
			}
			return emPlatform.service;
		}

		public static emPlatform GetPlatform()
		{
			return (ConfigurationManager.AppSettings.Get("platform") ?? "work").ToLower().Parse(emPlatform.work);
		}

		public static bool IsServicePlatform
		{
			get
			{
				return Platform.GetPlatform() == emPlatform.service || Platform.GetPlatform() == emPlatform.preservice;
			}
		}

		public static bool IsTestPlatform
		{
			get
			{
				return Platform.GetPlatform() == emPlatform.test;
			}
		}

		public static bool IsWorkPlatform
		{
			get
			{
				return Platform.GetPlatform() == emPlatform.work;
			}
		}

		private static string[] PrePIs = new string[]
		{
			"10.0.8.1",
			"10.0.8.254",
			"10.0.8.253",
			"220.90.205.53",
			"220.90.205.83",
			"220.90.205.82",
			"222.122.222.122",
			"218.145.45.135"
		};
	}
}
