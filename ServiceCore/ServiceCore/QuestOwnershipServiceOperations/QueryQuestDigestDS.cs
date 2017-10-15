using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class QueryQuestDigestDS : Operation
	{
		public string QuestID { get; set; }

		public QueryQuestDigestDS(string questID)
		{
			this.QuestID = questID;
		}

		public QuestDigest QuestDigest
		{
			get
			{
				return this.resultDigest;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryQuestDigestDS.Request(this);
		}

		[NonSerialized]
		private QuestDigest resultDigest;

		private class Request : OperationProcessor<QueryQuestDigestDS>
		{
			public Request(QueryQuestDigestDS op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.resultDigest = (base.Feedback as QuestDigest);
				if (base.Operation.resultDigest != null)
				{
					base.Result = true;
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
