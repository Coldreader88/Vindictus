using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryMailInfo : Operation
	{
		public long MailID { get; set; }

		public QueryMailInfo(long MailID)
		{
			this.MailID = MailID;
		}

		public MailInfo ResultMessage
		{
			get
			{
				return this.resultMessage;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryMailInfo.Request(this);
		}

		[NonSerialized]
		private MailInfo resultMessage;

		private class Request : OperationProcessor<QueryMailInfo>
		{
			public Request(QueryMailInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.resultMessage = (MailInfo)base.Feedback;
				yield break;
			}
		}
	}
}
