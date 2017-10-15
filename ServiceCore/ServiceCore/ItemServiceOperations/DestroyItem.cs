using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class DestroyItem : Operation
	{
		public IDictionary<string, int> ItemList { get; set; }

		public bool DestroyExpiredItem { get; set; }

		public GiveItem.SourceEnum Source { get; set; }

		public DestroyItem(string iclass, int num, GiveItem.SourceEnum source)
		{
			this.ItemList = new Dictionary<string, int>
			{
				{
					iclass,
					num
				}
			};
			this.Source = source;
		}

		public DestroyItem(IDictionary<string, int> itemlist, GiveItem.SourceEnum source)
		{
			this.ItemList = itemlist;
			this.Source = source;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new DestroyItem.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(DestroyItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Result = (base.Feedback is OkMessage);
				yield break;
			}
		}
	}
}
