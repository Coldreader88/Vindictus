using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserGetIdentitySNEventSoapResult : SoapResultBase
	{
		public bool ValidUser { get; internal set; }

		public long IdentitySN { get; internal set; }

		public long IdentitySN_Recommended { get; internal set; }

		public int NexonSN_Recommended { get; internal set; }
	}
}
