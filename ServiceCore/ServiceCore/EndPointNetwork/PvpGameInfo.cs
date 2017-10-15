using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpGameInfo
	{
		public IDictionary<int, string> TeamInfo { get; set; }

		public IDictionary<int, string> PlayerInfo { get; set; }

		public PvpGameInfo(Dictionary<int, string> teamInfo, Dictionary<int, string> playerInfo)
		{
			this.TeamInfo = teamInfo;
			this.PlayerInfo = playerInfo;
		}

		public override string ToString()
		{
			return string.Format("PvpGameInfo(teaminfo x {0}, playerInfo x {1})", this.TeamInfo.Count, this.PlayerInfo.Count);
		}
	}
}
