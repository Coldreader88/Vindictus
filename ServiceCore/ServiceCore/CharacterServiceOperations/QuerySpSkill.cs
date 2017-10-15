using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QuerySpSkill : Operation
	{
		public IDictionary<int, string> SpSkills
		{
			get
			{
				return this.spSkills;
			}
			set
			{
				this.spSkills = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QuerySpSkill.Request(this);
		}

		[NonSerialized]
		private IDictionary<int, string> spSkills;

		private class Request : OperationProcessor<QuerySpSkill>
		{
			public Request(QuerySpSkill op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is Dictionary<int, string>)
				{
					object feedback = base.Feedback;
					base.Operation.SpSkills = (base.Feedback as Dictionary<int, string>);
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
