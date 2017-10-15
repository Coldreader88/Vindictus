using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class NMInfo : Operation
	{
		public long CID
		{
			get
			{
				return this.cid;
			}
			private set
			{
				this.cid = value;
			}
		}

		public int NexonSN
		{
			get
			{
				return this.nexonSN;
			}
			private set
			{
				this.nexonSN = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NMInfo.Request(this);
		}

		[NonSerialized]
		private long cid;

		[NonSerialized]
		private int nexonSN;

		private class Request : OperationProcessor<NMInfo>
		{
			public Request(NMInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is FailMessage)
				{
					base.Result = false;
				}
				else
				{
					base.Operation.CID = (long)base.Feedback;
					yield return null;
					base.Operation.NexonSN = (int)base.Feedback;
				}
				yield break;
			}
		}
	}
}
