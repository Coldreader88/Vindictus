using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public class ConsumeBlessStone : Operation
	{
		public List<BlessStoneType> TargetStoneList { get; set; }

		public string QuestID { get; set; }

		public bool IgnoreFatigue { get; set; }

		public BlessStoneBonusInfo BonusInfo
		{
			get
			{
				return this.bonusInfo;
			}
			set
			{
				this.bonusInfo = value;
			}
		}

		public bool DestroyFatigue
		{
			get
			{
				return this.destroyFatigue;
			}
			set
			{
				this.destroyFatigue = value;
			}
		}

		public ConsumeBlessStone(string questID)
		{
			this.QuestID = questID;
			this.TargetStoneList = new List<BlessStoneType>();
			this.IgnoreFatigue = false;
		}

		public void AddTargetStone(BlessStoneType type)
		{
			this.TargetStoneList.Add(type);
		}

		public override OperationProcessor RequestProcessor()
		{
			return new ConsumeBlessStone.Request(this);
		}

		[NonSerialized]
		private BlessStoneBonusInfo bonusInfo;

		[NonSerialized]
		private bool destroyFatigue;

		private class Request : OperationProcessor<ConsumeBlessStone>
		{
			public Request(ConsumeBlessStone op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is FailMessage)
				{
					base.Result = false;
					base.Operation.BonusInfo = new BlessStoneBonusInfo();
				}
				else
				{
					base.Result = true;
					base.Operation.BonusInfo = (base.Feedback as BlessStoneBonusInfo);
					yield return null;
					base.Operation.DestroyFatigue = (bool)base.Feedback;
				}
				yield break;
			}
		}
	}
}
