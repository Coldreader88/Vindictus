using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.P2;

namespace Nexon.Com.UserWrapper.P2
{
	internal class UserGetIdentitySNEventSoapWrapper : SoapWrapperBase<p2, UserGetIdentitySNEventSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.p2;
			}
		}

		internal int ServiceCode { get; set; }

		internal int NexonSN { get; set; }

		internal string NexonName_Recommended { get; set; }

		public UserGetIdentitySNEventSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			long identitySN = 0L;
			long identitySN_Recommended = 0L;
			int nexonSN_Recommended = 0;
			errorCode = base.Soap.GetUserIdentitySN_Event(this.ServiceCode, this.NexonSN, this.NexonName_Recommended, out identitySN, out identitySN_Recommended, out nexonSN_Recommended);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.ValidUser = true;
				base.Result.IdentitySN = identitySN;
				base.Result.IdentitySN_Recommended = identitySN_Recommended;
				base.Result.NexonSN_Recommended = nexonSN_Recommended;
			}
		}
	}
}
