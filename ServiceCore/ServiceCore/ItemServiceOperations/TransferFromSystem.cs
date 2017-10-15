using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TransferFromSystem : Operation
	{
		public ICollection<long> ItemList { get; set; }

		public bool IsUserTrade { get; set; }

		public byte TargetTab { get; set; }

		public int TargetSlotMin { get; set; }

		public int TargetSlotMax { get; set; }

		public TransferFromSystem.SourceEnum Source { get; set; }

		public TransferFromSystem.FailReasonEnum FailReason
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

		public TransferFromSystem(ICollection<long> ItemList, bool isUserTrade, TransferFromSystem.SourceEnum source)
		{
			this.ItemList = ItemList;
			this.IsUserTrade = isUserTrade;
			this.Source = source;
		}

		public TransferFromSystem(ICollection<long> ItemList, bool isUserTrade, byte targetTab, int targetSlotMin, int targetSlotMax, TransferFromSystem.SourceEnum source)
		{
			this.ItemList = ItemList;
			this.IsUserTrade = isUserTrade;
			this.TargetTab = targetTab;
			this.TargetSlotMin = targetSlotMin;
			this.TargetSlotMax = targetSlotMax;
			this.Source = source;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new TransferFromSystem.Request(this);
		}

		[NonSerialized]
		private TransferFromSystem.FailReasonEnum failReason;

		public enum FailReasonEnum
		{
			NoInventoryInfo,
			InvalidItemClass,
			NoEmptySlot,
			UniqueViolation,
			FeePayError,
			DBSubmitError,
			Unknown
		}

		public enum SourceEnum
		{
			Unknown,
			GuildStorage
		}

		private class Request : OperationProcessor<TransferFromSystem>
		{
			public Request(TransferFromSystem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
				}
				else if (base.Feedback is TransferFromSystem.FailReasonEnum)
				{
					base.Result = false;
					base.Operation.FailReason = (TransferFromSystem.FailReasonEnum)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.FailReason = TransferFromSystem.FailReasonEnum.Unknown;
				}
				yield break;
			}
		}
	}
}
