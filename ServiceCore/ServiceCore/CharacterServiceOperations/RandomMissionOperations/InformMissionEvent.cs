using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public sealed class InformMissionEvent : Operation
	{
		public long MissionID
		{
			get
			{
				return this._id;
			}
		}

		public int QuestIdx
		{
			get
			{
				return this._questIdx;
			}
		}

		public int ProcessCount
		{
			get
			{
				return this._processCount;
			}
		}

		public bool MissionComplete
		{
			get
			{
				return this.missionComplete;
			}
		}

		public InformMissionEvent(long id, int questIdx, int processCount)
		{
			this._id = id;
			this._questIdx = questIdx;
			this._processCount = processCount;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new InformMissionEvent.Request(this);
		}

		private long _id;

		private int _questIdx;

		private int _processCount;

		[NonSerialized]
		private bool missionComplete;

		private class Request : OperationProcessor<InformMissionEvent>
		{
			public Request(InformMissionEvent op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.missionComplete = (bool)base.Feedback;
				yield break;
			}
		}
	}
}
