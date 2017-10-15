using System;
using System.Collections.Generic;

namespace Nexon.Com.UserWrapper.P2
{
	public static class P2
	{
		public static UserInfo GetUserInfo(ServiceCode serviceCode, int nexonSN)
		{
			UserGetListSoapResult userGetListSoapResult = new UserGetInfoSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN
			}.Execute();
			if (userGetListSoapResult.SoapErrorCode != 0)
			{
				throw new UserWrapperException((ErrorCode)userGetListSoapResult.SoapErrorCode, userGetListSoapResult.SoapErrorMessage, null);
			}
			if (userGetListSoapResult.UserList.Count == 1)
			{
				return userGetListSoapResult.UserList[0];
			}
			return null;
		}

		public static UserInfo GetUserNexonSN_ByNexonName(ServiceCode serviceCode, string nexonName)
		{
			UserGetListSoapResult userGetListSoapResult = new UserGetNexonSNByNexonNameSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonName = nexonName
			}.Execute();
			if (userGetListSoapResult.SoapErrorCode != 0)
			{
				throw new UserWrapperException((ErrorCode)userGetListSoapResult.SoapErrorCode, userGetListSoapResult.SoapErrorMessage, null);
			}
			if (userGetListSoapResult.UserList.Count == 1)
			{
				return userGetListSoapResult.UserList[0];
			}
			return null;
		}

		public static UserInfo GetUserNexonSN_ByNexonID(ServiceCode serviceCode, string nexonID)
		{
			UserGetListSoapResult userGetListSoapResult = new UserGetNexonSNByNexonIDSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonID = nexonID
			}.Execute();
			if (userGetListSoapResult.SoapErrorCode != 0)
			{
				throw new UserWrapperException((ErrorCode)userGetListSoapResult.SoapErrorCode, userGetListSoapResult.SoapErrorMessage, null);
			}
			if (userGetListSoapResult.UserList.Count == 1)
			{
				return userGetListSoapResult.UserList[0];
			}
			return null;
		}

		public static bool CheckNexonName(ServiceCode serviceCode, int nexonSN, string nexonName, bool isCheckChangeCount)
		{
			UserCheckNexonNameSoapResult userCheckNexonNameSoapResult = new UserCheckNexonNameSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				NexonName = nexonName,
				IsCheckChangeCount = isCheckChangeCount
			}.Execute();
			if (userCheckNexonNameSoapResult.SoapErrorCode == 0)
			{
				return userCheckNexonNameSoapResult.IsUsable;
			}
			throw new UserWrapperException((ErrorCode)userCheckNexonNameSoapResult.SoapErrorCode, userCheckNexonNameSoapResult.SoapErrorMessage, null);
		}

		public static bool ModifyNexonName(ServiceCode serviceCode, int nexonSN, string nexonName, bool isCheckChangeCount, NexonNameChangeUseCode nexonNameChangeUseCode)
		{
			UserModifyNexonNameSoapResult userModifyNexonNameSoapResult = new UserModifyNexonNameSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				NexonName = nexonName,
				IsCheckChangeCount = isCheckChangeCount,
				NexonNameChangeUseCode = (byte)nexonNameChangeUseCode
			}.Execute();
			if (userModifyNexonNameSoapResult.SoapErrorCode == 0)
			{
				return userModifyNexonNameSoapResult.IsSuccess;
			}
			throw new UserWrapperException((ErrorCode)userModifyNexonNameSoapResult.SoapErrorCode, userModifyNexonNameSoapResult.SoapErrorMessage, null);
		}

		public static bool ModifyRealBirth(ServiceCode serviceCode, int nexonSN, string realBirthYear, string realBirthMonth, string realBirthDay, RealBirthCode realBirthCode)
		{
			UserModifyRealBirthSoapResult userModifyRealBirthSoapResult = new UserModifyRealBirthSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				RealBirthYear = realBirthYear,
				RealBirthMonth = realBirthMonth,
				RealBirthDay = realBirthDay,
				RealBirthCode = (byte)realBirthCode
			}.Execute();
			if (userModifyRealBirthSoapResult.SoapErrorCode == 0)
			{
				return userModifyRealBirthSoapResult.IsSuccess;
			}
			throw new UserWrapperException((ErrorCode)userModifyRealBirthSoapResult.SoapErrorCode, userModifyRealBirthSoapResult.SoapErrorMessage, null);
		}

		public static bool ModifySchoolInfo(ServiceCode serviceCode, int nexonSN, int mainSchoolSN, List<SchoolInfo> schoolList)
		{
			string text = string.Empty;
			if (schoolList != null && schoolList.Count > 5)
			{
				throw new UserWrapperException(ErrorCode.UserInfo_InvalidSchoolInfo, string.Empty, null);
			}
			int[][] array = null;
			if (schoolList != null)
			{
				array = new int[schoolList.Count][];
				int num = 0;
				foreach (SchoolInfo schoolInfo in schoolList)
				{
					if (schoolInfo.SchoolSN != 0)
					{
						if (text.IndexOf(string.Format("{0};", schoolInfo.SchoolSN)) != -1)
						{
							throw new UserWrapperException(ErrorCode.UserInfo_InvalidSchoolInfo, string.Empty, null);
						}
						array[num] = new int[]
						{
							schoolInfo.SchoolSN,
							Convert.ToInt32(schoolInfo.SchoolCode)
						};
						text += string.Format("{0};", schoolInfo.SchoolSN);
						num++;
					}
				}
			}
			UserModifyMainPageCodeSoapResult userModifyMainPageCodeSoapResult = new UserModifySchoolInfoSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				SchoolSN = mainSchoolSN,
				SchoolSNList = array
			}.Execute();
			if (userModifyMainPageCodeSoapResult.SoapErrorCode == 0)
			{
				return userModifyMainPageCodeSoapResult.IsSuccess;
			}
			throw new UserWrapperException((ErrorCode)userModifyMainPageCodeSoapResult.SoapErrorCode, userModifyMainPageCodeSoapResult.SoapErrorMessage, null);
		}

		public static bool ModifyMainPageCode(ServiceCode serviceCode, int nexonSN, MainPageCode mainPageCode)
		{
			UserModifyMainPageCodeSoapResult userModifyMainPageCodeSoapResult = new UserModifyMainPageCodeSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				MainPageCode = (byte)mainPageCode
			}.Execute();
			if (userModifyMainPageCodeSoapResult.SoapErrorCode == 0)
			{
				return userModifyMainPageCodeSoapResult.IsSuccess;
			}
			throw new UserWrapperException((ErrorCode)userModifyMainPageCodeSoapResult.SoapErrorCode, userModifyMainPageCodeSoapResult.SoapErrorMessage, null);
		}

		public static bool ModifyOpenConfigure(ServiceCode serviceCode, int nexonSN, OpenConfigureCode openConfigure_Birth, OpenConfigureCode openConfigure_Name, OpenConfigureCode openConfigure_Sex, OpenConfigureCode openConfigure_Area, OpenConfigureCode openConfigure_Email, OpenConfigureCode openConfigure_Phone, OpenConfigureCode openConfigure_School)
		{
			UserModifyOpenConfigureSoapResult userModifyOpenConfigureSoapResult = new UserModifyOpenConfigureSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				OpenConfigure_Birth = (byte)openConfigure_Birth,
				OpenConfigure_Name = (byte)openConfigure_Name,
				OpenConfigure_Sex = (byte)openConfigure_Sex,
				OpenConfigure_Area = (byte)openConfigure_Area,
				OpenConfigure_Email = (byte)openConfigure_Email,
				OpenConfigure_Phone = (byte)openConfigure_Phone,
				OpenConfigure_School = (byte)openConfigure_School
			}.Execute();
			if (userModifyOpenConfigureSoapResult.SoapErrorCode == 0)
			{
				return userModifyOpenConfigureSoapResult.IsSuccess;
			}
			throw new UserWrapperException((ErrorCode)userModifyOpenConfigureSoapResult.SoapErrorCode, userModifyOpenConfigureSoapResult.SoapErrorMessage, null);
		}

		public static bool CheckValidNexonIDnPassword(ServiceCode serviceCode, string nexonID, string password)
		{
			UserCheckValidNexonIDnPasswordSoapResult userCheckValidNexonIDnPasswordSoapResult = new UserCheckValidNexonIDnPasswordSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonID = nexonID,
				Password = password
			}.Execute();
			if (userCheckValidNexonIDnPasswordSoapResult.SoapErrorCode == 0)
			{
				return userCheckValidNexonIDnPasswordSoapResult.Enable;
			}
			throw new UserWrapperException((ErrorCode)userCheckValidNexonIDnPasswordSoapResult.SoapErrorCode, userCheckValidNexonIDnPasswordSoapResult.SoapErrorMessage, null);
		}

		public static bool GetUserIdentitySN_Event(ServiceCode n4ServiceCode, int nexonSN, string nexonName_Recommended, out long identitySN, out long identitySN_Recommended, out int nexonSN_Recommended)
		{
			identitySN = 0L;
			identitySN_Recommended = 0L;
			nexonSN_Recommended = 0;
			UserGetIdentitySNEventSoapResult userGetIdentitySNEventSoapResult = new UserGetIdentitySNEventSoapWrapper
			{
				ServiceCode = (int)n4ServiceCode,
				NexonSN = nexonSN,
				NexonName_Recommended = nexonName_Recommended
			}.Execute();
			if (userGetIdentitySNEventSoapResult.SoapErrorCode == 0)
			{
				identitySN = userGetIdentitySNEventSoapResult.IdentitySN;
				identitySN_Recommended = userGetIdentitySNEventSoapResult.IdentitySN_Recommended;
				nexonSN_Recommended = userGetIdentitySNEventSoapResult.NexonSN_Recommended;
				return userGetIdentitySNEventSoapResult.ValidUser;
			}
			throw new UserWrapperException((ErrorCode)userGetIdentitySNEventSoapResult.SoapErrorCode, userGetIdentitySNEventSoapResult.SoapErrorMessage, null);
		}

		public static WriteStatusCode GetWriteStatusCode(ServiceCode n4ServiceCode, int n4NexonSN)
		{
			if (n4NexonSN == 0)
			{
				throw new UserWrapperException(ErrorCode.Common_InvalidInputData, string.Empty, null);
			}
			UserWriteStatusGetInfoSoapResult userWriteStatusGetInfoSoapResult = new UserWriteStatusGetInfoSoapWrapper
			{
				ServiceCode = (int)n4ServiceCode,
				NexonSN = n4NexonSN
			}.Execute();
			if (userWriteStatusGetInfoSoapResult.SoapErrorCode == 0)
			{
				n4NexonSN = userWriteStatusGetInfoSoapResult.NexonSN;
				return userWriteStatusGetInfoSoapResult.WriteStatusCode;
			}
			throw new UserWrapperException((ErrorCode)userWriteStatusGetInfoSoapResult.SoapErrorCode, userWriteStatusGetInfoSoapResult.SoapErrorMessage, null);
		}
	}
}
