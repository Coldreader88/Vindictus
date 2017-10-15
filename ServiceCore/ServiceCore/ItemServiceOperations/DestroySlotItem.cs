using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class DestroySlotItem : Operation
	{
		public long ItemID { get; set; }

		public DestroySlotItem(long iid)
		{
			this.ItemID = iid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new DestroySlotItem.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(DestroySlotItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Result = (base.Feedback is OkMessage);
				yield break;
			}
		}
	}
}
