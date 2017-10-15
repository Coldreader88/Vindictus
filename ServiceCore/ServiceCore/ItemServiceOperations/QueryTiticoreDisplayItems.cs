using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryTiticoreDisplayItems : Operation
	{
		public string ItemClass { get; set; }

		public string TargetItemClass { get; set; }

		public ICollection<ColoredItem> TiticoreRareDisplayItems { get; set; }

		public ICollection<ColoredItem> TiticoreNormalDisplayItems { get; set; }

		public ICollection<ColoredItem> TiticoreKeyItems { get; set; }

		public QueryTiticoreDisplayItems(string itemClass, string targetItemClass)
		{
			this.ItemClass = itemClass;
			this.TargetItemClass = targetItemClass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryTiticoreDisplayItems.Request(this);
		}

		private class Request : OperationProcessor<QueryTiticoreDisplayItems>
		{
			public Request(QueryTiticoreDisplayItems op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.TiticoreRareDisplayItems = (ICollection<ColoredItem>)base.Feedback;
				yield return null;
				base.Operation.TiticoreNormalDisplayItems = (ICollection<ColoredItem>)base.Feedback;
				yield return null;
				base.Operation.TiticoreKeyItems = (ICollection<ColoredItem>)base.Feedback;
				yield break;
			}
		}
	}
}
