using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.P2;

namespace Nexon.Com.UserWrapper.P2
{
	internal class UserCheckValidNexonIDnPasswordSoapWrapper : SoapWrapperBase<p2, UserCheckValidNexonIDnPasswordSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.p2;
			}
		}

		internal int ServiceCode { get; set; }

		internal string NexonID { get; set; }

		internal string Password { get; set; }

		public UserCheckValidNexonIDnPasswordSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.CheckValidNexonIDnPassword(this.ServiceCode, this.NexonID, this.Password);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.Enable = true;
				return;
			}
			base.Result.Enable = false;
		}
	}
}
