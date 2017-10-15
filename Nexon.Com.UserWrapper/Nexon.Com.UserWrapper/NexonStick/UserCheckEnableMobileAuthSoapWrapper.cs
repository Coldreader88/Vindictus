using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.NexonStick;

namespace Nexon.Com.UserWrapper.NexonStick
{
	internal class UserCheckEnableMobileAuthSoapWrapper : SoapWrapperBase<nexonstick, UserCheckEnableMobileAuthSoapResult>
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
			errorCode = base.Soap.CheckEnableMobileAuth(this._n4ServiceCode, 40, this._strSsn, out enable);
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

		internal string Ssn
		{
			set
			{
				this._strSsn = value;
			}
		}

		private int _n4ServiceCode;

		private string _strSsn;
	}
}
