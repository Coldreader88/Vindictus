using System;

namespace UnifiedNetwork.PublishSubscribe
{
	public class ContentRemovedEventArgs<TPub, TSub> : EventArgs
	{
		public ContentRemovedEventArgs(TPub publisher, TSub subscriber)
		{
			this.Publisher = publisher;
			this.Subscriber = subscriber;
		}

		public TPub Publisher { get; private set; }

		public TSub Subscriber { get; private set; }
	}
}
