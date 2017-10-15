using System;
using System.Collections.Generic;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserBasicGetListnRepresentSchoolResult : SoapResultBase
	{
		public List<UserBasicInfonRepresentSchoolInfo> UserBasicList { get; internal set; }

		public int TotalRowCount { get; internal set; }

		public bool AdminUser { get; internal set; }

		public UserBasicGetListnRepresentSchoolResult()
		{
			this.UserBasicList = new List<UserBasicInfonRepresentSchoolInfo>();
		}

		public void AddUserBasic(int? nexonSN, string nexonID, DateTime? createDate, DateTime? lastModifyDate, string nexonName, string name, string ssn, byte? sexCode, byte? age, bool? isTempBlockByLoginFail, bool? isTempBlockByWarning, bool? isTempUserChild, bool? isTempUserForeigner, bool? isTempUserCash, bool? isTempUserRealName, bool? isMainID, bool? isRealNameCfm, bool? isParentAuth, bool? isOwnerCfm, bool? isNotOwnerCfm, bool? isBlockByAdmin, bool? isTempPassword, bool? isModifyInfo_1, bool? isForeignerCfm, bool? isAlertState, bool? isCashParentAuth, bool? isCashParentReject, bool? isModifiedName, bool? isModifiedSsn, bool? isBackOfficeUser, bool? isSmsReceiveAgree, bool? isMobilePhoneAuth, bool? isRemovedByAdmin, bool? isForeigner, OpenConfigureCode? openConfigureName, OpenConfigureCode? openConfigureSex, OpenConfigureCode? openConfigureAge, OpenConfigureCode? openConfigureBirth, OpenConfigureCode? openConfigureSchool, OpenConfigureCode? openConfigureArea, OpenConfigureCode? openConfigurePhone, OpenConfigureCode? openConfigureMobilePhone, OpenConfigureCode? openConfigureEmail, byte? userCode, bool? isBlockCashRefillByAdmin, string email, string realBirth, byte? realBirthCode, DateTime? lastLoginTime, DateTime? prevLastLoginTime, byte? loginFailCount, DateTime? lastLoginFailTime, DateTime? lastPasswordChanged, byte? totalWarningPoint, DateTime? lastWarningTime, DateTime? blockEndDateByWarning, DateTime? maxEndWarning, bool? isHasSsn, bool? isExistUser, bool? isPwdHashMatch, bool? isBlockByEvent, int? schoolSN, SchoolCode? schoolCode, string schoolRealName)
		{
			if (this.UserBasicList == null)
			{
				this.UserBasicList = new List<UserBasicInfonRepresentSchoolInfo>();
			}
			this.UserBasicList.Add(new UserBasicInfonRepresentSchoolInfo(nexonSN, nexonID, createDate, lastModifyDate, nexonName, name, ssn, new SexCode?((SexCode)sexCode.Value), age, isTempBlockByLoginFail, isTempBlockByWarning, isTempUserChild, isTempUserForeigner, isTempUserCash, isTempUserRealName, isMainID, isRealNameCfm, isParentAuth, isOwnerCfm, isNotOwnerCfm, isBlockByAdmin, isTempPassword, isModifyInfo_1, isForeignerCfm, isAlertState, isCashParentAuth, isCashParentReject, isModifiedName, isModifiedSsn, isBackOfficeUser, isSmsReceiveAgree, isMobilePhoneAuth, isRemovedByAdmin, isForeigner, openConfigureName, openConfigureSex, openConfigureAge, openConfigureBirth, openConfigureSchool, openConfigureArea, openConfigurePhone, openConfigureMobilePhone, openConfigureEmail, userCode, isBlockCashRefillByAdmin, email, realBirth, new RealBirthCode?((RealBirthCode)realBirthCode.Value), lastLoginTime, prevLastLoginTime, loginFailCount, lastLoginFailTime, lastPasswordChanged, totalWarningPoint, lastWarningTime, blockEndDateByWarning, maxEndWarning, isHasSsn, isExistUser, isPwdHashMatch, isBlockByEvent, schoolSN, schoolCode, schoolRealName));
		}
	}
}
