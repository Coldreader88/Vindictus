using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.QplayWeb;

namespace Nexon.Com.UserWrapper.QplayWeb
{
	internal class UserVerifySoapWrapper : SoapWrapperBase<qplayweb, UserVerifySoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserVerifySoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			string text;
			errorCode = base.Soap.CheckValidNexonIDnPassword(this._n4ServiceCode, this._strNexonID, this._strPassword, out text);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.PwdMatch = true;
				return;
			}
			base.Result.PwdMatch = false;
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

		private int _n4ServiceCode;

		private string _strNexonID;

		private string _strPassword;
	}
}
