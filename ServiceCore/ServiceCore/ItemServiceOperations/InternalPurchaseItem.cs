using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class InternalPurchaseItem : Operation
	{
		public string ItemClass { get; set; }

		public int BeforeCount { get; set; }

		public bool IsFree { get; set; }

		public int PurchasedCount
		{
			get
			{
				return this.purchased;
			}
		}

		public int TargetCount
		{
			get
			{
				return this.targetcount;
			}
		}

		public InternalPurchaseItem(string itemClass, int before, bool free)
		{
			this.ItemClass = itemClass;
			this.BeforeCount = before;
			this.IsFree = free;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new InternalPurchaseItem.Request(this);
		}

		[NonSerialized]
		private int purchased;

		[NonSerialized]
		private int targetcount;

		private class Request : OperationProcessor<InternalPurchaseItem>
		{
			public Request(InternalPurchaseItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Result = true;
					base.Operation.purchased = (int)base.Feedback;
					yield return null;
					base.Operation.targetcount = (int)base.Feedback;
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
