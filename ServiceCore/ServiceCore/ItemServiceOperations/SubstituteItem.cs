using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class SubstituteItem : Operation
	{
		public IDictionary<string, int> TargetItems { get; private set; }

		public IDictionary<string, int> NewItems { get; private set; }

		public SubstituteItem()
		{
			this.TargetItems = new Dictionary<string, int>();
			this.NewItems = new Dictionary<string, int>();
		}

		public SubstituteItem(IDictionary<string, int> from, IDictionary<string, int> to)
		{
			this.TargetItems = from;
			this.NewItems = to;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
