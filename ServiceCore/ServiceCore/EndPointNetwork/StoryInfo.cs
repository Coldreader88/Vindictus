using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StoryInfo
	{
		public string StoryLineID { get; set; }

		public bool IsRevealing { get; set; }

		public StoryInfo(string StoryLineID, bool IsRevealing)
		{
			this.StoryLineID = StoryLineID;
			this.IsRevealing = IsRevealing;
		}

		public override string ToString()
		{
			return this.StoryLineID;
		}
	}
}
