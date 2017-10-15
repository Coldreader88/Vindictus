using System;
using System.Collections.Generic;

namespace UnifiedNetwork.PublishSubscribe
{
	public class TopicClosedEventArgs<TPub, TSub> : EventArgs
	{
		public TopicClosedEventArgs(short topic, IEnumerable<TPub> publishers, IEnumerable<TSub> subscribers)
		{
			this.Topic = topic;
			this.Publishers = publishers;
			this.Subscribers = subscribers;
		}

		public short Topic { get; private set; }

		public IEnumerable<TPub> Publishers { get; private set; }

		public IEnumerable<TSub> Subscribers { get; private set; }
	}
}
