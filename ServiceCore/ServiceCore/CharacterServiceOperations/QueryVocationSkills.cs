using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryVocationSkills : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new QueryVocationSkills.Request(this);
		}

		[NonSerialized]
		public IDictionary<string, int> Skills;

		private class Request : OperationProcessor<QueryVocationSkills>
		{
			public Request(QueryVocationSkills op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is IDictionary<string, int>)
				{
					base.Operation.Skills = (base.Feedback as IDictionary<string, int>);
					base.Result = true;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
