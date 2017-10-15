using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.P2;

namespace Nexon.Com.UserWrapper.P2
{
	internal class UserGetNexonSNByNexonNameSoapWrapper : SoapWrapperBase<p2, UserGetListSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.p2;
			}
		}

		internal int ServiceCode { get; set; }

		internal string NexonName { get; set; }

		public UserGetNexonSNByNexonNameSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			int value = 0;
			errorCode = base.Soap.GetUserNexonSN_ByNexonName(this.ServiceCode, this.NexonName, out value);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.AddUser(new int?(value), null, null, null, this.NexonName, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
			}
		}
	}
}
