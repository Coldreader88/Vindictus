using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class GiveItem : Operation
	{
		public ItemRequestInfo ItemRequestInfo { get; set; }

		public GiveItem.FailMethodEnum FailMethod { get; set; }

		public GiveItem.SourceEnum Source { get; set; }

		public GiveItem.ResultEnum ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		public GiveItem(ItemRequestInfo request, GiveItem.FailMethodEnum failMethod, GiveItem.SourceEnum source)
		{
			this.ItemRequestInfo = request;
			this.FailMethod = failMethod;
			this.Source = source;
		}

		public GiveItem(IDictionary<string, int> itemDict, GiveItem.FailMethodEnum failMethod, GiveItem.SourceEnum source)
		{
			this.ItemRequestInfo = new ItemRequestInfo(itemDict);
			this.FailMethod = failMethod;
			this.Source = source;
		}

		public GiveItem(string itemclassEx, int num, GiveItem.FailMethodEnum failMethod, GiveItem.SourceEnum source)
		{
			this.ItemRequestInfo = new ItemRequestInfo(itemclassEx, num);
			this.FailMethod = failMethod;
			this.Source = source;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GiveItem.Request(this);
		}

		[NonSerialized]
		private GiveItem.ResultEnum errorCode;

		public enum ResultEnum
		{
			Success,
			Mailed_UniqueViolation,
			Mailed_NoEmptySlot,
			Mailed_Unknown,
			Failed
		}

		public enum FailMethodEnum
		{
			OperationFail,
			ForceMail
		}

		public enum SourceEnum
		{
			Unknown,
			CashShop,
			RandomMission,
			AutoFishing,
			Tower,
			QuestReward,
			QuestDrop,
			QuestErg,
			QuestToken,
			QuestActionReward,
			QuestReceive,
			Pvp,
			Story,
			StoryReward,
			GMMail,
			Skill,
			GuildLevelUp,
			GatheringRock,
			BingoBoard,
			AttendanceEvent,
			RouletteTicket
		}

		private class Request : OperationProcessor<GiveItem>
		{
			public Request(GiveItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is GiveItem.ResultEnum)
				{
					base.Operation.ErrorCode = (GiveItem.ResultEnum)base.Feedback;
					base.Result = true;
				}
				else
				{
					base.Operation.ErrorCode = GiveItem.ResultEnum.Failed;
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
