using System;
using System.Text.RegularExpressions;

namespace Nexon.Com.UserWrapper.QplayWeb
{
	public static class QplayWeb
	{
		public static bool VerifyUser(ServiceCode serviceCode, string strNexonID, string strPassword)
		{
			string pattern = "[\\x00-\\x08\\x0B-\\x0C\\x0E-\\x1F]";
			if (Regex.IsMatch(strNexonID, pattern) || Regex.IsMatch(strPassword, pattern))
			{
				throw new UserWrapperException(ErrorCode.Common_InvalidInputData, string.Empty, null);
			}
			UserVerifySoapResult userVerifySoapResult = new UserVerifySoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonID = strNexonID,
				Password = strPassword
			}.Execute();
			if (userVerifySoapResult.SoapErrorCode == 0)
			{
				return userVerifySoapResult.PwdMatch;
			}
			throw new UserWrapperException((ErrorCode)userVerifySoapResult.SoapErrorCode, userVerifySoapResult.SoapErrorMessage, null);
		}

		public static WriteStatusCode GetWriteStatusCode(out int n4NexonSN, ServiceCode serviceCode, string strNexonID)
		{
			string pattern = "[\\x00-\\x08\\x0B-\\x0C\\x0E-\\x1F]";
			if (Regex.IsMatch(strNexonID, pattern))
			{
				throw new UserWrapperException(ErrorCode.Common_InvalidInputData, string.Empty, null);
			}
			UserWriteStatusGetInfoSoapResult userWriteStatusGetInfoSoapResult = new UserWriteStatusGetInfoSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonID = strNexonID
			}.Execute();
			if (userWriteStatusGetInfoSoapResult.SoapErrorCode == 0)
			{
				n4NexonSN = userWriteStatusGetInfoSoapResult.NexonSN;
				return userWriteStatusGetInfoSoapResult.WriteStatusCode;
			}
			throw new UserWrapperException((ErrorCode)userWriteStatusGetInfoSoapResult.SoapErrorCode, userWriteStatusGetInfoSoapResult.SoapErrorMessage, null);
		}

		public static bool CheckCharacterBlock(ServiceCode serviceCode, string strNexonID)
		{
			string pattern = "[\\x00-\\x08\\x0B-\\x0C\\x0E-\\x1F]";
			if (Regex.IsMatch(strNexonID, pattern))
			{
				throw new UserWrapperException(ErrorCode.Common_InvalidInputData, string.Empty, null);
			}
			UserCheckCharacterBlockSoapResult userCheckCharacterBlockSoapResult = new UserCheckCharacterBlockSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonID = strNexonID
			}.Execute();
			if (userCheckCharacterBlockSoapResult.SoapErrorCode == 0 || userCheckCharacterBlockSoapResult.SoapErrorCode == 9201)
			{
				return userCheckCharacterBlockSoapResult.CharacterBlock;
			}
			throw new UserWrapperException((ErrorCode)userCheckCharacterBlockSoapResult.SoapErrorCode, userCheckCharacterBlockSoapResult.SoapErrorMessage, null);
		}
	}
}
