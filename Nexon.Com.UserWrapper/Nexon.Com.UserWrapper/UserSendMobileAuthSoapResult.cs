using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserSendMobileAuthSoapResult : SoapResultBase
	{
		public long AuthLogSN { get; internal set; }

		public byte AuthPGCode { get; internal set; }

		public string PccSeq { get; internal set; }

		public string AuthSeq { get; internal set; }
	}
}
