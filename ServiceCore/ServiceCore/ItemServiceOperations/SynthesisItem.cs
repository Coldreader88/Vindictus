using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class SynthesisItem : Operation
	{
		public long BaseItemID { get; set; }

		public long LookItemID { get; set; }

		public string AdditionalItemClass { get; set; }

		public SynthesisItem(long baseItemID, long lookItemID, string additionalItemClass)
		{
			this.BaseItemID = baseItemID;
			this.LookItemID = lookItemID;
			this.AdditionalItemClass = additionalItemClass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
