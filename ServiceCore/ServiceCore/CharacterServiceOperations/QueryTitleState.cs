using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryTitleState : Operation
	{
		public int TitleID
		{
			get
			{
				return this.titleID;
			}
		}

		public QueryTitleState(int titleID)
		{
			this.titleID = titleID;
		}

		public TitleState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryTitleState.Request(this);
		}

		private int titleID;

		[NonSerialized]
		private TitleState state;

		private class Request : OperationProcessor<QueryTitleState>
		{
			public Request(QueryTitleState op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.State = (TitleState)base.Feedback;
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
