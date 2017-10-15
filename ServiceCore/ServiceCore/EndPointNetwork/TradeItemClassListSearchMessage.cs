using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TradeItemClassListSearchMessage : IMessage
	{
		public ICollection<string> ItemClassList { get; set; }

		public int uniqueNumber { get; set; }

		public int ChunkPageNumber { get; set; }

		public SortOrder Order { get; set; }

		public bool isDescending { get; set; }

		public List<DetailOption> DetailOptions { get; set; }
	}
}
