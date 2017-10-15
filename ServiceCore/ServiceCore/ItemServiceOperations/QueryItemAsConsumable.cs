using System;
using System.Collections.Generic;
using ServiceCore.MicroPlayServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryItemAsConsumable : Operation
	{
		public string ItemClass { get; set; }

		public ConsumablesInfo Consumable
		{
			get
			{
				return this.consumable;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryItemAsConsumable.Request(this);
		}

		[NonSerialized]
		private ConsumablesInfo consumable;

		private class Request : OperationProcessor<QueryItemAsConsumable>
		{
			public Request(QueryItemAsConsumable op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.consumable = (base.Feedback as ConsumablesInfo);
				if (base.Operation.consumable == null)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
