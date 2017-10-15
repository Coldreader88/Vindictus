using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public sealed class ForceToSchedule : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new ForceToSchedule.Request(this);
		}

		private class Request : OperationProcessor<ForceToSchedule>
		{
			public Request(ForceToSchedule op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield break;
			}
		}
	}
}
