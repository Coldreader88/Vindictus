using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildStorageBriefLogElement
	{
		public string CharacterName { get; set; }

		public GuildStorageOperationType OperationType { get; set; }

		public int AddCount { get; set; }

		public int PickCount { get; set; }

		public int Datestamp { get; set; }

		public int Timestamp { get; set; }
	}
}
