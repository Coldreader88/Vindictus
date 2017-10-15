using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.P2;

namespace Nexon.Com.UserWrapper.P2
{
	internal class UserModifyMainPageCodeSoapWrapper : SoapWrapperBase<p2, UserModifyMainPageCodeSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.p2;
			}
		}

		public UserModifyMainPageCodeSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.ModifyMainPageCode(this._n4ServiceCode, this._n4NexonSN, this._n1MainPageCode);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.IsSuccess = true;
				return;
			}
			base.Result.IsSuccess = false;
		}

		internal int ServiceCode
		{
			set
			{
				this._n4ServiceCode = value;
			}
		}

		internal int NexonSN
		{
			set
			{
				this._n4NexonSN = value;
			}
		}

		internal byte MainPageCode
		{
			set
			{
				this._n1MainPageCode = value;
			}
		}

		private int _n4ServiceCode;

		private int _n4NexonSN;

		private byte _n1MainPageCode;
	}
}
