using System;

namespace ServiceCore.EndPointNetwork.Pvp
{
	[Serializable]
	public sealed class ArenaResultInfoMessage : IMessage
	{
		public int ArenaWinCount { get; set; }

		public int ArenaLoseCount { get; set; }

		public int ArenaSuccessiveWinCount { get; set; }

		public ArenaResultInfoMessage(int arenaWinCount, int arenaLoseCount, int arenaSuccessiveWinCount)
		{
			this.ArenaWinCount = arenaWinCount;
			this.ArenaLoseCount = arenaLoseCount;
			this.ArenaSuccessiveWinCount = arenaSuccessiveWinCount;
		}

		public override string ToString()
		{
			return string.Format("ArenaResultInfoMessage[ {0} : {1} : {2} ]", this.ArenaWinCount, this.ArenaLoseCount, this.ArenaSuccessiveWinCount);
		}
	}
}
