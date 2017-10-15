using System;
using System.Web;
using NPL.SSO.Configuration;

namespace NPL.SSO
{
	public class ErrorLogger
	{
		private ErrorLogger()
		{
		}

		public static void WriteLog(Enum n4ErrorCode, string strErrorMessage, string strStackTrace, string strLoginID, string strRequestInfo)
		{
			try
			{
				if (Config.Authenticator.Soap.ErrorLog)
				{
					string strRequestUrl = string.Empty;
					string strServerIP = string.Empty;
					string strHostName = string.Empty;
					string strClientIP = string.Empty;
					string strUrlReferrer = string.Empty;
					string strServerName = string.Empty;
					if (HttpContext.Current != null)
					{
						strRequestUrl = HttpContext.Current.Request.Url.ToString();
						strServerIP = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
						strHostName = HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
						strClientIP = HttpContext.Current.Request.UserHostAddress;
						strUrlReferrer = HttpContext.Current.Request.UrlReferrer.ToString();
						strServerName = HttpContext.Current.Server.MachineName;
					}
					ErrorLogDAO errorLogDAO = new ErrorLogDAO(Convert.ToInt32(n4ErrorCode), strErrorMessage, strServerIP, strRequestUrl, strHostName, strStackTrace, strClientIP, strUrlReferrer, strLoginID, strRequestInfo, 1010100, 1, strServerName);
					errorLogDAO.Execute();
				}
			}
			catch
			{
			}
		}
	}
}
