using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PlayerServiceOperations
{
	[Serializable]
	public class QueryIsHostQuest : Operation
	{
		public string QuestID
		{
			get
			{
				return this.questID;
			}
		}

		public bool IsHostOnlyQuest
		{
			get
			{
				return this.isHostOnlyQuest;
			}
		}

		public QueryIsHostQuest(string questID)
		{
			this.questID = questID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryIsHostQuest.Request(this);
		}

		private string questID;

		[NonSerialized]
		private bool isHostOnlyQuest;

		private class Request : OperationProcessor<QueryIsHostQuest>
		{
			public Request(QueryIsHostQuest op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is bool)
				{
					base.Operation.isHostOnlyQuest = (bool)base.Feedback;
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
