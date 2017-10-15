using System;

namespace ServiceCore.EndPointNetwork.Pvp
{
	[Serializable]
	public sealed class PvpConfirmJoinMessage : IMessage
	{
		public string PvpMode { get; set; }

		public override string ToString()
		{
			return string.Format("PvpConfirmJoinMessage[ PvpMode = {0} ]", this.PvpMode);
		}
	}
}
