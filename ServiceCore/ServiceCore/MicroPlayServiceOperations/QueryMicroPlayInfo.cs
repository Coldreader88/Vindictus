using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class QueryMicroPlayInfo : Operation
	{
		public string QuestID
		{
			get
			{
				return this.questID;
			}
		}

		public QuestSectorInfos QuestSectorInfo
		{
			get
			{
				return this.questSectorInfo;
			}
		}

		public bool HardMode
		{
			get
			{
				return this.hardMode;
			}
		}

		public int SoloSector
		{
			get
			{
				return this.soloSector;
			}
		}

		public int TimeLimit
		{
			get
			{
				return this.timeLimit;
			}
		}

		public int QuestLevel
		{
			get
			{
				return this.questLevel;
			}
		}

		public bool IsHuntingQuest
		{
			get
			{
				return this.isHuntingQuest;
			}
		}

		public int InitGameTime
		{
			get
			{
				return this.initGameTime;
			}
		}

		public int SectorMoveGameTime
		{
			get
			{
				return this.sectorMoveGameTime;
			}
		}

		public bool IsGiantRaid
		{
			get
			{
				return this.isGiantRaid;
			}
		}

		public bool IsNoWaitingShip
		{
			get
			{
				return this.isHuntingQuest || this.IsGiantRaid;
			}
		}

		public string ItemLimit
		{
			get
			{
				return this.itemLimit;
			}
		}

		public string GearLimit
		{
			get
			{
				return this.gearLimit;
			}
		}

		public int Difficulty
		{
			get
			{
				return this.difficulty;
			}
		}

		public bool IsTimerDecreasing
		{
			get
			{
				return this.timerDecreasing;
			}
		}

		public int QuestStartedPlayerCount
		{
			get
			{
				return this.questStartedPlayerCount;
			}
		}

		public QueryMicroPlayInfo.FailReasonEnum FailReason
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

		public bool InitItemDropEntities { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new QueryMicroPlayInfo.Request(this);
		}

		[NonSerialized]
		private string questID;

		[NonSerialized]
		private QuestSectorInfos questSectorInfo;

		[NonSerialized]
		private bool hardMode;

		[NonSerialized]
		private int soloSector;

		[NonSerialized]
		private int timeLimit;

		[NonSerialized]
		private int questLevel;

		[NonSerialized]
		private string itemLimit;

		[NonSerialized]
		private string gearLimit;

		[NonSerialized]
		private bool isHuntingQuest;

		[NonSerialized]
		private int initGameTime;

		[NonSerialized]
		private int sectorMoveGameTime;

		[NonSerialized]
		private bool isGiantRaid;

		[NonSerialized]
		private int difficulty;

		[NonSerialized]
		private bool timerDecreasing;

		[NonSerialized]
		private int questStartedPlayerCount;

		[NonSerialized]
		private QueryMicroPlayInfo.FailReasonEnum failReason;

		public enum FailReasonEnum : byte
		{
			NoSuchMicroplay,
			Unknown
		}

		private class Request : OperationProcessor<QueryMicroPlayInfo>
		{
			public Request(QueryMicroPlayInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.FailReason = QueryMicroPlayInfo.FailReasonEnum.Unknown;
				if (base.Feedback is string)
				{
					base.Operation.questID = (string)base.Feedback;
					yield return null;
					if (base.Operation.InitItemDropEntities)
					{
						base.Operation.questSectorInfo = (base.Feedback as QuestSectorInfos);
						yield return null;
					}
					base.Operation.hardMode = (bool)base.Feedback;
					yield return null;
					base.Operation.soloSector = (int)base.Feedback;
					yield return null;
					base.Operation.timeLimit = (int)base.Feedback;
					yield return null;
					base.Operation.questLevel = (int)base.Feedback;
					yield return null;
					base.Operation.itemLimit = (base.Feedback as string);
					yield return null;
					base.Operation.gearLimit = (base.Feedback as string);
					yield return null;
					base.Operation.isHuntingQuest = (bool)base.Feedback;
					yield return null;
					base.Operation.initGameTime = (int)base.Feedback;
					yield return null;
					base.Operation.sectorMoveGameTime = (int)base.Feedback;
					yield return null;
					base.Operation.isGiantRaid = (bool)base.Feedback;
					yield return null;
					base.Operation.difficulty = (int)base.Feedback;
					yield return null;
					base.Operation.timerDecreasing = (bool)base.Feedback;
					yield return null;
					base.Operation.questStartedPlayerCount = (int)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.FailReason = (QueryMicroPlayInfo.FailReasonEnum)base.Feedback;
				}
				yield break;
			}
		}
	}
}
