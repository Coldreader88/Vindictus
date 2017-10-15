using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class StartTrialEvent : Operation
	{
		public int SectorGroupID { get; set; }

		public string FactorName { get; set; }

		public int TimeLimit { get; set; }

		public List<int> ActorsIndex { get; set; }

		public StartTrialEvent(int sectorGroupID, string factorName, int timeLimit, List<int> actorsIndex)
		{
			this.SectorGroupID = sectorGroupID;
			this.FactorName = factorName;
			this.TimeLimit = timeLimit;
			this.ActorsIndex = actorsIndex;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new StartTrialEvent.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(StartTrialEvent op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Result = (base.Feedback is OkMessage);
				yield break;
			}
		}
	}
}
