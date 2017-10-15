using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class ModifyStoryProgress : Operation
	{
		public IDictionary<string, string> Progress { get; set; }

		public bool IgnoreReward { get; set; }

		public ModifyStoryProgress(Dictionary<string, string> progress, bool ignoreReward)
		{
			this.Progress = progress;
			this.IgnoreReward = ignoreReward;
		}

		public ModifyStoryProgress(string storyLineID, string phaseID)
		{
			this.Progress = new Dictionary<string, string>
			{
				{
					storyLineID,
					phaseID
				}
			};
			this.IgnoreReward = false;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
