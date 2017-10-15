using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class DirectPickUpByProductNo : Operation
	{
		public List<int> ProductNoList { get; set; }

		public bool IsCredit { get; set; }

		public int OrderNo
		{
			get
			{
				return this.orderno;
			}
		}

		public Dictionary<string, int> ResultingItems
		{
			get
			{
				return this.result;
			}
		}

		public string FailReasonString
		{
			get
			{
				return this.failReasonString;
			}
		}

		public DirectPickUpByProductNo(List<int> productNoList, bool isCredit)
		{
			this.ProductNoList = productNoList;
			this.IsCredit = isCredit;
		}

		public DirectPickUpByProductNo()
		{
		}

		public override OperationProcessor RequestProcessor()
		{
			return new DirectPickUpByProductNo.Request(this);
		}

		[NonSerialized]
		private int orderno;

		[NonSerialized]
		private Dictionary<string, int> result;

		[NonSerialized]
		private string failReasonString;

		private class Request : OperationProcessor<DirectPickUpByProductNo>
		{
			public Request(DirectPickUpByProductNo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.orderno = (int)base.Feedback;
					yield return null;
					base.Operation.result = (base.Feedback as Dictionary<string, int>);
				}
				else
				{
					base.Result = false;
					if (base.Feedback is string)
					{
						base.Operation.failReasonString = (base.Feedback as string);
						yield return null;
					}
				}
				yield break;
			}
		}
	}
}
