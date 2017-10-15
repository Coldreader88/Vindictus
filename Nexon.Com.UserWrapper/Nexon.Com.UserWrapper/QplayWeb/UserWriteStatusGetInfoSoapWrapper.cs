using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.QplayWeb;

namespace Nexon.Com.UserWrapper.QplayWeb
{
	internal class UserWriteStatusGetInfoSoapWrapper : SoapWrapperBase<qplayweb, UserWriteStatusGetInfoSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserWriteStatusGetInfoSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			byte writeStatusCode;
			int nexonSN;
			errorCode = base.Soap.GetUserWriteStatusCode2(this._n4ServiceCode, this._strNexonID, out writeStatusCode, out nexonSN);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.WriteStatusCode = (WriteStatusCode)writeStatusCode;
				base.Result.NexonSN = nexonSN;
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

		internal string NexonID
		{
			set
			{
				this._strNexonID = value;
			}
		}

		private int _n4ServiceCode;

		private string _strNexonID;
	}
}
