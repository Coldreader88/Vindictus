using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class AvatarSynthesisItem : Operation
	{
		public long Material1ID { get; set; }

		public long Material2ID { get; set; }

		public long Material3ID { get; set; }

		public AvatarSynthesisItem(long material1ID, long material2ID, long material3ID)
		{
			this.Material1ID = material1ID;
			this.Material2ID = material2ID;
			this.Material3ID = material3ID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
