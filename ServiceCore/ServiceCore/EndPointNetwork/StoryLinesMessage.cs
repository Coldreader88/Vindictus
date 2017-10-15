using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StoryLinesMessage : IMessage
	{
		public ICollection<BriefStoryLineInfo> StoryStatus { get; set; }

		public int HintCategory { get; set; }

		public StoryLinesMessage(ICollection<BriefStoryLineInfo> storyStatus, int hintCategoty)
		{
			this.StoryStatus = storyStatus;
			this.HintCategory = hintCategoty;
		}

		public override string ToString()
		{
			return string.Format("StoryLinesMessage[ status x {0}]", this.StoryStatus.Count);
		}
	}
}
