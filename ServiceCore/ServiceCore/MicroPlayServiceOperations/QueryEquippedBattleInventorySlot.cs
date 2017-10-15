using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class QueryEquippedBattleInventorySlot : Operation
	{
		public long CID { get; set; }

		public string ItemClass { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new QueryEquippedBattleInventorySlot.Request(this);
		}

		[NonSerialized]
		public byte? Slot;

		private class Request : OperationProcessor<QueryEquippedBattleInventorySlot>
		{
			public Request(QueryEquippedBattleInventorySlot op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is short)
				{
					short num = (short)base.Feedback;
					if (num >= 0 && num <= 255)
					{
						base.Operation.Slot = new byte?((byte)num);
					}
					else
					{
						base.Operation.Slot = null;
					}
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
