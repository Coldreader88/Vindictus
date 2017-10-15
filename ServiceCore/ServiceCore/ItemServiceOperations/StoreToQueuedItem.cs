using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class StoreToQueuedItem : Operation
	{
		public long CID { get; set; }

		public string ItemClass { get; set; }

		public int Count { get; set; }

		public Dictionary<string, int> ItemDictionary { get; set; }

		public string EventCode { get; set; }

		public string MailTitle { get; set; }

		public string MailContent { get; set; }

		public DateTime ExpireTime { get; set; }

		public StoreToQueuedItem()
		{
			this.CID = 0L;
			this.ItemClass = "";
			this.Count = 0;
			this.ItemDictionary = null;
			this.EventCode = null;
			this.MailTitle = null;
			this.MailContent = null;
			this.ExpireTime = DateTime.MinValue;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
