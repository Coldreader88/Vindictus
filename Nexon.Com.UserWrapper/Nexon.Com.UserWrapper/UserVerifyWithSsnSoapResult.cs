using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserVerifyWithSsnSoapResult : SoapResultBase
	{
		public bool ValidUser { get; internal set; }

		public int NexonSN { get; internal set; }
	}
}
