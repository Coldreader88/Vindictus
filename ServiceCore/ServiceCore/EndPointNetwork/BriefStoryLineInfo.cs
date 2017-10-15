using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BriefStoryLineInfo
	{
		public string StoryLine { get; set; }

		public string Phase { get; set; }

		public byte Status { get; set; }

		public string PhaseText { get; set; }

		public BriefStoryLineInfo(string storyLine, string phase, byte status)
		{
			this.StoryLine = storyLine;
			this.Phase = phase;
			this.Status = status;
			this.PhaseText = "";
		}

		public BriefStoryLineInfo(string storyLine, string phase, byte status, string phaseText)
		{
			this.StoryLine = storyLine;
			this.Phase = phase;
			this.Status = status;
			this.PhaseText = phaseText;
		}

		public override string ToString()
		{
			return string.Format("[{2}]{0}/{1}", this.StoryLine, this.Phase, this.Status);
		}
	}
}
