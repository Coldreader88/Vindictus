using System;

namespace Nexon.Com.UserWrapper.GuestUser
{
	public static class GuestUser
	{
		public static bool VerifyUser(out int n4NexonSN, ServiceCode serviceCode, string strNexonID, string strPassword, string strSsnSub)
		{
			UserVerifyWithSsnSoapResult userVerifyWithSsnSoapResult = new UserVerifyWithSsnSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonID = strNexonID,
				Password = strPassword,
				SsnSub = strSsnSub
			}.Execute();
			if (userVerifyWithSsnSoapResult.SoapErrorCode == 0)
			{
				n4NexonSN = userVerifyWithSsnSoapResult.NexonSN;
				return userVerifyWithSsnSoapResult.ValidUser;
			}
			throw new UserWrapperException((ErrorCode)userVerifyWithSsnSoapResult.SoapErrorCode, userVerifyWithSsnSoapResult.SoapErrorMessage, null);
		}
	}
}
