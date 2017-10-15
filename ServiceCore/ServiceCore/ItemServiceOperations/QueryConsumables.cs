using System;
using System.Collections.Generic;
using ServiceCore.MicroPlayServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryConsumables : Operation
	{
		public float SubWeaponMultiplier { get; set; }

		public float QuickSlotMultiplier { get; set; }

		public IDictionary<int, ConsumablesInfo> Consumables
		{
			get
			{
				return this.consumables;
			}
		}

		public long CID
		{
			get
			{
				return this.cid;
			}
			set
			{
				this.cid = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryConsumables.Request(this);
		}

		[NonSerialized]
		private IDictionary<int, ConsumablesInfo> consumables;

		[NonSerialized]
		private long cid;

		private class Request : OperationProcessor<QueryConsumables>
		{
			public Request(QueryConsumables op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.consumables = (base.Feedback as IDictionary<int, ConsumablesInfo>);
				if (base.Operation.consumables == null)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
