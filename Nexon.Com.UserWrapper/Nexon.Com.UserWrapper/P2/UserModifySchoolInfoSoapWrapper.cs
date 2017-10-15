using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.P2;

namespace Nexon.Com.UserWrapper.P2
{
	internal class UserModifySchoolInfoSoapWrapper : SoapWrapperBase<p2, UserModifyMainPageCodeSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.p2;
			}
		}

		public UserModifySchoolInfoSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.ModifySchoolInfo(this._n4ServiceCode, this._n4NexonSN, this._n4SchoolSN, this._arrSchoolSNList);
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

		internal int SchoolSN
		{
			set
			{
				this._n4SchoolSN = value;
			}
		}

		internal int[][] SchoolSNList
		{
			set
			{
				this._arrSchoolSNList = value;
			}
		}

		private int _n4ServiceCode;

		private int _n4NexonSN;

		private int _n4SchoolSN;

		private int[][] _arrSchoolSNList;
	}
}
