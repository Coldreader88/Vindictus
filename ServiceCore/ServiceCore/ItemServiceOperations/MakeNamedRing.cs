using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class MakeNamedRing : Operation
	{
		public long itemID { get; set; }

		public string UserName { get; set; }

		public MakeNamedRing(long itemID, string userName)
		{
			this.itemID = itemID;
			this.UserName = userName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
