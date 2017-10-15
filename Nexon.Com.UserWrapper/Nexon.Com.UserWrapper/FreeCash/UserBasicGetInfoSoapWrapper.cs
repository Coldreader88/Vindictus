using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.FreeCash;

namespace Nexon.Com.UserWrapper.FreeCash
{
	internal class UserBasicGetInfoSoapWrapper : SoapWrapperBase<freecash, UserBasicGetListSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserBasicGetInfoSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			bool isHasSsn = false;
			byte value = 0;
			bool value2 = false;
			string strNexonID = this._strNexonID;
			errorCode = base.Soap.GetUserBasicInfo(this._n4ServiceCode, 0, this._strNexonID, out isHasSsn, out value, out value2);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.AddUserBasic(null, strNexonID, null, null, null, null, null, null, new byte?(value), null, null, new bool?(value2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, isHasSsn);
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
