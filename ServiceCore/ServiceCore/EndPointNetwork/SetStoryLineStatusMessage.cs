using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetStoryLineStatusMessage : IMessage
	{
		public string StoryLineID { get; set; }

		public byte Status { get; set; }

		public SetStoryLineStatusMessage(string StoryLineID, byte Status)
		{
			this.StoryLineID = StoryLineID;
			this.Status = Status;
		}

		public override string ToString()
		{
			return string.Format("SetWatchingStoryLineMessage[ StoryLineID = {0} ]", this.StoryLineID);
		}
	}
}
