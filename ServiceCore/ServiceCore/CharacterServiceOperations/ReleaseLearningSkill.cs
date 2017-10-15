using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class ReleaseLearningSkill : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new ReleaseLearningSkill.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(ReleaseLearningSkill op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				yield break;
			}
		}
	}
}
