using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	[Serializable]
	public sealed class ReloadData : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new ReloadData.Request(this);
		}

		private class Request : OperationProcessor<ReloadData>
		{
			public Request(ReloadData op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield break;
			}
		}
	}
}
