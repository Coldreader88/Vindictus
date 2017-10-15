using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserConfirmMobileAuthSoapResult : SoapResultBase
	{
		public bool Success { get; internal set; }
	}
}
