using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.Character;

namespace Nexon.Com.UserWrapper.Character.DAO
{
	internal class UserSsnCheckMatchSubSoapWrapper : SoapWrapperBase<character, UserSsnCheckMatchResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		internal int ServiceCode { get; set; }

		internal int NexonSN { get; set; }

		internal string NexonID { get; set; }

		internal string Ssn { get; set; }

		public UserSsnCheckMatchSubSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.CheckMatchUserSsn_Sub(this.ServiceCode, this.NexonSN, this.NexonID, this.Ssn);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.IsMatch = true;
				return;
			}
			base.Result.IsMatch = false;
		}
	}
}
