using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryNpcTalkMessage : IMessage
	{
		public string Location { get; set; }

		public string NpcID { get; set; }

		public string StoryLine { get; set; }

		public string Command { get; set; }

		public QueryNpcTalkMessage(string Location, string NpcID, string StoryLine, string Command)
		{
			this.Location = Location;
			this.NpcID = NpcID;
			this.StoryLine = StoryLine;
			this.Command = Command;
		}

		public override string ToString()
		{
			return string.Format("QueryNpcTalkMessage[ {0}/{1} : {2}/{3}]", new object[]
			{
				this.Location,
				this.NpcID,
				this.StoryLine,
				this.Command
			});
		}
	}
}
