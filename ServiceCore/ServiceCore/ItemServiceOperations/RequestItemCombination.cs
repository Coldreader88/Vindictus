using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RequestItemCombination : Operation
	{
		public string CombinedItemClass { get; set; }

		public List<long> PartsIDList { get; set; }

		public RequestItemCombination(string combinedItemClass, List<long> partsIDList)
		{
			this.CombinedItemClass = combinedItemClass;
			this.PartsIDList = partsIDList;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
