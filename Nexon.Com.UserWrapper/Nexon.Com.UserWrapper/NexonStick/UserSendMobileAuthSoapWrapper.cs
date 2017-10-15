using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.NexonStick;

namespace Nexon.Com.UserWrapper.NexonStick
{
	internal class UserSendMobileAuthSoapWrapper : SoapWrapperBase<nexonstick, UserSendMobileAuthSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserSendMobileAuthSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			long authLogSN;
			byte authPGCode;
			string pccSeq;
			string authSeq;
			string text;
			errorCode = base.Soap.SendSMSAuthOwnerCfm(this._n4ServiceCode, 40, this._n1MobileCompanyCode, this._strMobilePhone1, this._strMobilePhone2, this._strMobilePhone3, this._strSsn, this._strName, this._n4NexonSN, this._strClientIP, out authLogSN, out authPGCode, out pccSeq, out authSeq, out text);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.AuthLogSN = authLogSN;
				base.Result.AuthPGCode = authPGCode;
				base.Result.PccSeq = pccSeq;
				base.Result.AuthSeq = authSeq;
			}
		}

		internal int ServiceCode
		{
			set
			{
				this._n4ServiceCode = value;
			}
		}

		internal byte MobileCompanyCode
		{
			set
			{
				this._n1MobileCompanyCode = value;
			}
		}

		internal string MobilePhone1
		{
			set
			{
				this._strMobilePhone1 = value;
			}
		}

		internal string MobilePhone2
		{
			set
			{
				this._strMobilePhone2 = value;
			}
		}

		internal string MobilePhone3
		{
			set
			{
				this._strMobilePhone3 = value;
			}
		}

		internal string Ssn
		{
			set
			{
				this._strSsn = value;
			}
		}

		internal string Name
		{
			set
			{
				this._strName = value;
			}
		}

		internal int NexonSN
		{
			set
			{
				this._n4NexonSN = value;
			}
		}

		internal string ClientIP
		{
			set
			{
				this._strClientIP = value;
			}
		}

		private int _n4ServiceCode;

		private byte _n1MobileCompanyCode;

		private string _strMobilePhone1;

		private string _strMobilePhone2;

		private string _strMobilePhone3;

		private string _strSsn;

		private string _strName;

		private int _n4NexonSN;

		private string _strClientIP;
	}
}
