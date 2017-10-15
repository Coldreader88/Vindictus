using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class MoveToQuestHost : Operation
	{
		public bool IsToQuestHost { get; set; }

		public string QuestID { get; set; }

		public string Title { get; set; }

		public int SwearID { get; set; }

		public ShipOptionInfo Option { get; set; }

		public MoveToQuestHost()
		{
			this.IsToQuestHost = false;
		}

		public MoveToQuestHost(string questID, string title, int swearID, ShipOptionInfo option)
		{
			this.IsToQuestHost = true;
			this.QuestID = questID;
			this.Title = title;
			this.SwearID = swearID;
			this.Option = option;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
