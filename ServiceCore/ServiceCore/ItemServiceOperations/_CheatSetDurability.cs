using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class _CheatSetDurability : Operation
	{
		public int Part { get; set; }

		public int Durability { get; set; }

		public int MaxDurability { get; set; }

		public _CheatSetDurability(int Part, int Durability, int MaxDurability)
		{
			this.Part = Part;
			this.Durability = Durability;
			this.MaxDurability = MaxDurability;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
