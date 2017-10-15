using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class QueryDSQuestList : Operation
	{
		public List<string> DSQuestList
		{
			get
			{
				return this.dsQuestList;
			}
			set
			{
				this.dsQuestList = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryDSQuestList.Request(this);
		}

		[NonSerialized]
		private List<string> dsQuestList;

		private class Request : OperationProcessor<QueryDSQuestList>
		{
			public Request(QueryDSQuestList op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is List<string>)
				{
					base.Operation.DSQuestList = (base.Feedback as List<string>);
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
