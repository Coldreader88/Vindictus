using System;
using System.Collections.Generic;

namespace Nexon.Com.UserWrapper
{
	public class UserInfo
	{
		public UserInfo(int? nexonSN, string nexonID, DateTime? createDate, DateTime? lastModifyDate, string nexonName, string name, string ssn, SexCode? sexCode, byte? age, bool? isTempBlockByLoginFail, bool? isTempBlockByWarning, bool? isTempUserChild, bool? isTempUserForeigner, bool? isTempUserCash, bool? isTempUserRealName, bool? isMainID, bool? isRealNameCfm, bool? isParentAuth, bool? isOwnerCfm, bool? isNotOwnerCfm, bool? isBlockByAdmin, bool? isTempPassword, bool? isModifyInfo_1, bool? isForeignerCfm, bool? isAlertState, bool? isCashParentAuth, bool? isCashParentReject, bool? isModifiedName, bool? isModifiedSsn, bool? isBackOfficeUser, bool? isSmsReceiveAgree, bool? isMobilePhoneAuth, bool? isRemovedByAdmin, bool? isForeigner, OpenConfigureCode? openConfigureName, OpenConfigureCode? openConfigureSex, OpenConfigureCode? openConfigureAge, OpenConfigureCode? openConfigureBirth, OpenConfigureCode? openConfigureSchool, OpenConfigureCode? openConfigureArea, OpenConfigureCode? openConfigurePhone, OpenConfigureCode? openConfigureMobilePhone, OpenConfigureCode? openConfigureEmail, UserCode? userCode, bool? isBlockCashRefillByAdmin, string email, string realBirth, RealBirthCode? realBirthCode, DateTime? lastLoginTime, DateTime? prevLastLoginTime, byte? loginFailCount, DateTime? lastLoginFailTime, DateTime? lastPasswordChanged, byte? totalWarningPoint, DateTime? lastWarningTime, DateTime? blockEndDateByWarning, DateTime? maxEndWarning, string areaCode, string address1, string address2, string phone, MobileCompanyCode? mobileCompanyCode, string mobilePhone, MainPageCode? mainPageCode, List<SchoolInfo> schoolList, bool? hasSsn, bool? isExistUser, bool? isPwdHashMatch, bool? isBlockByEvent)
		{
			this._n4NexonSN = nexonSN;
			this._strNexonID = nexonID;
			this._dtCreateDate = createDate;
			this._dtLastModifyDate = lastModifyDate;
			this._strNexonName = nexonName;
			this._strName = name;
			this._strSsn = ssn;
			this._SexCode = sexCode;
			this._n1Age = age;
			this._isTempBlockByLoginFail = isTempBlockByLoginFail;
			this._isTempBlockByWarning = isTempBlockByWarning;
			this._isTempUserChild = isTempUserChild;
			this._isTempUserForeigner = isTempUserForeigner;
			this._isTempUserCash = isTempUserCash;
			this._isTempUserRealName = isTempUserRealName;
			this._isMainID = isMainID;
			this._isRealNameCfm = isRealNameCfm;
			this._isParentAuth = isParentAuth;
			this._isOwnerCfm = isOwnerCfm;
			this._isNotOwnerCfm = isNotOwnerCfm;
			this._isBlockByAdmin = isBlockByAdmin;
			this._isTempPassword = isTempPassword;
			this._isModifyInfo_1 = isModifyInfo_1;
			this._isForeignerCfm = isForeignerCfm;
			this._isAlertState = isAlertState;
			this._isCashParentAuth = isCashParentAuth;
			this._isCashParentReject = isCashParentReject;
			this._isModifiedName = isModifiedName;
			this._isModifiedSsn = isModifiedSsn;
			this._isBackOfficeUser = isBackOfficeUser;
			this._isSmsReceiveAgree = isSmsReceiveAgree;
			this._isMobilePhoneAuth = isMobilePhoneAuth;
			this._isRemovedByAdmin = isRemovedByAdmin;
			this._isForeigner = isForeigner;
			this._n1OpenConfigureName = openConfigureName;
			this._n1OpenConfigureSex = openConfigureSex;
			this._n1OpenConfigureAge = openConfigureAge;
			this._n1OpenConfigureBirth = openConfigureBirth;
			this._n1OpenConfigureSchool = openConfigureSchool;
			this._n1OpenConfigureArea = openConfigureArea;
			this._n1OpenConfigurePhone = openConfigurePhone;
			this._n1OpenConfigureMobilePhone = openConfigureMobilePhone;
			this._n1OpenConfigureEmail = openConfigureEmail;
			this._UserCode = userCode;
			this._isBlockCashRefillByAdmin = isBlockCashRefillByAdmin;
			this._strEmail = email;
			this._strRealBirth = realBirth;
			this._RealBirthCode = realBirthCode;
			this._dtLastLoginTime = lastLoginTime;
			this._dtPrevLastLoginTime = prevLastLoginTime;
			this._n1LoginFailCount = loginFailCount;
			this._dtLastLoginFailTime = lastLoginFailTime;
			this._dtLastPasswordChanged = lastPasswordChanged;
			this._n1TotalWarningPoint = totalWarningPoint;
			this._dtLastWarningTime = lastWarningTime;
			this._dtBlockEndDateByWarning = blockEndDateByWarning;
			this._dtMaxEndWarning = maxEndWarning;
			this._strAreaCode = areaCode;
			this._strAddress1 = address1;
			this._strAddress2 = address2;
			this._strPhone = phone;
			this._MobileCompanyCode = mobileCompanyCode;
			this._strMobilePhone = mobilePhone;
			this._MainPageCode = mainPageCode;
			this._schoolList = schoolList;
			this._hasSsn = hasSsn;
			this._isExistUser = isExistUser;
			this._isPwdHashMatch = isPwdHashMatch;
			this._isBlockByEvent = isBlockByEvent;
		}

		public int NexonSN
		{
			get
			{
				int value;
				try
				{
					value = this._n4NexonSN.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n4NexonSN = new int?(value);
			}
		}

		public string NexonID
		{
			get
			{
				string strNexonID;
				try
				{
					strNexonID = this._strNexonID;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strNexonID;
			}
			internal set
			{
				this._strNexonID = value;
			}
		}

		public DateTime CreateDate
		{
			get
			{
				DateTime value;
				try
				{
					value = this._dtCreateDate.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._dtCreateDate = new DateTime?(value);
			}
		}

		public DateTime LastModifyDate
		{
			get
			{
				DateTime value;
				try
				{
					value = this._dtLastModifyDate.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._dtLastModifyDate = new DateTime?(value);
			}
		}

		public string NexonName
		{
			get
			{
				string strNexonName;
				try
				{
					strNexonName = this._strNexonName;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strNexonName;
			}
			internal set
			{
				this._strNexonName = value;
			}
		}

		public string Name
		{
			get
			{
				string strName;
				try
				{
					strName = this._strName;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strName;
			}
			internal set
			{
				this._strName = value;
			}
		}

		public string Ssn
		{
			get
			{
				string strSsn;
				try
				{
					strSsn = this._strSsn;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strSsn;
			}
			internal set
			{
				this._strSsn = value;
			}
		}

		public SexCode SexCode
		{
			get
			{
				SexCode value;
				try
				{
					value = this._SexCode.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._SexCode = new SexCode?(value);
			}
		}

		public byte Age
		{
			get
			{
				byte value;
				try
				{
					value = this._n1Age.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1Age = new byte?(value);
			}
		}

		public bool IsTempBlockByLoginFail
		{
			get
			{
				bool value;
				try
				{
					value = this._isTempBlockByLoginFail.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isTempBlockByLoginFail = new bool?(value);
			}
		}

		public bool IsTempBlockByWarning
		{
			get
			{
				bool value;
				try
				{
					value = this._isTempBlockByWarning.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isTempBlockByWarning = new bool?(value);
			}
		}

		public bool IsTempUserChild
		{
			get
			{
				bool value;
				try
				{
					value = this._isTempUserChild.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isTempUserChild = new bool?(value);
			}
		}

		public bool IsTempUserForeigner
		{
			get
			{
				bool value;
				try
				{
					value = this._isTempUserForeigner.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isTempUserForeigner = new bool?(value);
			}
		}

		public bool IsTempUserCash
		{
			get
			{
				bool value;
				try
				{
					value = this._isTempUserCash.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isTempUserCash = new bool?(value);
			}
		}

		public bool IsTempUserRealName
		{
			get
			{
				bool value;
				try
				{
					value = this._isTempUserRealName.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isTempUserRealName = new bool?(value);
			}
		}

		public bool IsMainID
		{
			get
			{
				bool value;
				try
				{
					value = this._isMainID.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isMainID = new bool?(value);
			}
		}

		public bool IsRealNameCfm
		{
			get
			{
				bool value;
				try
				{
					value = this._isRealNameCfm.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isRealNameCfm = new bool?(value);
			}
		}

		public bool IsParentAuth
		{
			get
			{
				bool value;
				try
				{
					value = this._isParentAuth.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isParentAuth = new bool?(value);
			}
		}

		public bool IsOwnerCfm
		{
			get
			{
				bool value;
				try
				{
					value = this._isOwnerCfm.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isOwnerCfm = new bool?(value);
			}
		}

		public bool IsNotOwnerCfm
		{
			get
			{
				bool value;
				try
				{
					value = this._isNotOwnerCfm.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isNotOwnerCfm = new bool?(value);
			}
		}

		public bool IsBlockByAdmin
		{
			get
			{
				bool value;
				try
				{
					value = this._isBlockByAdmin.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isBlockByAdmin = new bool?(value);
			}
		}

		public bool IsTempPassword
		{
			get
			{
				bool value;
				try
				{
					value = this._isTempPassword.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isTempPassword = new bool?(value);
			}
		}

		public bool IsModifyInfo_1
		{
			get
			{
				bool value;
				try
				{
					value = this._isModifyInfo_1.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isModifyInfo_1 = new bool?(value);
			}
		}

		public bool IsForeignerCfm
		{
			get
			{
				bool value;
				try
				{
					value = this._isForeignerCfm.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isForeignerCfm = new bool?(value);
			}
		}

		public bool IsAlertState
		{
			get
			{
				bool value;
				try
				{
					value = this._isAlertState.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isAlertState = new bool?(value);
			}
		}

		public bool IsCashParentAuth
		{
			get
			{
				bool value;
				try
				{
					value = this._isCashParentAuth.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isCashParentAuth = new bool?(value);
			}
		}

		public bool IsCashParentReject
		{
			get
			{
				bool value;
				try
				{
					value = this._isCashParentReject.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isCashParentReject = new bool?(value);
			}
		}

		public bool IsModifiedName
		{
			get
			{
				bool value;
				try
				{
					value = this._isModifiedName.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isModifiedName = new bool?(value);
			}
		}

		public bool IsModifiedSsn
		{
			get
			{
				bool value;
				try
				{
					value = this._isModifiedSsn.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isModifiedSsn = new bool?(value);
			}
		}

		public bool IsBackOfficeUser
		{
			get
			{
				bool value;
				try
				{
					value = this._isBackOfficeUser.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isBackOfficeUser = new bool?(value);
			}
		}

		public bool IsSmsReceiveAgree
		{
			get
			{
				bool value;
				try
				{
					value = this._isSmsReceiveAgree.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isSmsReceiveAgree = new bool?(value);
			}
		}

		public bool IsMobilePhoneAuth
		{
			get
			{
				bool value;
				try
				{
					value = this._isMobilePhoneAuth.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isMobilePhoneAuth = new bool?(value);
			}
		}

		public bool IsRemovedByAdmin
		{
			get
			{
				bool value;
				try
				{
					value = this._isRemovedByAdmin.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isRemovedByAdmin = new bool?(value);
			}
		}

		public bool IsForeigner
		{
			get
			{
				bool value;
				try
				{
					value = this._isForeigner.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isForeigner = new bool?(value);
			}
		}

		public OpenConfigureCode OpenConfigureName
		{
			get
			{
				OpenConfigureCode value;
				try
				{
					value = this._n1OpenConfigureName.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1OpenConfigureName = new OpenConfigureCode?(value);
			}
		}

		public OpenConfigureCode OpenConfigureSex
		{
			get
			{
				OpenConfigureCode value;
				try
				{
					value = this._n1OpenConfigureSex.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1OpenConfigureSex = new OpenConfigureCode?(value);
			}
		}

		public OpenConfigureCode OpenConfigureAge
		{
			get
			{
				OpenConfigureCode value;
				try
				{
					value = this._n1OpenConfigureAge.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1OpenConfigureAge = new OpenConfigureCode?(value);
			}
		}

		public OpenConfigureCode OpenConfigureBirth
		{
			get
			{
				OpenConfigureCode value;
				try
				{
					value = this._n1OpenConfigureBirth.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1OpenConfigureBirth = new OpenConfigureCode?(value);
			}
		}

		public OpenConfigureCode OpenConfigureSchool
		{
			get
			{
				OpenConfigureCode value;
				try
				{
					value = this._n1OpenConfigureSchool.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1OpenConfigureSchool = new OpenConfigureCode?(value);
			}
		}

		public OpenConfigureCode OpenConfigureArea
		{
			get
			{
				OpenConfigureCode value;
				try
				{
					value = this._n1OpenConfigureArea.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1OpenConfigureArea = new OpenConfigureCode?(value);
			}
		}

		public OpenConfigureCode OpenConfigurePhone
		{
			get
			{
				OpenConfigureCode value;
				try
				{
					value = this._n1OpenConfigurePhone.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1OpenConfigurePhone = new OpenConfigureCode?(value);
			}
		}

		public OpenConfigureCode OpenConfigureMobilePhone
		{
			get
			{
				OpenConfigureCode value;
				try
				{
					value = this._n1OpenConfigureMobilePhone.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1OpenConfigureMobilePhone = new OpenConfigureCode?(value);
			}
		}

		public OpenConfigureCode OpenConfigureEmail
		{
			get
			{
				OpenConfigureCode value;
				try
				{
					value = this._n1OpenConfigureEmail.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1OpenConfigureEmail = new OpenConfigureCode?(value);
			}
		}

		public UserCode UserCode
		{
			get
			{
				UserCode value;
				try
				{
					value = this._UserCode.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._UserCode = new UserCode?(value);
			}
		}

		public bool IsBlockCashRefillByAdmin
		{
			get
			{
				bool value;
				try
				{
					value = this._isBlockCashRefillByAdmin.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._isBlockCashRefillByAdmin = new bool?(value);
			}
		}

		public string Email
		{
			get
			{
				string strEmail;
				try
				{
					strEmail = this._strEmail;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strEmail;
			}
			internal set
			{
				this._strEmail = value;
			}
		}

		public string RealBirth
		{
			get
			{
				string strRealBirth;
				try
				{
					strRealBirth = this._strRealBirth;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strRealBirth;
			}
			internal set
			{
				this._strRealBirth = value;
			}
		}

		public RealBirthCode RealBirthCode
		{
			get
			{
				RealBirthCode value;
				try
				{
					value = this._RealBirthCode.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._RealBirthCode = new RealBirthCode?(value);
			}
		}

		public DateTime LastLoginTime
		{
			get
			{
				DateTime value;
				try
				{
					value = this._dtLastModifyDate.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._dtLastModifyDate = new DateTime?(value);
			}
		}

		public DateTime PrevLastLoginTime
		{
			get
			{
				DateTime value;
				try
				{
					value = this._dtLastModifyDate.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._dtLastModifyDate = new DateTime?(value);
			}
		}

		public byte LoginFailCount
		{
			get
			{
				byte value;
				try
				{
					value = this._n1LoginFailCount.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1LoginFailCount = new byte?(value);
			}
		}

		public DateTime LastLoginFailTime
		{
			get
			{
				DateTime value;
				try
				{
					value = this._dtLastLoginFailTime.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._dtLastLoginFailTime = new DateTime?(value);
			}
		}

		public DateTime LastPasswordChanged
		{
			get
			{
				DateTime value;
				try
				{
					value = this._dtLastPasswordChanged.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._dtLastPasswordChanged = new DateTime?(value);
			}
		}

		public byte TotalWarningPoint
		{
			get
			{
				byte value;
				try
				{
					value = this._n1TotalWarningPoint.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._n1TotalWarningPoint = new byte?(value);
			}
		}

		public DateTime LastWarningTime
		{
			get
			{
				DateTime value;
				try
				{
					value = this._dtLastWarningTime.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._dtLastWarningTime = new DateTime?(value);
			}
		}

		public DateTime BlockEndDateByWarning
		{
			get
			{
				DateTime value;
				try
				{
					value = this._dtBlockEndDateByWarning.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._dtBlockEndDateByWarning = new DateTime?(value);
			}
		}

		public DateTime MaxEndWarning
		{
			get
			{
				DateTime value;
				try
				{
					value = this._dtMaxEndWarning.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._dtMaxEndWarning = new DateTime?(value);
			}
		}

		public string AreaCode
		{
			get
			{
				string strAreaCode;
				try
				{
					strAreaCode = this._strAreaCode;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strAreaCode;
			}
			internal set
			{
				this._strAreaCode = value;
			}
		}

		public string Address1
		{
			get
			{
				string strAddress;
				try
				{
					strAddress = this._strAddress1;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strAddress;
			}
			internal set
			{
				this._strAddress1 = value;
			}
		}

		public string Address2
		{
			get
			{
				string strAddress;
				try
				{
					strAddress = this._strAddress2;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strAddress;
			}
			internal set
			{
				this._strAddress2 = value;
			}
		}

		public string Phone
		{
			get
			{
				string strPhone;
				try
				{
					strPhone = this._strPhone;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strPhone;
			}
			internal set
			{
				this._strPhone = value;
			}
		}

		public MobileCompanyCode MobileCompanyCode
		{
			get
			{
				MobileCompanyCode value;
				try
				{
					value = this._MobileCompanyCode.Value;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return value;
			}
			internal set
			{
				this._MobileCompanyCode = new MobileCompanyCode?(value);
			}
		}

		public string MobilePhone
		{
			get
			{
				string strMobilePhone;
				try
				{
					strMobilePhone = this._strMobilePhone;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strMobilePhone;
			}
			internal set
			{
				this._strMobilePhone = value;
			}
		}

		public MainPageCode? MainPageCode
		{
			get
			{
				MainPageCode? result;
				try
				{
					result = new MainPageCode?(this._MainPageCode.Value);
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return result;
			}
			internal set
			{
				this._MainPageCode = value;
			}
		}

		public List<SchoolInfo> SchoolList
		{
			get
			{
				List<SchoolInfo> schoolList;
				try
				{
					schoolList = this._schoolList;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return schoolList;
			}
			internal set
			{
				this._schoolList = value;
			}
		}

		public bool? HasSsn
		{
			get
			{
				bool? result;
				try
				{
					result = new bool?(this._hasSsn.Value);
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return result;
			}
			internal set
			{
				this._hasSsn = value;
			}
		}

		public bool? IsExistUser
		{
			get
			{
				bool? result;
				try
				{
					result = new bool?(this._isExistUser.Value);
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return result;
			}
			internal set
			{
				this._isExistUser = value;
			}
		}

		public bool? IsPwdHashMatch
		{
			get
			{
				bool? result;
				try
				{
					result = new bool?(this._isPwdHashMatch.Value);
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return result;
			}
			internal set
			{
				this._isPwdHashMatch = value;
			}
		}

		public bool? IsBlockByEvent
		{
			get
			{
				bool? result;
				try
				{
					result = new bool?(this._isBlockByEvent.Value);
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return result;
			}
			internal set
			{
				this._isBlockByEvent = value;
			}
		}

		private int? _n4NexonSN;

		private string _strNexonID;

		private DateTime? _dtCreateDate;

		private DateTime? _dtLastModifyDate;

		private string _strNexonName;

		private string _strName;

		private string _strSsn;

		private SexCode? _SexCode;

		private byte? _n1Age;

		private bool? _isTempBlockByLoginFail;

		private bool? _isTempBlockByWarning;

		private bool? _isTempUserChild;

		private bool? _isTempUserForeigner;

		private bool? _isTempUserCash;

		private bool? _isTempUserRealName;

		private bool? _isMainID;

		private bool? _isRealNameCfm;

		private bool? _isParentAuth;

		private bool? _isOwnerCfm;

		private bool? _isNotOwnerCfm;

		private bool? _isBlockByAdmin;

		private bool? _isTempPassword;

		private bool? _isModifyInfo_1;

		private bool? _isForeignerCfm;

		private bool? _isAlertState;

		private bool? _isCashParentAuth;

		private bool? _isCashParentReject;

		private bool? _isModifiedName;

		private bool? _isModifiedSsn;

		private bool? _isBackOfficeUser;

		private bool? _isSmsReceiveAgree;

		private bool? _isMobilePhoneAuth;

		private bool? _isRemovedByAdmin;

		private bool? _isForeigner;

		private OpenConfigureCode? _n1OpenConfigureName;

		private OpenConfigureCode? _n1OpenConfigureSex;

		private OpenConfigureCode? _n1OpenConfigureAge;

		private OpenConfigureCode? _n1OpenConfigureBirth;

		private OpenConfigureCode? _n1OpenConfigureSchool;

		private OpenConfigureCode? _n1OpenConfigureArea;

		private OpenConfigureCode? _n1OpenConfigurePhone;

		private OpenConfigureCode? _n1OpenConfigureMobilePhone;

		private OpenConfigureCode? _n1OpenConfigureEmail;

		private UserCode? _UserCode;

		private bool? _isBlockCashRefillByAdmin;

		private string _strEmail;

		private string _strRealBirth;

		private RealBirthCode? _RealBirthCode;

		private DateTime? _dtLastLoginTime;

		private DateTime? _dtPrevLastLoginTime;

		private byte? _n1LoginFailCount;

		private DateTime? _dtLastLoginFailTime;

		private DateTime? _dtLastPasswordChanged;

		private byte? _n1TotalWarningPoint;

		private DateTime? _dtLastWarningTime;

		private DateTime? _dtBlockEndDateByWarning;

		private DateTime? _dtMaxEndWarning;

		private string _strAreaCode;

		private string _strAddress1;

		private string _strAddress2;

		private string _strPhone;

		private MobileCompanyCode? _MobileCompanyCode;

		private string _strMobilePhone;

		private MainPageCode? _MainPageCode;

		private List<SchoolInfo> _schoolList;

		private bool? _hasSsn;

		private bool? _isExistUser;

		private bool? _isPwdHashMatch;

		private bool? _isBlockByEvent;
	}
}
