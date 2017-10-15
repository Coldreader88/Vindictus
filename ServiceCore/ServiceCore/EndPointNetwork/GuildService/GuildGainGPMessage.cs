using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildGainGPMessage : IMessage
	{
		public long GuildPoint { get; set; }

		public Dictionary<byte, int> DailyGainGP { get; set; }

		public GuildGainGPMessage(long guildPoint)
		{
			this.GuildPoint = guildPoint;
			this.DailyGainGP = new Dictionary<byte, int>();
		}

		public GuildGainGPMessage(long guildPoint, Dictionary<byte, int> dailyGainGP)
		{
			this.GuildPoint = guildPoint;
			this.DailyGainGP = dailyGainGP;
		}

		public override string ToString()
		{
			string text = "";
			foreach (KeyValuePair<byte, int> keyValuePair in this.DailyGainGP)
			{
				if (text.Length > 0)
				{
					text += ", ";
				}
				text += keyValuePair.Key.ToString();
				text += ": ";
				text += keyValuePair.Value;
			}
			return string.Format("GuildGainGPMessage[ {0} ]", text);
		}
	}
}
