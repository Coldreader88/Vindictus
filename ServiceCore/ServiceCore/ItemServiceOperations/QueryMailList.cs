using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryMailList : Operation
	{
		public List<BriefMailInfo> RecvMail
		{
			get
			{
				return this.recv;
			}
		}

		public List<BriefMailInfo> SentMail
		{
			get
			{
				return this.sent;
			}
		}

		public int NewMailCount
		{
			get
			{
				return this.newMailCount;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryMailList.Request(this);
		}

		[NonSerialized]
		private List<BriefMailInfo> recv;

		[NonSerialized]
		private List<BriefMailInfo> sent;

		[NonSerialized]
		private int newMailCount;

		private class Request : OperationProcessor<QueryMailList>
		{
			public Request(QueryMailList op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.newMailCount = (int)base.Feedback;
				yield return null;
				base.Operation.recv = (List<BriefMailInfo>)base.Feedback;
				yield return null;
				base.Operation.sent = (List<BriefMailInfo>)base.Feedback;
				yield break;
			}
		}
	}
}
