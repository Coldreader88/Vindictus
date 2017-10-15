using System;
using System.Collections.Generic;
using Utility;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpGameHostedMessage : IMessage
	{
		public PvpGameInfo GameInfo { get; set; }

		public GameJoinMemberInfo HostInfo { get; set; }

		public int TeamID { get; set; }

		public int Tag { get; set; }

		public Dictionary<string, string> Config { get; set; }

		public PvpGameHostedMessage(GameJoinMemberInfo hostInfo, PvpTeamID teamID, int tag, Dictionary<string, string> config, PvpGameInfo gameInfo)
		{
			this.HostInfo = hostInfo;
			this.TeamID = (int)teamID;
			this.Tag = tag;
			this.Config = config;
			this.GameInfo = gameInfo;
		}

		public override string ToString()
		{
			return string.Format("PvpGameHostedMessage[ Host {0} team = {1} Tag = {2} {3} PvpMode = {4}]", new object[]
			{
				(this.HostInfo == null) ? "DS" : this.HostInfo.Name,
				(PvpTeamID)this.TeamID,
				this.Tag,
				this.GameInfo,
				this.Config.TryGetValue("PvpMode")
			});
		}
	}
}
