using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TradeCategorySearchMessage : IMessage
	{
		public string tradeCategory { get; set; }

		public string tradeCategorySub { get; set; }

		public int minLevel { get; set; }

		public int maxLevel { get; set; }

		public int uniqueNumber { get; set; }

		public int ChunkPageNumber { get; set; }

		public SortOrder Order { get; set; }

		public bool isDescending { get; set; }

		public List<DetailOption> DetailOptions { get; set; }
	}
}
