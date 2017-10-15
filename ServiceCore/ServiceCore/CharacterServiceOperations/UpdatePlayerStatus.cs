using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class UpdatePlayerStatus : Operation
	{
		public bool IsInGame { get; set; }

		public UpdatePlayerStatus(bool isInGame)
		{
			this.IsInGame = isInGame;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
