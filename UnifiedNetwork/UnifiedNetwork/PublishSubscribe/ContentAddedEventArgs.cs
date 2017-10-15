using System;

namespace UnifiedNetwork.PublishSubscribe
{
	public class ContentAddedEventArgs<TPub, TSub> : EventArgs
	{
		public ContentAddedEventArgs(TPub publisher, TSub subscriber)
		{
			this.Publisher = publisher;
			this.Subscriber = subscriber;
		}

		public TPub Publisher { get; private set; }

		public TSub Subscriber { get; private set; }
	}
}
