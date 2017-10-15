using System;
using System.Collections.Generic;
using System.Web;

namespace Nexon.Com.Web
{
	public class Redirector
	{
		public Redirector(string strMainPageUrl)
		{
			this._MainPageUrl = strMainPageUrl;
		}

		public void AddMainUrl(string strPattern)
		{
			if (strPattern == null || strPattern.Trim() == string.Empty)
			{
				throw new Exception("strPattern is null or empty.");
			}
			this._MainRedirectUrl.Add(new RedirectorRegEx(strPattern.Trim(), string.Empty));
		}

		public void AddRedirectUrl(string strPattern, string strReplacePattern)
		{
			if (strPattern == null || strPattern.Trim() == string.Empty)
			{
				throw new Exception("strPattern is null or empty.");
			}
			if (strReplacePattern == null || strReplacePattern.Trim() == string.Empty)
			{
				throw new Exception("strReplacePattern is null or empty.");
			}
			this._RedirectUrl.Add(new RedirectorRegEx(strPattern.Trim(), strReplacePattern));
		}

		public string GetRedirectUrl(string strRequestUrl)
		{
			if (strRequestUrl == null)
			{
				throw new Exception("strRequestUrl is null.");
			}
			strRequestUrl = strRequestUrl.Trim();
			string text = null;
			RedirectorRegEx redirectorRegEx = new RedirectorRegEx(string.Empty, string.Empty);
			foreach (RedirectorRegEx redirectorRegEx2 in this._RedirectUrl)
			{
				string text2;
				if (redirectorRegEx2.Replace(strRequestUrl, out text2))
				{
					if (redirectorRegEx2.Length == redirectorRegEx.Length)
					{
						throw new Exception("Duplicated Pattern. Check AddRedirectUrl Method!!!");
					}
					if (redirectorRegEx2.Length > redirectorRegEx.Length)
					{
						redirectorRegEx = redirectorRegEx2;
						text = text2;
					}
				}
			}
			foreach (RedirectorRegEx redirectorRegEx3 in this._MainRedirectUrl)
			{
				if (redirectorRegEx3.Match(strRequestUrl))
				{
					if (redirectorRegEx3.Length == redirectorRegEx.Length)
					{
						throw new Exception("Duplicated Pattern. Check AddMainUrl Method!!!");
					}
					if (redirectorRegEx3.Length > redirectorRegEx.Length)
					{
						redirectorRegEx = redirectorRegEx3;
						text = this._MainPageUrl;
						break;
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.IndexOf('&');
				if (num > 0 && text.IndexOf('?') < 0)
				{
					text = text.Substring(0, num) + "?" + text.Substring(num + 1);
				}
			}
			return text;
		}

		public static string GetErrorPage(string strErrorURL, int n4ErrorLogSN, DateTime dtCreateDate)
		{
			string value;
			if (Platform.IsServicePlatform)
			{
				value = "S";
			}
			else if (Platform.IsTestPlatform)
			{
				value = "T";
			}
			else
			{
				value = "W";
			}
			string text = string.Empty;
			if (HttpContext.Current.Request != null)
			{
				text = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
				text = text.Substring(text.LastIndexOf(".") + 1);
			}
			Url url = new Url(strErrorURL);
			url["env"] = value;
			url["oidErrorLog"] = n4ErrorLogSN.ToString();
			url["dateErrorCreate"] = dtCreateDate.ToString("yyyy-MM-dd");
			url["s"] = text;
			return url.URL;
		}

		private string _MainPageUrl;

		private List<RedirectorRegEx> _MainRedirectUrl = new List<RedirectorRegEx>();

		private List<RedirectorRegEx> _RedirectUrl = new List<RedirectorRegEx>();
	}
}
