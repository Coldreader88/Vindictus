using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Pvp
{
	[Serializable]
	public sealed class PvpChannelListMessage : IMessage
	{
		public List<PvpChannelInfo> PvpChannelInfos { get; set; }

		public string PvpMode { get; set; }

		public string Key { get; set; }

		public PvpChannelListMessage(string pvpMode, string key, List<PvpChannelInfo> list)
		{
			this.PvpMode = pvpMode;
			this.Key = key;
			this.PvpChannelInfos = list;
		}

		public override string ToString()
		{
			return string.Format("PvpChannelListMessage[ x {0}]", this.PvpChannelInfos.Count);
		}
	}
}
