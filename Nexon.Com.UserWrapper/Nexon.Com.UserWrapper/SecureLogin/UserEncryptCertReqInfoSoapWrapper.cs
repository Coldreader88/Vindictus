using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.SecureLogin;

namespace Nexon.Com.UserWrapper.SecureLogin
{
	internal class UserEncryptCertReqInfoSoapWrapper : SoapWrapperBase<securelogin, UserEncryptCertReqInfoSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			string encryptData;
			errorCode = base.Soap.GetCommonCertAuthCrypt(this._n4ServiceCode, this._strName, this._strSsn, out encryptData);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.EncryptData = encryptData;
			}
		}

		internal int ServiceCode
		{
			set
			{
				this._n4ServiceCode = value;
			}
		}

		internal string Name
		{
			set
			{
				this._strName = value;
			}
		}

		internal string Ssn
		{
			set
			{
				this._strSsn = value;
			}
		}

		private int _n4ServiceCode;

		private string _strName;

		private string _strSsn;
	}
}
