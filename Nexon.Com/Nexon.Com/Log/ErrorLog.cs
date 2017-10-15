using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;

namespace Nexon.Com.Log
{
	public static class ErrorLog
	{
		public static ErrorLogInfo CreateErrorLog(ServiceCode serviceCode, int n4ErrorCode, string strLoginID, Exception ex)
		{
			int num;
			DateTime dateTime;
			return ErrorLog.CreateErrorLog(serviceCode, n4ErrorCode, strLoginID, ex, out num, out dateTime);
		}

		public static ErrorLogInfo CreateErrorLog(ServiceCode serviceCode, int n4ErrorCode, string strLoginID, string strErrorMessage, string strStackTrace)
		{
			int num;
			DateTime dateTime;
			return ErrorLog.CreateErrorLog(serviceCode, n4ErrorCode, strLoginID, strErrorMessage, strStackTrace, out num, out dateTime);
		}

		public static ErrorLogInfo CreateErrorLog(ServiceCode serviceCode, int n4ErrorCode, string strLoginID, Exception ex, out int n4ErrorLogSN, out DateTime dtCreateDate)
		{
			string strErrorMessage;
			string strStackTrace;
			if (ex != null && ex.InnerException != null)
			{
				strErrorMessage = ex.InnerException.Message;
				strStackTrace = ex.InnerException.StackTrace;
			}
			else if (ex != null)
			{
				strErrorMessage = ex.Message;
				strStackTrace = ex.StackTrace;
			}
			else
			{
				strErrorMessage = "Unknown Exception";
				strStackTrace = "Unknown Exception";
			}
			return ErrorLog.CreateErrorLog(serviceCode, n4ErrorCode, strLoginID, strErrorMessage, strStackTrace, out n4ErrorLogSN, out dtCreateDate);
		}

		public static ErrorLogInfo CreateErrorLog(ServiceCode serviceCode, int n4ErrorCode, string strLoginID, string strErrorMessage, string strStackTrace, out int n4ErrorLogSN, out DateTime dtCreateDate)
		{
			ErrorLogCreateSPWrapper errorLogCreateSPWrapper = new ErrorLogCreateSPWrapper();
			errorLogCreateSPWrapper.n4ServiceCode = serviceCode.Parse(0);
			errorLogCreateSPWrapper.n4ErrorCode = n4ErrorCode;
			errorLogCreateSPWrapper.strErrorMessage = strErrorMessage;
			errorLogCreateSPWrapper.strLoginID = strLoginID;
			errorLogCreateSPWrapper.strStackTrace = strStackTrace;
			errorLogCreateSPWrapper.n1PlatformCode = Platform.GetPlatformForErrorLog().Parse<byte>(0);
			errorLogCreateSPWrapper.strServerName = Environment.HostName;
			errorLogCreateSPWrapper.strServerIP = Environment.LocalIP;
			if (HttpContext.Current != null)
			{
				errorLogCreateSPWrapper.strDomainName = Environment.HttpHostName;
				errorLogCreateSPWrapper.strRequestUrl = Environment.RequestUrl;
				errorLogCreateSPWrapper.strClientIP = Environment.ClientIP;
				errorLogCreateSPWrapper.strUrlReferrer = Environment.ReferrerUrl;
				StringBuilder stringBuilder = new StringBuilder();
				string arg = HttpContext.Current.Request.ServerVariables["ALL_HTTP"];
				string httpMethod = HttpContext.Current.Request.HttpMethod;
				string arg2 = HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
				stringBuilder.AppendFormat("[Headers]\r\n{0}\r\n[HttpMethod]\r\n{1}\r\n[QueryString]\r\n{2}\r\n[Form]\r\n", arg, httpMethod, arg2);
				foreach (object obj in HttpContext.Current.Request.Form)
				{
					string text = (string)obj;
					if (text != "__VIEWSTATE")
					{
						stringBuilder.AppendFormat("{0}:{1}\r\n", text, HttpContext.Current.Request.Form[text]);
					}
				}
				errorLogCreateSPWrapper.strRequestInfo = stringBuilder.ToString();
			}
			else if (OperationContext.Current != null && OperationContext.Current.RequestContext != null)
			{
				errorLogCreateSPWrapper.strDomainName = OperationContext.Current.RequestContext.RequestMessage.Headers.To.Host;
				errorLogCreateSPWrapper.strRequestUrl = OperationContext.Current.RequestContext.RequestMessage.Headers.To.AbsoluteUri;
				RemoteEndpointMessageProperty remoteEndpointMessageProperty = OperationContext.Current.RequestContext.RequestMessage.Properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
				if (remoteEndpointMessageProperty != null)
				{
					errorLogCreateSPWrapper.strClientIP = remoteEndpointMessageProperty.Address;
				}
				HttpRequestMessageProperty httpRequestMessageProperty = OperationContext.Current.RequestContext.RequestMessage.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
				if (httpRequestMessageProperty != null)
				{
					if (httpRequestMessageProperty.QueryString != null && httpRequestMessageProperty.QueryString != string.Empty)
					{
						errorLogCreateSPWrapper.strRequestUrl = string.Format("{0}?{1}", OperationContext.Current.RequestContext.RequestMessage.Headers.To.AbsoluteUri, httpRequestMessageProperty.QueryString);
					}
					StringBuilder stringBuilder2 = new StringBuilder();
					foreach (string text2 in httpRequestMessageProperty.Headers.AllKeys)
					{
						stringBuilder2.AppendFormat("{0}:{1}", text2, httpRequestMessageProperty.Headers[text2]);
					}
					errorLogCreateSPWrapper.strRequestInfo = string.Format("[Hearder]\r\n{0}\r\n[HttpMethod]\r\n{1}\r\n[QueryString]\r\n{2}\r\n[Form]\r\n", stringBuilder2.ToString(), httpRequestMessageProperty.Method, httpRequestMessageProperty.QueryString);
				}
			}
			else
			{
				errorLogCreateSPWrapper.strDomainName = Environment.ProgramName;
				errorLogCreateSPWrapper.strRequestUrl = Environment.ProgramLocation;
			}
			ErrorLogCreateSPResult errorLogCreateSPResult = errorLogCreateSPWrapper.Execute();
			if (errorLogCreateSPResult.SPErrorCode == 0)
			{
				n4ErrorLogSN = errorLogCreateSPResult.n4ErrorLogSN;
				dtCreateDate = errorLogCreateSPResult.dtCreateDate;
				return errorLogCreateSPResult.errorLogInfo;
			}
			n4ErrorLogSN = 0;
			dtCreateDate = DateTime.MinValue;
			return null;
		}

		public static bool CreateLightErrorLog(ServiceCode serviceCode, int n4ErrorCode, string strLoginID, Exception ex)
		{
			int num = 0;
			DateTime minValue = DateTime.MinValue;
			string strErrorMessage;
			string strStackTrace;
			if (ex != null && ex.InnerException != null)
			{
				strErrorMessage = ex.InnerException.Message;
				strStackTrace = ex.InnerException.StackTrace;
			}
			else if (ex != null)
			{
				strErrorMessage = ex.Message;
				strStackTrace = ex.StackTrace;
			}
			else
			{
				strErrorMessage = "Unknown Exception";
				strStackTrace = "Unknown Exception";
			}
			return ErrorLog.CreateLightErrorLog(serviceCode, n4ErrorCode, strLoginID, strErrorMessage, strStackTrace, out num, out minValue);
		}

		public static bool CreateLightErrorLog(ServiceCode serviceCode, int n4ErrorCode, string strLoginID, string strErrorMessage, string strStackTrace, out int n4ErrorLogSN, out DateTime dtCreateDate)
		{
			LightErrorLogCreateSPWrapper lightErrorLogCreateSPWrapper = new LightErrorLogCreateSPWrapper();
			lightErrorLogCreateSPWrapper.n4ServiceCode = serviceCode.Parse(0);
			lightErrorLogCreateSPWrapper.n4ErrorCode = n4ErrorCode;
			lightErrorLogCreateSPWrapper.strErrorMessage = strErrorMessage;
			lightErrorLogCreateSPWrapper.strLoginID = strLoginID;
			lightErrorLogCreateSPWrapper.strStackTrace = strStackTrace;
			lightErrorLogCreateSPWrapper.n1PlatformCode = Platform.GetPlatformForErrorLog().Parse<byte>(0);
			lightErrorLogCreateSPWrapper.strServerName = Environment.HostName;
			lightErrorLogCreateSPWrapper.strServerIP = Environment.LocalIP;
			if (HttpContext.Current != null)
			{
				lightErrorLogCreateSPWrapper.strDomainName = Environment.HttpHostName;
				lightErrorLogCreateSPWrapper.strRequestUrl = Environment.RequestUrl;
				lightErrorLogCreateSPWrapper.strClientIP = Environment.ClientIP;
				lightErrorLogCreateSPWrapper.strUrlReferrer = Environment.ReferrerUrl;
				StringBuilder stringBuilder = new StringBuilder();
				string arg = HttpContext.Current.Request.ServerVariables["ALL_HTTP"];
				string httpMethod = HttpContext.Current.Request.HttpMethod;
				string arg2 = HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
				stringBuilder.AppendFormat("[Headers]\r\n{0}\r\n[HttpMethod]\r\n{1}\r\n[QueryString]\r\n{2}\r\n[Form]\r\n", arg, httpMethod, arg2);
				foreach (object obj in HttpContext.Current.Request.Form)
				{
					string text = (string)obj;
					if (text != "__VIEWSTATE")
					{
						stringBuilder.AppendFormat("{0}:{1}\r\n", text, HttpContext.Current.Request.Form[text]);
					}
				}
				lightErrorLogCreateSPWrapper.strRequestInfo = stringBuilder.ToString();
			}
			else if (OperationContext.Current != null && OperationContext.Current.RequestContext != null)
			{
				lightErrorLogCreateSPWrapper.strDomainName = OperationContext.Current.RequestContext.RequestMessage.Headers.To.Host;
				lightErrorLogCreateSPWrapper.strRequestUrl = OperationContext.Current.RequestContext.RequestMessage.Headers.To.AbsoluteUri;
				RemoteEndpointMessageProperty remoteEndpointMessageProperty = OperationContext.Current.RequestContext.RequestMessage.Properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
				if (remoteEndpointMessageProperty != null)
				{
					lightErrorLogCreateSPWrapper.strClientIP = remoteEndpointMessageProperty.Address;
				}
				HttpRequestMessageProperty httpRequestMessageProperty = OperationContext.Current.RequestContext.RequestMessage.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
				if (httpRequestMessageProperty != null)
				{
					if (httpRequestMessageProperty.QueryString != null && httpRequestMessageProperty.QueryString != string.Empty)
					{
						lightErrorLogCreateSPWrapper.strRequestUrl = string.Format("{0}?{1}", OperationContext.Current.RequestContext.RequestMessage.Headers.To.AbsoluteUri, httpRequestMessageProperty.QueryString);
					}
					StringBuilder stringBuilder2 = new StringBuilder();
					foreach (string text2 in httpRequestMessageProperty.Headers.AllKeys)
					{
						stringBuilder2.AppendFormat("{0}:{1}", text2, httpRequestMessageProperty.Headers[text2]);
					}
					lightErrorLogCreateSPWrapper.strRequestInfo = string.Format("[Hearder]\r\n{0}\r\n[HttpMethod]\r\n{1}\r\n[QueryString]\r\n{2}\r\n[Form]\r\n", stringBuilder2.ToString(), httpRequestMessageProperty.Method, httpRequestMessageProperty.QueryString);
				}
			}
			else
			{
				lightErrorLogCreateSPWrapper.strDomainName = Environment.ProgramName;
				lightErrorLogCreateSPWrapper.strRequestUrl = Environment.ProgramLocation;
			}
			ErrorLogCreateSPResult errorLogCreateSPResult = lightErrorLogCreateSPWrapper.Execute();
			if (errorLogCreateSPResult.SPErrorCode == 0)
			{
				n4ErrorLogSN = errorLogCreateSPResult.n4ErrorLogSN;
				dtCreateDate = errorLogCreateSPResult.dtCreateDate;
				return true;
			}
			n4ErrorLogSN = 0;
			dtCreateDate = DateTime.MinValue;
			return false;
		}
	}
}
