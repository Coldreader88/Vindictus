using System;
using System.Collections.Generic;

namespace UnifiedNetwork.PublishSubscribe
{
	public class PublisherRemovedEventArgs<TPub, TSub> : EventArgs
	{
		public PublisherRemovedEventArgs(TPub publisher, IEnumerable<TSub> subscribers)
		{
			this.Publisher = publisher;
			this.Subscribers = subscribers;
		}

		public TPub Publisher { get; private set; }

		public IEnumerable<TSub> Subscribers { get; private set; }
	}
}
