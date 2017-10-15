using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.NexonStick;

namespace Nexon.Com.UserWrapper.NexonStick
{
	internal class UserConfirmMobileAuthSoapWrapper : SoapWrapperBase<nexonstick, UserConfirmMobileAuthSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserConfirmMobileAuthSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.ConfirmSMSAuthOwnerCfm(this._n4ServiceCode, this._n8AuthLogSN, this._strInputData);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.Success = true;
				return;
			}
			base.Result.Success = false;
		}

		internal int ServiceCode
		{
			set
			{
				this._n4ServiceCode = value;
			}
		}

		internal long AuthLogSN
		{
			set
			{
				this._n8AuthLogSN = value;
			}
		}

		internal string InputData
		{
			set
			{
				this._strInputData = value;
			}
		}

		private int _n4ServiceCode;

		private long _n8AuthLogSN;

		private string _strInputData;
	}
}
