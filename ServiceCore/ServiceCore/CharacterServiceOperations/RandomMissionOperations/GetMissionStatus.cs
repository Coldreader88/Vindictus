using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public sealed class GetMissionStatus : Operation
	{
		public List<MissionInfo> ResultMissionList
		{
			get
			{
				return this.resultMissionList;
			}
			set
			{
				this.resultMissionList = value;
			}
		}

		public int CompletedMissionCount
		{
			get
			{
				return this.completedMissionCount;
			}
			set
			{
				this.completedMissionCount = value;
			}
		}

		public DateTime NextCompletedMissionCountClearTime
		{
			get
			{
				return this.nextCompletedMissionCountClearTime;
			}
			set
			{
				this.nextCompletedMissionCountClearTime = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GetMissionStatus.Request(this);
		}

		[NonSerialized]
		private List<MissionInfo> resultMissionList;

		[NonSerialized]
		private int completedMissionCount;

		[NonSerialized]
		private DateTime nextCompletedMissionCountClearTime;

		private class Request : OperationProcessor<GetMissionStatus>
		{
			public Request(GetMissionStatus op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.ResultMissionList = (base.Feedback as List<MissionInfo>);
				yield return null;
				base.Operation.CompletedMissionCount = (int)base.Feedback;
				yield return null;
				base.Operation.NextCompletedMissionCountClearTime = (DateTime)base.Feedback;
				yield break;
			}
		}
	}
}
