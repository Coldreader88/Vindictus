using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserSsnCheckMatchResult : SoapResultBase
	{
		public bool IsMatch { get; internal set; }
	}
}
