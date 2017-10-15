using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QuerySkillState : Operation
	{
		public string SkillID
		{
			get
			{
				return this.skillID;
			}
		}

		public QuerySkillState(string skillID)
		{
			this.skillID = skillID;
		}

		public int Rank
		{
			get
			{
				return this.rank;
			}
			set
			{
				this.rank = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QuerySkillState.Request(this);
		}

		private string skillID;

		[NonSerialized]
		private int rank;

		private class Request : OperationProcessor<QuerySkillState>
		{
			public Request(QuerySkillState op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.Rank = (int)base.Feedback;
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
