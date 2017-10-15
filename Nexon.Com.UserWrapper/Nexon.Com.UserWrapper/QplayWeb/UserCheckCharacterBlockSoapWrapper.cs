using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.QplayWeb;

namespace Nexon.Com.UserWrapper.QplayWeb
{
	internal class UserCheckCharacterBlockSoapWrapper : SoapWrapperBase<qplayweb, UserCheckCharacterBlockSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserCheckCharacterBlockSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.CheckCharacterBlock(this._n4ServiceCode, this._strNexonID);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.CharacterBlock = false;
				return;
			}
			if (errorCode == 9201)
			{
				base.Result.CharacterBlock = true;
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
