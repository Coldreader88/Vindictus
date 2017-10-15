using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class QueryNpcTalk : Operation
	{
		public string BuildingID { get; set; }

		public string NpcID { get; set; }

		public string StoryLine { get; set; }

		public string Command { get; set; }

		public bool CheatPermission { get; set; }

		public QueryNpcTalk(string BuildingID, string NpcID, string StoryLine, string Command, bool CheatPermission)
		{
			this.BuildingID = BuildingID;
			this.NpcID = NpcID;
			this.StoryLine = StoryLine;
			this.Command = Command;
			this.CheatPermission = CheatPermission;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
