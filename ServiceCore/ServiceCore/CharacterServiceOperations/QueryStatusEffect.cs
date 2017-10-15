using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryStatusEffect : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new QueryStatusEffect.Request(this);
		}

		[NonSerialized]
		private List<StatusEffectElement> elements;

		private class Request : OperationProcessor<QueryStatusEffect>
		{
			public Request(QueryStatusEffect op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.elements = (base.Feedback as List<StatusEffectElement>);
				yield break;
			}
		}
	}
}
