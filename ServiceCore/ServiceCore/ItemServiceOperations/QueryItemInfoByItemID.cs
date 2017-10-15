using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryItemInfoByItemID : Operation
	{
		public long ItemID { get; set; }

		public SlotInfo SlotInfo
		{
			get
			{
				return this.slotInfo;
			}
		}

		public bool Auctionable
		{
			get
			{
				return this.auctionable;
			}
		}

		public string ItemClassEX
		{
			get
			{
				return this.itemClassEX;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryItemInfoByItemID.Request(this);
		}

		[NonSerialized]
		private SlotInfo slotInfo;

		[NonSerialized]
		private bool auctionable;

		[NonSerialized]
		private string itemClassEX;

		private class Request : OperationProcessor<QueryItemInfoByItemID>
		{
			public Request(QueryItemInfoByItemID op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is SlotInfo)
				{
					base.Operation.slotInfo = (base.Feedback as SlotInfo);
					yield return null;
					base.Operation.auctionable = (bool)base.Feedback;
					yield return null;
					base.Operation.itemClassEX = (string)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
