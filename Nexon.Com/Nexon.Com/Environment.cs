using System;
using System.Net;
using System.Reflection;
using System.Web;

namespace Nexon.Com
{
	public class Environment
	{
		public static string HostName
		{
			get
			{
				if (HttpContext.Current != null && HttpContext.Current.Server.MachineName != null)
				{
					return HttpContext.Current.Server.MachineName.ToUpper();
				}
				return Dns.GetHostName();
			}
		}

		public static string LocalIP
		{
			get
			{
				string result;
				try
				{
					if (HttpContext.Current != null)
					{
						result = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
					}
					else
					{
						IPHostEntry hostEntry = Dns.GetHostEntry(Environment.HostName);
						IPAddress[] addressList = hostEntry.AddressList;
						foreach (IPAddress ipaddress in addressList)
						{
							if (!ipaddress.ToString().StartsWith("192.168"))
							{
								return ipaddress.ToString();
							}
						}
						if (addressList.Length > 0)
						{
							result = addressList[0].ToString();
						}
						else
						{
							result = string.Empty;
						}
					}
				}
				catch
				{
					result = string.Empty;
				}
				return result;
			}
		}

		public static string HttpHostName
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
				}
				return string.Empty;
			}
		}

		public static string RequestUrl
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.Request.Url.AbsoluteUri;
				}
				return string.Empty;
			}
		}

		public static string ReferrerUrl
		{
			get
			{
				if (HttpContext.Current != null && HttpContext.Current.Request.UrlReferrer != null)
				{
					return HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
				}
				return string.Empty;
			}
		}

		public static string ClientIP
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.Request.UserHostAddress;
				}
				return string.Empty;
			}
		}

		public static string AppRoot
		{
			get
			{
				if (Environment._AppRoot == null && HttpContext.Current != null)
				{
					string text = HttpContext.Current.Request.ServerVariables["APPL_MD_PATH"];
					if (text.ToLower().IndexOf("/root/") != -1)
					{
						text = text.Substring(text.ToLower().IndexOf("/root/") + 5);
					}
					Environment._AppRoot = text;
				}
				return Environment._AppRoot;
			}
		}

		public static string HttpRoot
		{
			get
			{
				if (Environment._HttpRoot == null && HttpContext.Current != null)
				{
					string scheme = HttpContext.Current.Request.Url.Scheme;
					string host = HttpContext.Current.Request.Url.Host;
					int port = HttpContext.Current.Request.Url.Port;
					Environment._HttpRoot = scheme + "://" + host + ((port != 80) ? (":" + port) : "");
				}
				return Environment._HttpRoot;
			}
		}

		public static string PhysicalAppRoot
		{
			get
			{
				if (Environment._PhysicalAppRoot == null && HttpContext.Current != null)
				{
					string physicalAppRoot = HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
					Environment._PhysicalAppRoot = physicalAppRoot;
				}
				return Environment._PhysicalAppRoot;
			}
		}

		public static string ProgramName
		{
			get
			{
				string result;
				try
				{
					result = Assembly.GetEntryAssembly().GetName().Name;
				}
				catch
				{
					result = string.Empty;
				}
				return result;
			}
		}

		public static string ProgramLocation
		{
			get
			{
				string result;
				try
				{
					result = Assembly.GetEntryAssembly().Location;
				}
				catch
				{
					result = string.Empty;
				}
				return result;
			}
		}

		private static string _AppRoot;

		private static string _HttpRoot;

		private static string _PhysicalAppRoot;
	}
}
