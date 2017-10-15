using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class SetStoryLineStatus : Operation
	{
		public string StoryLineID { get; set; }

		public StoryLineStatus Status { get; set; }

		public SetStoryLineStatus(string StoryLineID, StoryLineStatus Status)
		{
			this.StoryLineID = StoryLineID;
			this.Status = Status;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
