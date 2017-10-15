using System;

namespace Nexon.Com.Log
{
	public class ErrorLogInfo
	{
		public ErrorLogInfo(int oidErrorLog, DateTime dateCreate, string strErrorMessage, string strServerIP, string strRequestUrl, string strStackTrace, string strHostName, string strClientIP, string strLoginID, string strUrlReferrer, int n4ErrorCode, int n4ServiceCode, string strRequestInfo, byte n1PlatformCode, string strServerName)
		{
			this.n4ErrorLogSN = oidErrorLog;
			this.dtCreateDate = dateCreate;
			this.strErrorMessage = strErrorMessage;
			this.strServerIP = strServerIP;
			this.strRequestUrl = strRequestUrl;
			this.strStackTrace = strStackTrace;
			this.strHostName = strHostName;
			this.strClientIP = strClientIP;
			this.strLoginID = strLoginID;
			this.strUrlReferrer = strUrlReferrer;
			this.n4ErrorCode = n4ErrorCode;
			this.n4ServiceCode = n4ServiceCode;
			this.strRequestInfo = strRequestInfo;
			this.n1PlatformCode = n1PlatformCode;
			this.strServerName = strServerName;
		}

		public int n4ErrorLogSN { get; set; }

		public DateTime dtCreateDate { get; set; }

		public string strErrorMessage { get; set; }

		public string strServerIP { get; set; }

		public string strRequestUrl { get; set; }

		public string strStackTrace { get; set; }

		public string strHostName { get; set; }

		public string strClientIP { get; set; }

		public string strLoginID { get; set; }

		public string strUrlReferrer { get; set; }

		public int n4ErrorCode { get; set; }

		public int n4ServiceCode { get; set; }

		public string strRequestInfo { get; set; }

		public byte n1PlatformCode { get; set; }

		public string strServerName { get; set; }
	}
}
