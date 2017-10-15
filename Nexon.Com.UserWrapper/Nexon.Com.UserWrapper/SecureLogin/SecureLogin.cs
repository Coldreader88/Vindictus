using System;
using System.Collections.Generic;

namespace Nexon.Com.UserWrapper.SecureLogin
{
	public static class SecureLogin
	{
		public static bool VerifyUser(ServiceCode serviceCode, string strNexonID, string strPassword)
		{
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

		public static UserBasicInfo GetUserBasicInfo(ServiceCode serviceCode, int n4NexonSN)
		{
			UserBasicGetListSoapResult userBasicGetListSoapResult = new UserBasicGetInfoSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = n4NexonSN
			}.Execute();
			if (userBasicGetListSoapResult.SoapErrorCode == 0 && userBasicGetListSoapResult.UserBasicList.Count == 1)
			{
				return userBasicGetListSoapResult.UserBasicList[0];
			}
			return null;
		}

		public static UserBasicInfo GetUserBasicInfo(ServiceCode serviceCode, string strNexonID)
		{
			UserBasicGetListSoapResult userBasicGetListSoapResult = new UserBasicGetInfoSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonID = strNexonID
			}.Execute();
			if (userBasicGetListSoapResult.SoapErrorCode == 0 && userBasicGetListSoapResult.UserBasicList.Count == 1)
			{
				return userBasicGetListSoapResult.UserBasicList[0];
			}
			return null;
		}

		public static List<UserBasicInfo> GetUserBasicList(out int n4TotalRowCount, out bool isAdminUser, ServiceCode serviceCode, string strSsn, bool isTempUserInclude)
		{
			UserBasicGetListSoapResult userBasicGetListSoapResult = new UserBasicGetListSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				Ssn = strSsn,
				TempUserInclude = isTempUserInclude
			}.Execute();
			if (userBasicGetListSoapResult.SoapErrorCode == 0)
			{
				n4TotalRowCount = userBasicGetListSoapResult.TotalRowCount;
				isAdminUser = userBasicGetListSoapResult.AdminUser;
				return userBasicGetListSoapResult.UserBasicList;
			}
			n4TotalRowCount = 0;
			isAdminUser = false;
			return null;
		}

		public static int SendMobileAuthNum(out long n8AuthLogSN, out byte n1AuthPGCode, out string strPccSeq, out string strAuthSeq, ServiceCode serviceCode, byte n1MobileCompanyCode, string strMobilePhone1, string strMobilePhone2, string strMobilePhone3, string strSsn, string strName, int n4NexonSN, string strClientIP)
		{
			UserSendMobileAuthSoapResult userSendMobileAuthSoapResult = new UserSendMobileAuthSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				MobileCompanyCode = n1MobileCompanyCode,
				MobilePhone1 = strMobilePhone1,
				MobilePhone2 = strMobilePhone2,
				MobilePhone3 = strMobilePhone3,
				Ssn = strSsn,
				Name = strName,
				NexonSN = n4NexonSN,
				ClientIP = strClientIP
			}.Execute();
			if (userSendMobileAuthSoapResult.SoapErrorCode == 0)
			{
				n8AuthLogSN = userSendMobileAuthSoapResult.AuthLogSN;
				n1AuthPGCode = userSendMobileAuthSoapResult.AuthPGCode;
				strPccSeq = userSendMobileAuthSoapResult.PccSeq;
				strAuthSeq = userSendMobileAuthSoapResult.AuthSeq;
				return 0;
			}
			n8AuthLogSN = 0L;
			n1AuthPGCode = 0;
			strPccSeq = string.Empty;
			strAuthSeq = string.Empty;
			return userSendMobileAuthSoapResult.SoapErrorCode;
		}

		public static bool ConfirmMobileAuthNum(ServiceCode serviceCode, long n8AuthLogSN, string strInputData)
		{
			UserConfirmMobileAuthSoapResult userConfirmMobileAuthSoapResult = new UserConfirmMobileAuthSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				AuthLogSN = n8AuthLogSN,
				InputData = strInputData
			}.Execute();
			if (userConfirmMobileAuthSoapResult.SoapErrorCode == 0)
			{
				return userConfirmMobileAuthSoapResult.Success;
			}
			throw new UserWrapperException((ErrorCode)userConfirmMobileAuthSoapResult.SoapErrorCode, userConfirmMobileAuthSoapResult.SoapErrorMessage, null);
		}

		public static bool CheckEnableMobileAuth(ServiceCode serviceCode, string strMobilePhone1, string strMobilePhone2, string strMobilePhone3)
		{
			UserCheckEnableMobileAuthSoapResult userCheckEnableMobileAuthSoapResult = new UserCheckEnableMobileAuthSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				MobilePhone1 = strMobilePhone1,
				MobilePhone2 = strMobilePhone2,
				MobilePhone3 = strMobilePhone3
			}.Execute();
			if (userCheckEnableMobileAuthSoapResult.SoapErrorCode == 0)
			{
				return userCheckEnableMobileAuthSoapResult.Enable;
			}
			throw new UserWrapperException((ErrorCode)userCheckEnableMobileAuthSoapResult.SoapErrorCode, userCheckEnableMobileAuthSoapResult.SoapErrorMessage, null);
		}

		public static string EncryptCertReqInfo(ServiceCode serviceCode, string strName, string strSsn)
		{
			UserEncryptCertReqInfoSoapResult userEncryptCertReqInfoSoapResult = new UserEncryptCertReqInfoSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				Name = strName,
				Ssn = strSsn
			}.Execute();
			if (userEncryptCertReqInfoSoapResult.SoapErrorCode == 0)
			{
				return userEncryptCertReqInfoSoapResult.EncryptData;
			}
			throw new UserWrapperException((ErrorCode)userEncryptCertReqInfoSoapResult.SoapErrorCode, userEncryptCertReqInfoSoapResult.SoapErrorMessage, null);
		}

		public static bool CheckAuthLogSN(ServiceCode serviceCode, long n8AuthLogSN, int n4NexonSN)
		{
			UserCheckAuthLogSNSoapResult userCheckAuthLogSNSoapResult = new UserCheckAuthLogSNSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = n4NexonSN,
				AuthLogSN = n8AuthLogSN
			}.Execute();
			if (userCheckAuthLogSNSoapResult.SoapErrorCode == 0)
			{
				return userCheckAuthLogSNSoapResult.Valid;
			}
			throw new UserWrapperException((ErrorCode)userCheckAuthLogSNSoapResult.SoapErrorCode, userCheckAuthLogSNSoapResult.SoapErrorMessage, null);
		}
	}
}
