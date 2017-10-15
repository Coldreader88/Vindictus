using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class GuildStorageLogsMessage : IMessage
	{
		public bool IsTodayLog { get; private set; }

		public ICollection<GuildStorageBriefLogElement> BriefLogs { get; private set; }

		public ICollection<GuildStorageItemLogElement> ItemLogs { get; private set; }

		public GuildStorageLogsMessage(bool isToday, ICollection<GuildStorageBriefLogElement> brief, ICollection<GuildStorageItemLogElement> item)
		{
			this.IsTodayLog = isToday;
			this.BriefLogs = brief;
			this.ItemLogs = item;
		}

		public override string ToString()
		{
			return string.Format("GuildStorageLogsMessage[]", new object[0]);
		}
	}
}
