using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildStorageItemLogElement
	{
		public string CharacterName { get; set; }

		public bool IsAddItem { get; set; }

		public string ItemClass { get; set; }

		public int Count { get; set; }

		public int Datestamp { get; set; }

		public int Timestamp { get; set; }

		public int Color1 { get; set; }

		public int Color2 { get; set; }

		public int Color3 { get; set; }
	}
}
