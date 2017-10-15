using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.P2;

namespace Nexon.Com.UserWrapper.P2
{
	internal class UserWriteStatusGetInfoSoapWrapper : SoapWrapperBase<p2, UserWriteStatusGetInfoSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.p2;
			}
		}

		public UserWriteStatusGetInfoSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			byte writeStatusCode;
			errorCode = base.Soap.GetUserWriteStatusCode(this._n4ServiceCode, this._n4NexonSN, out writeStatusCode);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.WriteStatusCode = (WriteStatusCode)writeStatusCode;
				base.Result.NexonSN = this._n4NexonSN;
				return;
			}
			base.Result.WriteStatusCode = WriteStatusCode.Unknown;
			base.Result.NexonSN = 0;
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

		private int _n4ServiceCode;

		private int _n4NexonSN;
	}
}
