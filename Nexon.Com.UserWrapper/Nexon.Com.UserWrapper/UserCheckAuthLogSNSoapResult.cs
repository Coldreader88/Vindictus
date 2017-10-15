using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserCheckAuthLogSNSoapResult : SoapResultBase
	{
		public bool Valid { get; internal set; }
	}
}
