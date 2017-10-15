using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class RewardCharacter : Operation
	{
		public string QuestID { get; private set; }

		public bool IsSuccess { get; private set; }

		public int Difficulty { get; private set; }

		public int PlayerCount { get; private set; }

		public int XeRank { get; private set; }

		public int ClearTimeSec { get; set; }

		public int ClearInfo { get; set; }

		public long HostCID { get; set; }

		public Dictionary<string, int> BossKilled { get; private set; }

		public Dictionary<int, DateTime> QuestGoal { get; set; }

		public int Exp { get; set; }

		public int Gold { get; set; }

		public int AP { get; set; }

		public Dictionary<string, int> ItemRewards { get; set; }

		public Dictionary<string, float> ItemDurabilityPollutionRatios { get; set; }

		public Dictionary<string, float> ItemMaxDurabilityPollutionRatios { get; set; }

		public Dictionary<int, int> StoryDropResult { get; set; }

		public bool IsRefrashFatiueEffect { get; set; }

		public RewardCharacter(string questID, bool isSuccess, int difficulty, int playerCount, int xeRank, int clearTimeSec, int clearInfo, Dictionary<string, int> bossKilled, long hostCID, Dictionary<int, int> storyDropResult, bool isRefrashFatiueEffect)
		{
			this.QuestID = questID;
			this.IsSuccess = isSuccess;
			this.Difficulty = difficulty;
			this.PlayerCount = playerCount;
			this.XeRank = xeRank;
			this.ClearTimeSec = clearTimeSec;
			this.ClearInfo = clearInfo;
			this.BossKilled = bossKilled;
			this.HostCID = hostCID;
			this.StoryDropResult = storyDropResult;
			this.IsRefrashFatiueEffect = isRefrashFatiueEffect;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RewardCharacter.Request(this);
		}

		[NonSerialized]
		public int GivenExp;

		[NonSerialized]
		public int GivenGold;

		[NonSerialized]
		public int GivenAP;

		[NonSerialized]
		public Dictionary<string, int> GivenItems;

		[NonSerialized]
		public int StorySectorID;

		private class Request : OperationProcessor<RewardCharacter>
		{
			public Request(RewardCharacter op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Result = true;
					base.Operation.GivenExp = (int)base.Feedback;
					yield return null;
					base.Operation.GivenGold = (int)base.Feedback;
					yield return null;
					base.Operation.GivenAP = (int)base.Feedback;
					yield return null;
					base.Operation.GivenItems = (base.Feedback as Dictionary<string, int>);
					yield return null;
					base.Operation.StorySectorID = (int)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.GivenExp = 0;
					base.Operation.GivenGold = 0;
					base.Operation.GivenAP = 0;
					base.Operation.GivenItems = new Dictionary<string, int>();
				}
				yield break;
			}
		}
	}
}
