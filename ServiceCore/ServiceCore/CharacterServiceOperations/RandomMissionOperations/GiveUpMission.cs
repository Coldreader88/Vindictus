using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public sealed class GiveUpMission : Operation
	{
		public long MissionID
		{
			get
			{
				return this.missionID;
			}
		}

		public bool GiveUpMissionResult
		{
			get
			{
				return this.giveUpMissionResult;
			}
			set
			{
				this.giveUpMissionResult = value;
			}
		}

		public GiveUpMission(long _missionID)
		{
			this.missionID = _missionID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GiveUpMission.Request(this);
		}

		private long missionID;

		[NonSerialized]
		private bool giveUpMissionResult;

		private class Request : OperationProcessor<GiveUpMission>
		{
			public Request(GiveUpMission op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.GiveUpMissionResult = (bool)base.Feedback;
				yield break;
			}
		}
	}
}
