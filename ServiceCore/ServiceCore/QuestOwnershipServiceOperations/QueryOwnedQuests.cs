using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class QueryOwnedQuests : Operation
	{
		public ICollection<string> ResultQuestIDList
		{
			get
			{
				return this.resultQuestIDList;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryOwnedQuests.Request(this);
		}

		[NonSerialized]
		private ICollection<string> resultQuestIDList;

		private class Request : OperationProcessor<QueryOwnedQuests>
		{
			public Request(QueryOwnedQuests op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.resultQuestIDList = (base.Feedback as IList<string>);
				if (base.Operation.resultQuestIDList == null)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
