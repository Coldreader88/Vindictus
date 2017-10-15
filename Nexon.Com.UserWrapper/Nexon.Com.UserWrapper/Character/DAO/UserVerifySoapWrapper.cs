using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.Character;

namespace Nexon.Com.UserWrapper.Character.DAO
{
	internal class UserVerifySoapWrapper : SoapWrapperBase<character, UserVerifySoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		internal int ServiceCode { get; set; }

		internal string NexonID { get; set; }

		internal string Password { get; set; }

		public UserVerifySoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.CheckValidNexonIDnPassword(this.ServiceCode, this.NexonID, this.Password);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.PwdMatch = true;
				return;
			}
			base.Result.PwdMatch = false;
		}
	}
}
