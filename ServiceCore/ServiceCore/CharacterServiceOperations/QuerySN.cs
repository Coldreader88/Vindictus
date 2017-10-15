using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QuerySN : Operation
	{
		public int NexonSN
		{
			get
			{
				return this.nexonSN;
			}
		}

		public int CharacterSN
		{
			get
			{
				return this.characterSN;
			}
		}

		public string CharacterName
		{
			get
			{
				return this.characterName;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QuerySN.Request(this);
		}

		public static QuerySN MakeResult(int nexonSN, int cSN, string name)
		{
			return new QuerySN
			{
				characterName = name,
				nexonSN = nexonSN,
				characterSN = cSN
			};
		}

		[NonSerialized]
		private int nexonSN;

		[NonSerialized]
		private int characterSN;

		[NonSerialized]
		private string characterName;

		private class Request : OperationProcessor<QuerySN>
		{
			public Request(QuerySN op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Result = true;
					base.Operation.nexonSN = (int)base.Feedback;
					yield return null;
					base.Operation.characterSN = (int)base.Feedback;
					yield return null;
					base.Operation.characterName = (base.Feedback as string);
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
