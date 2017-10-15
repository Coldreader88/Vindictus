using System;
using System.Collections.Generic;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserGetListSoapResult : SoapResultBase
	{
		public List<UserInfo> UserList { get; internal set; }

		public int TotalRowCount { get; internal set; }

		public bool AdminUser { get; internal set; }

		public UserGetListSoapResult()
		{
			this.UserList = new List<UserInfo>();
		}

		public void AddUser(int? nexonSN, string nexonID, DateTime? createDate, DateTime? lastModifyDate, string nexonName, string name, string ssn, SexCode? sexCode, byte? age, bool? isTempBlockByLoginFail, bool? isTempBlockByWarning, bool? isTempUserChild, bool? isTempUserForeigner, bool? isTempUserCash, bool? isTempUserRealName, bool? isMainID, bool? isRealNameCfm, bool? isParentAuth, bool? isOwnerCfm, bool? isNotOwnerCfm, bool? isBlockByAdmin, bool? isTempPassword, bool? isModifyInfo_1, bool? isForeignerCfm, bool? isAlertState, bool? isCashParentAuth, bool? isCashParentReject, bool? isModifiedName, bool? isModifiedSsn, bool? isBackOfficeUser, bool? isSmsReceiveAgree, bool? isMobilePhoneAuth, bool? isRemovedByAdmin, bool? isForeigner, OpenConfigureCode? openConfigureName, OpenConfigureCode? openConfigureSex, OpenConfigureCode? openConfigureAge, OpenConfigureCode? openConfigureBirth, OpenConfigureCode? openConfigureSchool, OpenConfigureCode? openConfigureArea, OpenConfigureCode? openConfigurePhone, OpenConfigureCode? openConfigureMobilePhone, OpenConfigureCode? openConfigureEmail, UserCode? userCode, bool? isBlockCashRefillByAdmin, string email, string realBirth, RealBirthCode? realBirthCode, DateTime? lastLoginTime, DateTime? prevLastLoginTime, byte? loginFailCount, DateTime? lastLoginFailTime, DateTime? lastPasswordChanged, byte? totalWarningPoint, DateTime? lastWarningTime, DateTime? blockEndDateByWarning, DateTime? maxEndWarning, string areaCode, string address1, string address2, string phone, MobileCompanyCode? mobileCompanyCode, string mobilePhone, MainPageCode? mainPageCode, List<SchoolInfo> schoolList, bool? hasSsn, bool? isExistUser)
		{
			if (this.UserList == null)
			{
				this.UserList = new List<UserInfo>();
			}
			this.UserList.Add(new UserInfo(nexonSN, nexonID, createDate, lastModifyDate, nexonName, name, ssn, sexCode, age, isTempBlockByLoginFail, isTempBlockByWarning, isTempUserChild, isTempUserForeigner, isTempUserCash, isTempUserRealName, isMainID, isRealNameCfm, isParentAuth, isOwnerCfm, isNotOwnerCfm, isBlockByAdmin, isTempPassword, isModifyInfo_1, isForeignerCfm, isAlertState, isCashParentAuth, isCashParentReject, isModifiedName, isModifiedSsn, isBackOfficeUser, isSmsReceiveAgree, isMobilePhoneAuth, isRemovedByAdmin, isForeigner, openConfigureName, openConfigureSex, openConfigureAge, openConfigureBirth, openConfigureSchool, openConfigureArea, openConfigurePhone, openConfigureMobilePhone, openConfigureEmail, userCode, isBlockCashRefillByAdmin, email, realBirth, realBirthCode, lastLoginTime, prevLastLoginTime, loginFailCount, lastLoginFailTime, lastPasswordChanged, totalWarningPoint, lastWarningTime, blockEndDateByWarning, maxEndWarning, areaCode, address1, address2, phone, mobileCompanyCode, mobilePhone, mainPageCode, schoolList, hasSsn, isExistUser, null, null));
		}
	}
}
