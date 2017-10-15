using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.GuestUser;

namespace Nexon.Com.UserWrapper.GuestUser
{
	internal class UserVerifyWithSsnSoapWrapper : SoapWrapperBase<guestuser, UserVerifyWithSsnSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserVerifyWithSsnSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			int nexonSN;
			errorCode = base.Soap.CheckEnableChangeUser(this._n4ServiceCode, this._strNexonID, this._strPassword, this._strSsnSub, out nexonSN);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.ValidUser = true;
				base.Result.NexonSN = nexonSN;
				return;
			}
			base.Result.ValidUser = false;
			base.Result.NexonSN = 0;
		}

		internal int ServiceCode
		{
			set
			{
				this._n4ServiceCode = value;
			}
		}

		internal string NexonID
		{
			set
			{
				this._strNexonID = value;
			}
		}

		internal string Password
		{
			set
			{
				this._strPassword = value;
			}
		}

		internal string SsnSub
		{
			set
			{
				this._strSsnSub = value;
			}
		}

		private int _n4ServiceCode;

		private string _strNexonID;

		private string _strPassword;

		private string _strSsnSub;
	}
}
