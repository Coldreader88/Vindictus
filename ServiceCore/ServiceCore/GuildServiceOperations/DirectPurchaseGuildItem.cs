using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.GuildService;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class DirectPurchaseGuildItem : Operation
	{
		public GuildMemberKey Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		public int ProductNo
		{
			get
			{
				return this.productNo;
			}
			set
			{
				this.productNo = value;
			}
		}

		public bool IsCredit
		{
			get
			{
				return this.isCredit;
			}
			set
			{
				this.isCredit = value;
			}
		}

		public string ItemClass
		{
			get
			{
				return this.itemClass;
			}
			set
			{
				this.itemClass = value;
			}
		}

		public new DirectPurchaseGuildItemResultCode Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		public DirectPurchaseGuildItem(GuildMemberKey key, int productNo, bool isCredit, string itemClass)
		{
			this.Key = key;
			this.ProductNo = productNo;
			this.IsCredit = isCredit;
			this.ItemClass = itemClass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new DirectPurchaseGuildItem.Request(this);
		}

		private GuildMemberKey key;

		private int productNo;

		private bool isCredit;

		private string itemClass;

		private DirectPurchaseGuildItemResultCode result;

		private class Request : OperationProcessor<DirectPurchaseGuildItem>
		{
			public Request(DirectPurchaseGuildItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is DirectPurchaseGuildItemResultCode)
				{
					base.Result = true;
					base.Operation.Result = (DirectPurchaseGuildItemResultCode)base.Feedback;
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
