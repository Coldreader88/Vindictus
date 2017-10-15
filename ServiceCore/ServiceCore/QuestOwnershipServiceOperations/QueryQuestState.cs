using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class QueryQuestState : Operation
	{
		public string QuestID { get; set; }

		public int Difficulty { get; set; }

		public bool IsPracticeMode { get; set; }

		public int Progress
		{
			get
			{
				return this.progress;
			}
		}

		public QuestConstraintResult ContraintResult
		{
			get
			{
				return this.contraintResult;
			}
		}

		public bool IsAvailable
		{
			get
			{
				return this.contraintResult == QuestConstraintResult.Success;
			}
		}

		public bool IsOwned
		{
			get
			{
				return this.isOwned;
			}
		}

		public List<int> AchievedGoals
		{
			get
			{
				return this.achievedGoals;
			}
		}

		public int PlayCount
		{
			get
			{
				return this.playCount;
			}
		}

		public int TodayPlayCount
		{
			get
			{
				return this.todayPlayCount;
			}
		}

		public bool IsTodayQuest
		{
			get
			{
				return this.isTodayQuest;
			}
		}

		public bool IsNamedKill
		{
			get
			{
				return this.isNamedKill;
			}
		}

		public static QueryQuestState MakeResult(int progress, QuestConstraintResult contraintResult, bool isOwned, List<int> achievedGoals, int playCount, int todayPlayCount, bool isTodayQuest, bool isNamedKill)
		{
			return new QueryQuestState("", 0, false)
			{
				progress = progress,
				contraintResult = contraintResult,
				isOwned = isOwned,
				achievedGoals = achievedGoals,
				playCount = playCount,
				todayPlayCount = todayPlayCount,
				isTodayQuest = isTodayQuest,
				isNamedKill = isNamedKill
			};
		}

		public QueryQuestState(string questID, int difficulty, bool isPracticeMode)
		{
			this.QuestID = questID;
			this.Difficulty = difficulty;
			this.IsPracticeMode = isPracticeMode;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryQuestState.Request(this);
		}

		[NonSerialized]
		private int progress;

		[NonSerialized]
		private QuestConstraintResult contraintResult;

		[NonSerialized]
		private bool isOwned;

		[NonSerialized]
		private List<int> achievedGoals;

		[NonSerialized]
		private int playCount;

		[NonSerialized]
		private int todayPlayCount;

		[NonSerialized]
		private bool isTodayQuest;

		[NonSerialized]
		private bool isNamedKill;

		private class Request : OperationProcessor<QueryQuestState>
		{
			public Request(QueryQuestState op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.progress = (int)base.Feedback;
					yield return null;
					base.Operation.contraintResult = (QuestConstraintResult)base.Feedback;
					yield return null;
					base.Operation.isOwned = (bool)base.Feedback;
					yield return null;
					base.Operation.achievedGoals = (base.Feedback as List<int>);
					yield return null;
					base.Operation.playCount = (int)base.Feedback;
					yield return null;
					base.Operation.todayPlayCount = (int)base.Feedback;
					yield return null;
					base.Operation.isTodayQuest = (bool)base.Feedback;
					yield return null;
					base.Operation.isNamedKill = (bool)base.Feedback;
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
