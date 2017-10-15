using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public sealed class CompleteMission : Operation
	{
		public long MissionID
		{
			get
			{
				return this.missionID;
			}
		}

		public CompleteMissionResult CompleteMissionResult
		{
			get
			{
				return this.completeMissionResult;
			}
			set
			{
				this.completeMissionResult = value;
			}
		}

		public MissionInfo Mission
		{
			get
			{
				return this.mission;
			}
			set
			{
				this.mission = value;
			}
		}

		public CompleteMission(long _missionID)
		{
			this.missionID = _missionID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CompleteMission.Request(this);
		}

		private long missionID;

		[NonSerialized]
		private CompleteMissionResult completeMissionResult;

		[NonSerialized]
		private MissionInfo mission;

		private class Request : OperationProcessor<CompleteMission>
		{
			public Request(CompleteMission op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.CompleteMissionResult = (CompleteMissionResult)base.Feedback;
				if (base.Operation.CompleteMissionResult == CompleteMissionResult.Success)
				{
					yield return null;
					base.Operation.Mission = (base.Feedback as MissionInfo);
				}
				yield break;
			}
		}
	}
}
