using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserGetIdentitySNSoapResult : SoapResultBase
	{
		public long IdentitySN { get; internal set; }
	}
}
