using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class EquipItem : Operation
	{
		public long ItemID
		{
			get
			{
				return this.itemID;
			}
		}

		public int PartID
		{
			get
			{
				return this.partID;
			}
		}

		public EquipItem(long iid, int pid)
		{
			this.itemID = iid;
			this.partID = pid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new EquipItem.Request(this);
		}

		private long itemID;

		private int partID;

		private class Request : OperationProcessor
		{
			public Request(EquipItem op) : base(op)
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
