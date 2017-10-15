using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.P2;

namespace Nexon.Com.UserWrapper.P2
{
	internal class UserModifyRealBirthSoapWrapper : SoapWrapperBase<p2, UserModifyRealBirthSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.p2;
			}
		}

		public UserModifyRealBirthSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.ModifyRealBirth(this._n4ServiceCode, this._n4NexonSN, this._strRealBirthYear, this._strRealBirthMonth, this._strRealBirthDay, this._n1RealBirthCode);
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

		internal string RealBirthYear
		{
			set
			{
				this._strRealBirthYear = value;
			}
		}

		internal string RealBirthMonth
		{
			set
			{
				this._strRealBirthMonth = value;
			}
		}

		internal string RealBirthDay
		{
			set
			{
				this._strRealBirthDay = value;
			}
		}

		internal byte RealBirthCode
		{
			set
			{
				this._n1RealBirthCode = value;
			}
		}

		private int _n4ServiceCode;

		private int _n4NexonSN;

		private string _strRealBirthYear;

		private string _strRealBirthMonth;

		private string _strRealBirthDay;

		private byte _n1RealBirthCode;
	}
}
