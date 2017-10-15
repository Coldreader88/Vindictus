using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class AddMicroPlayCount : Operation
	{
		public int StartTime { get; private set; }

		public bool IsTokenQuest { get; private set; }

		public bool IsFreeQuest { get; private set; }

		public AddMicroPlayCount(int startTime, bool isToken, bool isFree)
		{
			this.StartTime = startTime;
			this.IsTokenQuest = isToken;
			this.IsFreeQuest = isFree;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
