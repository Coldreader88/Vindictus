using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class IncreaseFreeMatchWinCount : Operation
	{
		public int Count { get; set; }

		public IncreaseFreeMatchWinCount()
		{
		}

		public IncreaseFreeMatchWinCount(int count)
		{
			this.Count = count;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
