using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class UpdateArenaWinCount : Operation
	{
		public int ArenaWinCount { get; set; }

		public int ArenaLoseCount { get; set; }

		public int ArenaSuccessiveWinCount { get; set; }

		public UpdateArenaWinCount(int arenaWinCount, int arenaLoseCount, int arenaSuccessiveWinCount)
		{
			this.ArenaWinCount = arenaWinCount;
			this.ArenaLoseCount = arenaLoseCount;
			this.ArenaSuccessiveWinCount = arenaSuccessiveWinCount;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
