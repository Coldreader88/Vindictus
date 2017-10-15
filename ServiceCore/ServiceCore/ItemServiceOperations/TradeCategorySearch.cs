using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TradeCategorySearch : Operation
	{
		public TradeResultCode RC
		{
			get
			{
				return this.rc;
			}
			set
			{
				this.rc = value;
			}
		}

		public ICollection<TradeItemInfo> SearchResult
		{
			get
			{
				return this.searchResult;
			}
			set
			{
				this.searchResult = value;
			}
		}

		public TradeCategorySearch(string tradeCategory, string tradeCategorySub, int minLevel, int maxLevel, int chunkPageNumber, int chunkSize, SortOrder order, bool isDescending, List<DetailOption> detailOptions)
		{
			this.TradeCategory = tradeCategory;
			this.TradeCategorySub = tradeCategorySub;
			this.MinLevel = minLevel;
			this.MaxLevel = maxLevel;
			this.ChunkPageNumber = chunkPageNumber;
			this.ChunkSize = chunkSize;
			this.Order = order;
			this.IsDescending = isDescending;
			this.DetailOptions = detailOptions;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new TradeCategorySearch.Request(this);
		}

		public string TradeCategory;

		public string TradeCategorySub;

		public int MinLevel;

		public int MaxLevel;

		public int ChunkPageNumber;

		public int ChunkSize;

		public SortOrder Order;

		public bool IsDescending;

		public List<DetailOption> DetailOptions;

		[NonSerialized]
		private TradeResultCode rc;

		[NonSerialized]
		private ICollection<TradeItemInfo> searchResult;

		private class Request : OperationProcessor<TradeCategorySearch>
		{
			public Request(TradeCategorySearch op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.RC = (TradeResultCode)base.Feedback;
				if (base.Operation.RC == TradeResultCode.Success)
				{
					yield return null;
					base.Operation.SearchResult = (ICollection<TradeItemInfo>)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
