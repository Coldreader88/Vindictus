using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class IncreaseFreeMatchLoseCount : Operation
	{
		public int Count { get; set; }

		public IncreaseFreeMatchLoseCount()
		{
		}

		public IncreaseFreeMatchLoseCount(int count)
		{
			this.Count = count;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
