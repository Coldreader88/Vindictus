using System;

namespace ServiceCore.EndPointNetwork.Pvp
{
	[Serializable]
	public sealed class PvpChannelInfo
	{
		public string Desc { get; set; }

		public int ChannelID { get; set; }

		public PvpChannelInfo(int channelID, string desc)
		{
			this.ChannelID = channelID;
			this.Desc = desc;
		}
	}
}
