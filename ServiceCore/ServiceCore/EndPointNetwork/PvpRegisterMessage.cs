using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpRegisterMessage : IMessage
	{
		public string PvpMode { get; set; }

		public PvpRegisterCheat Cheat { get; set; }

		public int ChannelID { get; set; }

		public override string ToString()
		{
			return string.Format("PvpRegisterMessage[ PvpMode = {0} Cheat = {1}]", this.PvpMode, this.Cheat);
		}
	}
}
