using System;

namespace ServiceCore.EndPointNetwork.Pvp
{
	[Serializable]
	public sealed class PvpQueryChannelListMessage : IMessage
	{
		public string PvpMode { get; set; }

		public string Key { get; set; }

		public override string ToString()
		{
			return string.Format("PvpQueryChannelListMessage[ PvpMode = {0} ]", this.PvpMode);
		}
	}
}
