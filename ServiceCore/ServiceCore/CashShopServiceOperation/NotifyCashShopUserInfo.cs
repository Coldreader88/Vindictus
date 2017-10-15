using System;
using System.Net;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class NotifyCashShopUserInfo : Operation
	{
		public string Name { get; set; }

		public int NexonSN { get; set; }

		public long FID { get; set; }

		public long CID { get; set; }

		public int CharacterSN { get; set; }

		public byte Age { get; set; }

		public IPAddress RemoteIP { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
