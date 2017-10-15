using System;
using System.Text.RegularExpressions;
using Nexon.Com.UserWrapper.Character.DAO;

namespace Nexon.Com.UserWrapper.Character
{
	public static class Character
	{
		public static UserBasicInfo GetUserBasicInfo(ServiceCode serviceCode, int nexonSN, string nexonID, string password)
		{
			UserBasicGetListSoapResult userBasicGetListSoapResult = new UserBasicGetInfoSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				NexonID = nexonID,
				Password = password
			}.Execute();
			if (userBasicGetListSoapResult.SoapErrorCode != 0)
			{
				throw new UserWrapperException((ErrorCode)userBasicGetListSoapResult.SoapErrorCode, userBasicGetListSoapResult.SoapErrorMessage, null);
			}
			if (userBasicGetListSoapResult.UserBasicList.Count == 1)
			{
				return userBasicGetListSoapResult.UserBasicList[0];
			}
			return null;
		}

		public static bool CheckCharacterBlock(ServiceCode serviceCode, int nexonSN)
		{
			UserCheckCharacterBlockSoapResult userCheckCharacterBlockSoapResult = new UserCheckCharacterBlockSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN
			}.Execute();
			if (userCheckCharacterBlockSoapResult.SoapErrorCode == 0)
			{
				return userCheckCharacterBlockSoapResult.CharacterBlock;
			}
			throw new UserWrapperException((ErrorCode)userCheckCharacterBlockSoapResult.SoapErrorCode, userCheckCharacterBlockSoapResult.SoapErrorMessage, null);
		}

		public static bool CheckMatchUserSsn_Full(ServiceCode serviceCode, int nexonSN, string nexonID, string ssn)
		{
			UserSsnCheckMatchResult userSsnCheckMatchResult = new UserSsnCheckMatchFullSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				NexonID = nexonID,
				Ssn = ssn
			}.Execute();
			if (userSsnCheckMatchResult.SoapErrorCode == 0)
			{
				return userSsnCheckMatchResult.IsMatch;
			}
			throw new UserWrapperException((ErrorCode)userSsnCheckMatchResult.SoapErrorCode, userSsnCheckMatchResult.SoapErrorMessage, null);
		}

		public static bool CheckMatchUserSsn_Sub(ServiceCode serviceCode, int nexonSN, string nexonID, string ssn)
		{
			UserSsnCheckMatchResult userSsnCheckMatchResult = new UserSsnCheckMatchSubSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				NexonID = nexonID,
				Ssn = ssn
			}.Execute();
			if (userSsnCheckMatchResult.SoapErrorCode == 0)
			{
				return userSsnCheckMatchResult.IsMatch;
			}
			throw new UserWrapperException((ErrorCode)userSsnCheckMatchResult.SoapErrorCode, userSsnCheckMatchResult.SoapErrorMessage, null);
		}

		public static bool VerifyUser(ServiceCode serviceCode, string nexonID, string password)
		{
			string pattern = "[\\x00-\\x08\\x0B-\\x0C\\x0E-\\x1F]";
			if (Regex.IsMatch(nexonID, pattern) || Regex.IsMatch(password, pattern))
			{
				throw new UserWrapperException(ErrorCode.Common_InvalidInputData, string.Empty, null);
			}
			UserVerifySoapResult userVerifySoapResult = new UserVerifySoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonID = nexonID,
				Password = password
			}.Execute();
			if (userVerifySoapResult.SoapErrorCode == 0)
			{
				return userVerifySoapResult.PwdMatch;
			}
			throw new UserWrapperException((ErrorCode)userVerifySoapResult.SoapErrorCode, userVerifySoapResult.SoapErrorMessage, null);
		}
	}
}
