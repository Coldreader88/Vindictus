using System;
using System.Web;

namespace Nexon.Com.Log
{
	public static class IISLog
	{
		public static void AddMethodNameToIISLog(string strMethodName)
		{
			IISLog.AddMessageToIISLog("#MethodName#:" + strMethodName);
		}

		public static void AddMessageToIISLog(string strMessage)
		{
			if (HttpContext.Current != null && HttpContext.Current.Response != null)
			{
				HttpContext.Current.Response.AppendToLog(strMessage);
			}
		}
	}
}
