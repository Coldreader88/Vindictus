using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class UnequipClassifiedParts : Operation
	{
		public string Class { get; private set; }

		public UnequipClassifiedParts(string TargetClass)
		{
			this.Class = TargetClass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
