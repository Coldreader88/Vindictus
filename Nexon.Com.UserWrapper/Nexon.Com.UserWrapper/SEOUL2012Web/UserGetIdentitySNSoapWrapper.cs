using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.SEOUL2012Web;

namespace Nexon.Com.UserWrapper.SEOUL2012Web
{
	internal class UserGetIdentitySNSoapWrapper : SoapWrapperBase<seoul2012web, UserGetIdentitySNSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.seoul2012web;
			}
		}

		public UserGetIdentitySNSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			long identitySN = 0L;
			errorCode = base.Soap.GetUserIdentitySN(this._n4ServiceCode, this._strNexonID, out identitySN);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.IdentitySN = identitySN;
			}
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
