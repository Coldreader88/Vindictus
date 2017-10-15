using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public sealed class ReloadCharacterMissionList : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new ReloadCharacterMissionList.Request(this);
		}

		private class Request : OperationProcessor<ReloadCharacterMissionList>
		{
			public Request(ReloadCharacterMissionList op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield break;
			}
		}
	}
}
