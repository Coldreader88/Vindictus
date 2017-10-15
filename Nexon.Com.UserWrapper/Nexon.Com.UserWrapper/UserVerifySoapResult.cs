using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserVerifySoapResult : SoapResultBase
	{
		public bool PwdMatch { get; internal set; }
	}
}
