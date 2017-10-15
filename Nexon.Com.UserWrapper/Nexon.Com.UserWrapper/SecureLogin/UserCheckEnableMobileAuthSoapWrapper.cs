using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.SecureLogin;

namespace Nexon.Com.UserWrapper.SecureLogin
{
	internal class UserCheckEnableMobileAuthSoapWrapper : SoapWrapperBase<securelogin, UserCheckEnableMobileAuthSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserCheckEnableMobileAuthSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			bool enable;
			errorCode = base.Soap.GetOTPRemoveMobileCountCheck(this._n4ServiceCode, this._strMobilePhone1, this._strMobilePhone2, this._strMobilePhone3, out enable);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.Enable = enable;
				return;
			}
			base.Result.Enable = false;
		}

		internal int ServiceCode
		{
			set
			{
				this._n4ServiceCode = value;
			}
		}

		internal string MobilePhone1
		{
			set
			{
				this._strMobilePhone1 = value;
			}
		}

		internal string MobilePhone2
		{
			set
			{
				this._strMobilePhone2 = value;
			}
		}

		internal string MobilePhone3
		{
			set
			{
				this._strMobilePhone3 = value;
			}
		}

		private int _n4ServiceCode;

		private string _strMobilePhone1;

		private string _strMobilePhone2;

		private string _strMobilePhone3;
	}
}
