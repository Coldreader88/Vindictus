using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class QueryQuestSettings : Operation
	{
		public string QuestID { get; set; }

		public int Difficulty { get; set; }

		public QuestSettings QuestSettings
		{
			get
			{
				return this.resultSettings;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryQuestSettings.Request(this);
		}

		[NonSerialized]
		private QuestSettings resultSettings;

		private class Request : OperationProcessor<QueryQuestSettings>
		{
			public Request(QueryQuestSettings op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is QuestSettings)
				{
					base.Operation.resultSettings = (base.Feedback as QuestSettings);
				}
				else
				{
					base.Operation.resultSettings = null;
				}
				yield break;
			}
		}
	}
}
