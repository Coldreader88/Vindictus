using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildLevelUpMessage : IMessage
	{
		public int Level { get; set; }

		public override string ToString()
		{
			return string.Format("GuildLevelUpMessage[ {0} ]", this.Level);
		}
	}
}
