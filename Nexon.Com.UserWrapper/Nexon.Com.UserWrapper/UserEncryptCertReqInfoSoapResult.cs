using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserEncryptCertReqInfoSoapResult : SoapResultBase
	{
		public string EncryptData { get; internal set; }
	}
}
