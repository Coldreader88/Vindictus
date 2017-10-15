using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class HandleGuildStorageSessionMessage : IMessage
	{
		public bool IsStarted { get; set; }

		public override string ToString()
		{
			return string.Format("HandleGuildStorageSessionMessage[]", new object[0]);
		}
	}
}
