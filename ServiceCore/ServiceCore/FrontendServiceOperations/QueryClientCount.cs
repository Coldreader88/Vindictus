using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class QueryClientCount : Operation
	{
		public Dictionary<int, Dictionary<string, int>> StateCounts
		{
			get
			{
				return this.stateCounts;
			}
		}

		public Dictionary<byte, Dictionary<string, int>> StateChannelCounts
		{
			get
			{
				return this.stateChannelCounts;
			}
		}

		public override int TimeOut
		{
			get
			{
				return this.timeOut;
			}
			set
			{
				this.timeOut = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryClientCount.Request(this);
		}

		[NonSerialized]
		private Dictionary<int, Dictionary<string, int>> stateCounts;

		[NonSerialized]
		private Dictionary<byte, Dictionary<string, int>> stateChannelCounts;

		[NonSerialized]
		private int timeOut;

		private class Request : OperationProcessor<QueryClientCount>
		{
			public Request(QueryClientCount op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.stateCounts = new Dictionary<int, Dictionary<string, int>>();
				int count = (int)base.Feedback;
				for (int i = 0; i < count; i++)
				{
					yield return null;
					int regionCode = (int)base.Feedback;
					yield return null;
					base.Operation.stateCounts.Add(regionCode, base.Feedback as Dictionary<string, int>);
				}
				if (FeatureMatrix.IsEnable("Channeling_UserCount"))
				{
					yield return null;
					base.Operation.stateChannelCounts = new Dictionary<byte, Dictionary<string, int>>();
					int channelCount = (int)base.Feedback;
					for (int j = 0; j < channelCount; j++)
					{
						yield return null;
						byte channelCode = (byte)base.Feedback;
						yield return null;
						base.Operation.stateChannelCounts.Add(channelCode, base.Feedback as Dictionary<string, int>);
					}
				}
				yield break;
			}
		}
	}
}
