using System;

namespace Nexon.Com.UserWrapper.Warning
{
	public static class Warning
	{
		public static UserWarningInfo GetUserWarningInfo(ServiceCode serviceCode, int nexonSN, int gameCode)
		{
			UserWarningGetListSoapResult userWarningGetListSoapResult = new UserWarningGetInfoSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				GameCode = gameCode
			}.Execute();
			if (userWarningGetListSoapResult.SoapErrorCode == 0 && userWarningGetListSoapResult.UserWarningList.Count == 1)
			{
				return userWarningGetListSoapResult.UserWarningList[0];
			}
			return null;
		}
	}
}
