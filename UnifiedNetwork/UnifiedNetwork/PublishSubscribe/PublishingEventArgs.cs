using System;
using System.Collections.Generic;

namespace UnifiedNetwork.PublishSubscribe
{
	public class PublishingEventArgs<TPub, TSub> : EventArgs
	{
		public PublishingEventArgs(TPub publisher, IEnumerable<TSub> subscribers, object tag)
		{
			this.Publisher = publisher;
			this.Subscribers = subscribers;
			this.Tag = tag;
		}

		public TPub Publisher { get; private set; }

		public IEnumerable<TSub> Subscribers { get; private set; }

		public object Tag { get; private set; }
	}
}
