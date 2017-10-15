using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class DyeItem : Operation
	{
		public bool StartNewSession { get; set; }

		public long ItemID { get; set; }

		public bool IsPremium { get; set; }

		public DyeItem(bool StartNewSession, long itemID, bool IsPremium)
		{
			this.StartNewSession = StartNewSession;
			this.ItemID = itemID;
			this.IsPremium = IsPremium;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
