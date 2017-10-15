using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.Character;

namespace Nexon.Com.UserWrapper.Character.DAO
{
	internal class UserCheckCharacterBlockSoapWrapper : SoapWrapperBase<character, UserCheckCharacterBlockSoapResult>
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

		public UserCheckCharacterBlockSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.CheckCharacterBlock(this.ServiceCode, this.NexonSN);
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
	}
}
