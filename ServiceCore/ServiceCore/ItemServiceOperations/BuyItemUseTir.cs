using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class BuyItemUseTir : Operation
	{
		public IList<int> QueryProductList { get; set; }

		public int NexonSN { get; set; }

		public bool notifyDialog { get; set; }

		public Dictionary<string, int> ResultingItems
		{
			get
			{
				return this.result;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new BuyItemUseTir.Request(this);
		}

		[NonSerialized]
		private Dictionary<string, int> result;

		private class Request : OperationProcessor<BuyItemUseTir>
		{
			public Request(BuyItemUseTir op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					yield return null;
					base.Operation.result = (base.Feedback as Dictionary<string, int>);
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
