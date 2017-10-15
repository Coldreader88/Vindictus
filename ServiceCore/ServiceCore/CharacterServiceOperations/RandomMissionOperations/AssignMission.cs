using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public sealed class AssignMission : Operation
	{
		public long RequestMissionID
		{
			get
			{
				return this.requestMissionID;
			}
			set
			{
				this.requestMissionID = value;
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

		public MissionAssignResult AssignResult
		{
			get
			{
				return this.assignResult;
			}
			set
			{
				this.assignResult = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new AssignMission.Request(this);
		}

		private long requestMissionID;

		[NonSerialized]
		private MissionInfo mission;

		[NonSerialized]
		private MissionAssignResult assignResult;

		private class Request : OperationProcessor<AssignMission>
		{
			public Request(AssignMission op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.AssignResult = (MissionAssignResult)base.Feedback;
				if (base.Operation.AssignResult == MissionAssignResult.Success)
				{
					yield return null;
					base.Operation.Mission = (base.Feedback as MissionInfo);
				}
				yield break;
			}
		}
	}
}
