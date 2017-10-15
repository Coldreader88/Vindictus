using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class QueryRecommendShip : Operation
	{
		public int Level { get; set; }

		public bool IsAdult { get; set; }

		public int Difficulty { get; set; }

		public ICollection<string> CleardStoryQuestIDs { get; set; }

		public ICollection<string> QuestIDs { get; set; }

		public ICollection<string> UnavailableQuestIDs { get; set; }

		public ICollection<string> PracticeModeQuestIDList { get; set; }

		public bool IsSeason2 { get; set; }

		public ICollection<int> SelectedBossQuestIDInfos { get; set; }

		public ICollection<ShipInfo> RecommendedShip
		{
			get
			{
				return this.recommendedShip;
			}
			set
			{
				this.recommendedShip = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRecommendShip.Request(this);
		}

		[NonSerialized]
		private ICollection<ShipInfo> recommendedShip;

		private class Request : OperationProcessor<QueryRecommendShip>
		{
			public Request(QueryRecommendShip op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.RecommendedShip = ((base.Feedback as IList<ShipInfo>) ?? ((IList<ShipInfo>)new ShipInfo[0]));
				yield break;
			}
		}
	}
}
