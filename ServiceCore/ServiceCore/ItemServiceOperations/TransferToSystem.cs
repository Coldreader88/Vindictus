using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TransferToSystem : Operation
	{
		public ICollection<TransferItemInfo> ItemList { get; set; }

		public TransferToSystem.SourceEnum Source { get; set; }

		public ICollection<TransferredItemInfo> TransferredItemList
		{
			get
			{
				return this.transferredItemList;
			}
			set
			{
				this.transferredItemList = value;
			}
		}

		public TransferToSystem.FailReasonEnum FailReason
		{
			get
			{
				return this.failReason;
			}
			set
			{
				this.failReason = value;
			}
		}

		public TransferToSystem(ICollection<TransferItemInfo> itemList, TransferToSystem.SourceEnum source)
		{
			this.ItemList = itemList;
			this.Source = source;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new TransferToSystem.Request(this);
		}

		[NonSerialized]
		private ICollection<TransferredItemInfo> transferredItemList;

		[NonSerialized]
		private TransferToSystem.FailReasonEnum failReason;

		public enum FailReasonEnum
		{
			Unknown,
			NoInventoryInfo,
			NoItemInfo,
			ImproperTradeLevel,
			EquippedItem,
			TrasferError,
			FeePayError,
			DBSubmitError
		}

		public enum SourceEnum
		{
			Unknown,
			GuildStorage
		}

		private class Request : OperationProcessor<TransferToSystem>
		{
			public Request(TransferToSystem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is ICollection<TransferredItemInfo>)
				{
					base.Operation.TransferredItemList = (base.Feedback as ICollection<TransferredItemInfo>);
				}
				else
				{
					base.Operation.FailReason = (TransferToSystem.FailReasonEnum)base.Feedback;
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
