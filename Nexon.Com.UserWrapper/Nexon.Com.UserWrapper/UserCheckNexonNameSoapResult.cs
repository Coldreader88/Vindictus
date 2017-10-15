using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserCheckNexonNameSoapResult : SoapResultBase
	{
		public bool IsUsable { get; internal set; }
	}
}
