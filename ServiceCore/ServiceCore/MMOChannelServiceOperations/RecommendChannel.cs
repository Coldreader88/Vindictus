using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MMOChannelServiceOperations
{
	[Serializable]
	public class RecommendChannel : Operation
	{
		public long ChannelID
		{
			get
			{
				return this.channelID;
			}
		}

		public int ServiceID
		{
			get
			{
				return this.serviceID;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RecommendChannel.Request(this);
		}

		[NonSerialized]
		private long channelID = -1L;

		[NonSerialized]
		private int serviceID = -1;

		private class Request : OperationProcessor<RecommendChannel>
		{
			public Request(RecommendChannel op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback.GetType() != typeof(int))
				{
					base.Result = false;
				}
				else
				{
					base.Operation.serviceID = (int)base.Feedback;
					yield return null;
					base.Operation.channelID = (long)base.Feedback;
				}
				yield break;
			}
		}
	}
}
