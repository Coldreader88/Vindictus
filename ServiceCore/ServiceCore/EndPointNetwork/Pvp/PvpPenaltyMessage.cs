using System;

namespace ServiceCore.EndPointNetwork.Pvp
{
	[Serializable]
	public sealed class PvpPenaltyMessage : IMessage
	{
		public int PlayerIndex { get; set; }

		public override string ToString()
		{
			return string.Format("PvpPanaltyMessage[ PlayerIndex = {0} ]", this.PlayerIndex);
		}
	}
}
