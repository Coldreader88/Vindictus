using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public sealed class ShowAllPostedMission : Operation
	{
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		public DateTime NextPostingTime
		{
			get
			{
				return this.nextPostingTime;
			}
			set
			{
				this.nextPostingTime = value;
			}
		}

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

		public override OperationProcessor RequestProcessor()
		{
			return new ShowAllPostedMission.Request(this);
		}

		private string location;

		[NonSerialized]
		private DateTime nextPostingTime;

		[NonSerialized]
		private List<MissionInfo> resultMissionList;

		private class Request : OperationProcessor<ShowAllPostedMission>
		{
			public Request(ShowAllPostedMission op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.NextPostingTime = (DateTime)base.Feedback;
				yield return null;
				base.Operation.ResultMissionList = (base.Feedback as List<MissionInfo>);
				yield break;
			}
		}
	}
}
