using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class UpdateGuildStorageSettingMessage : IMessage
	{
		public int GoldLimit { get; set; }

		public long AccessLimtiTag { get; set; }

		public override string ToString()
		{
			return string.Format("UpdateGuildStorageSettingMessage[]", new object[0]);
		}
	}
}
