using System;

namespace Nexon.Com.UserWrapper.SEOUL2012Web
{
	public static class SEOUL2012Web
	{
		public static int GetUserIdentitySN(ServiceCode serviceCode, string nexonID, out long identitySN)
		{
			identitySN = 0L;
			UserGetIdentitySNSoapResult userGetIdentitySNSoapResult = new UserGetIdentitySNSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonID = nexonID
			}.Execute();
			if (userGetIdentitySNSoapResult.SoapErrorCode == 0)
			{
				identitySN = userGetIdentitySNSoapResult.IdentitySN;
				return userGetIdentitySNSoapResult.SoapErrorCode;
			}
			throw new UserWrapperException((ErrorCode)userGetIdentitySNSoapResult.SoapErrorCode, userGetIdentitySNSoapResult.SoapErrorMessage, null);
		}
	}
}
