using System;
using System.Collections.Generic;
using Utility;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpHostInfoMessage : IMessage
	{
		public GameInfo GameInfo { get; set; }

		public GameJoinMemberInfo MemberInfo { get; set; }

		public Dictionary<string, string> Config { get; set; }

		public PvpHostInfoMessage(GameInfo gameInfo, GameJoinMemberInfo memberInfo, Dictionary<string, string> config)
		{
			this.GameInfo = gameInfo;
			this.MemberInfo = memberInfo;
			this.Config = config;
		}

		public override string ToString()
		{
			return string.Format("PvpHostInfoMessage[ {0} info = {1} PvpMode = {2}]", this.GameInfo, this.MemberInfo, this.Config.TryGetValue("PvpMode"));
		}
	}
}
