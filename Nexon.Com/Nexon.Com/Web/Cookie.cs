using System;
using System.Web;

namespace Nexon.Com.Web
{
	public class Cookie
	{
		public static void SetCookie(string strCookieDomainName, string strKey, string strValue)
		{
			Cookie.SetCookie(strCookieDomainName, strKey, strValue, false);
		}

		public static void SetCookie(string strCookieDomainName, string strKey, string strValue, bool isHttpOnly)
		{
			HttpCookieCollection cookies = HttpContext.Current.Response.Cookies;
			HttpCookie httpCookie = new HttpCookie(strKey, strValue);
			if (!string.IsNullOrEmpty(strCookieDomainName))
			{
				httpCookie.Domain = strCookieDomainName;
			}
			httpCookie.Path = "/";
			httpCookie.HttpOnly = isHttpOnly;
			cookies.Set(httpCookie);
		}

		public static void SetSubCookie(string strCookieDomainName, string strGroupName, string strKey, string strValue)
		{
			Cookie.SetSubCookie(strCookieDomainName, strGroupName, strKey, strValue, false);
		}

		public static void SetSubCookie(string strCookieDomainName, string strGroupName, string strKey, string strValue, bool isHttpOnly)
		{
			HttpCookieCollection cookies = HttpContext.Current.Response.Cookies;
			HttpCookie httpCookie;
			if (cookies[strGroupName] == null)
			{
				httpCookie = new HttpCookie(strGroupName);
				if (!string.IsNullOrEmpty(strCookieDomainName))
				{
					httpCookie.Domain = strCookieDomainName;
				}
				httpCookie.Path = "/";
				httpCookie.HttpOnly = isHttpOnly;
				httpCookie.Values.Set(strKey, strValue);
				cookies.Set(httpCookie);
				return;
			}
			httpCookie = cookies.Get(strGroupName);
			if (!string.IsNullOrEmpty(strCookieDomainName))
			{
				httpCookie.Domain = strCookieDomainName;
			}
			httpCookie.Path = "/";
			httpCookie.HttpOnly = isHttpOnly;
			httpCookie.Values.Set(strKey, strValue);
			cookies.Set(httpCookie);
		}

		public static string GetCookie(string strKey)
		{
			string result = string.Empty;
			HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
			if (cookies[strKey] != null)
			{
				result = cookies[strKey].Value;
			}
			return result;
		}

		public static void RemoveCookie(string strKey)
		{
			Cookie.RemoveCookie(string.Empty, strKey);
		}

		public static void RemoveCookie(string strCookieDomainName, string strKey)
		{
			HttpCookieCollection cookies = HttpContext.Current.Response.Cookies;
			HttpCookie httpCookie = new HttpCookie(strKey, null);
			if (!string.IsNullOrEmpty(strCookieDomainName))
			{
				httpCookie.Domain = strCookieDomainName;
			}
			httpCookie.Path = "/";
			httpCookie.Expires = DateTime.MinValue;
			cookies.Set(httpCookie);
		}
	}
}
