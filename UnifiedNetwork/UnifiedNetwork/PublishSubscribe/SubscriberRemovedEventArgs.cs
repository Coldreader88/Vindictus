using System;
using System.Collections.Generic;

namespace UnifiedNetwork.PublishSubscribe
{
	public class SubscriberRemovedEventArgs<TPub, TSub> : EventArgs
	{
		public SubscriberRemovedEventArgs(TSub subscriber, IEnumerable<TPub> publishers)
		{
			this.Subscriber = subscriber;
			this.Publishers = publishers;
		}

		public TSub Subscriber { get; private set; }

		public IEnumerable<TPub> Publishers { get; private set; }
	}
}
