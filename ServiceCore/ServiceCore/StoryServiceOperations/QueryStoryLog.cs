using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class QueryStoryLog : Operation
	{
		public string StoryLineID { get; set; }

		public QueryStoryLog(string StoryLineID)
		{
			this.StoryLineID = StoryLineID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
