using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TradeRequestMyItem : Operation
	{
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

		public TradeResultCode RC
		{
			get
			{
				return this.resultCode;
			}
			set
			{
				this.resultCode = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new TradeRequestMyItem.Request(this);
		}

		[NonSerialized]
		private ICollection<TradeItemInfo> searchResult;

		[NonSerialized]
		private TradeResultCode resultCode;

		private class Request : OperationProcessor<TradeRequestMyItem>
		{
			public Request(TradeRequestMyItem op) : base(op)
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
