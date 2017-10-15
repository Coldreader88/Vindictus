using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GRCServiceOperations
{
	[Serializable]
	public sealed class GameResourceRespond : Operation
	{
		public long CharacterID { get; set; }

		public GameResourceRespond(long charcterID, string param)
		{
			this.CharacterID = this.CharacterID;
			this.RespondParam = param;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}

		public string RespondParam;
	}
}
