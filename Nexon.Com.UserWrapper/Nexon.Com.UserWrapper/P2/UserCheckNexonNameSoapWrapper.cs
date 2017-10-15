using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.P2;

namespace Nexon.Com.UserWrapper.P2
{
	internal class UserCheckNexonNameSoapWrapper : SoapWrapperBase<p2, UserCheckNexonNameSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.p2;
			}
		}

		public UserCheckNexonNameSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.CheckNexonName(this._n4ServiceCode, this._n4NexonSN, this._strNexonName, this._isCheckChangeCount);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.IsUsable = true;
				return;
			}
			base.Result.IsUsable = false;
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

		internal string NexonName
		{
			set
			{
				this._strNexonName = value;
			}
		}

		internal bool IsCheckChangeCount
		{
			set
			{
				this._isCheckChangeCount = value;
			}
		}

		private int _n4ServiceCode;

		private int _n4NexonSN;

		private string _strNexonName;

		private bool _isCheckChangeCount;
	}
}
