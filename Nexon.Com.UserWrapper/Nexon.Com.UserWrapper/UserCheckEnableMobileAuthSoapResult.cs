using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserCheckEnableMobileAuthSoapResult : SoapResultBase
	{
		public bool Enable { get; internal set; }
	}
}
