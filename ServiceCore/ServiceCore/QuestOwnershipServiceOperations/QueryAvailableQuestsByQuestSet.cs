using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class QueryAvailableQuestsByQuestSet : Operation
	{
		public int QuestSet { get; set; }

		public int Difficulty { get; set; }

		public QueryAvailableQuestsByQuestSet(int questSet, int difficulty)
		{
			this.QuestSet = questSet;
			this.Difficulty = difficulty;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryAvailableQuestsByQuestSet.Request(this);
		}

		[NonSerialized]
		public ICollection<string> ResultQuestIDList;

		[NonSerialized]
		public ICollection<string> UnavailableQuestIDList;

		[NonSerialized]
		public ICollection<string> PracticeModeQuestIDList;

		private class Request : OperationProcessor<QueryAvailableQuestsByQuestSet>
		{
			public Request(QueryAvailableQuestsByQuestSet op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IList<string>)
				{
					base.Operation.ResultQuestIDList = (base.Feedback as IList<string>);
					yield return null;
					base.Operation.UnavailableQuestIDList = (base.Feedback as IList<string>);
					yield return null;
					base.Operation.PracticeModeQuestIDList = (base.Feedback as IList<string>);
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
