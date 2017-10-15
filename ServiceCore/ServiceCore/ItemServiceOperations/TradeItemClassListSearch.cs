using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TradeItemClassListSearch : Operation
	{
		public ICollection<string> ItemClasses { get; set; }

		public int ChunkPageNumber { get; set; }

		public int ChunkSize { get; set; }

		public List<DetailOption> DetailOptions { get; private set; }

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

		public TradeItemClassListSearch(ICollection<string> itemclasses, int chunkPageNumber, int chunkSize, SortOrder order, bool isDescending, List<DetailOption> detailOptions)
		{
			this.ItemClasses = itemclasses;
			this.ChunkPageNumber = chunkPageNumber;
			this.ChunkSize = chunkSize;
			this.Order = order;
			this.IsDescending = isDescending;
			this.DetailOptions = detailOptions;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new TradeItemClassListSearch.Request(this);
		}

		public SortOrder Order;

		public bool IsDescending;

		[NonSerialized]
		private TradeResultCode rc;

		[NonSerialized]
		private ICollection<TradeItemInfo> searchResult;

		private class Request : OperationProcessor<TradeItemClassListSearch>
		{
			public Request(TradeItemClassListSearch op) : base(op)
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
