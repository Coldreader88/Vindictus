using System;
using Nexon.Com.DAO;

namespace Nexon.Com.UserWrapper
{
	internal class UserWriteStatusGetInfoSoapResult : SoapResultBase
	{
		public WriteStatusCode WriteStatusCode { get; internal set; }

		public int NexonSN { get; internal set; }
	}
}
