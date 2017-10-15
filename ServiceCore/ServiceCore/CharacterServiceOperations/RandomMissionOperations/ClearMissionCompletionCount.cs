using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public sealed class ClearMissionCompletionCount : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new ClearMissionCompletionCount.Request(this);
		}

		private class Request : OperationProcessor<ClearMissionCompletionCount>
		{
			public Request(ClearMissionCompletionCount op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield break;
			}
		}
	}
}
