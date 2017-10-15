using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.NexonStick;

namespace Nexon.Com.UserWrapper.NexonStick
{
	internal class UserCheckAuthLogSNSoapWrapper : SoapWrapperBase<nexonstick, UserCheckAuthLogSNSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserCheckAuthLogSNSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.CheckValidAuthLogSN(this._n4ServiceCode, this._n8AuthLog, this._n4NexonSN);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.Valid = true;
				return;
			}
			base.Result.Valid = false;
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
				this._n8AuthLog = value;
			}
		}

		internal int NexonSN
		{
			set
			{
				this._n4NexonSN = value;
			}
		}

		private int _n4ServiceCode;

		private long _n8AuthLog;

		private int _n4NexonSN;
	}
}
