using System;
using System.Collections.Generic;

namespace UnifiedNetwork.PublishSubscribe
{
	public class Broker<TPub, TSub> : IDisposable
	{
		public short Topic { get; private set; }

		public IEnumerable<TPub> Publishers
		{
			get
			{
				return this.publishers.Keys;
			}
		}

		public event EventHandler<PublisherRemovedEventArgs<TPub, TSub>> PublisherRemoved;

		public IEnumerable<TSub> Subscribers
		{
			get
			{
				return this.subscribers.Keys;
			}
		}

		public event EventHandler<SubscriberRemovedEventArgs<TPub, TSub>> SubscriberRemoved;

		public event EventHandler<ContentAddedEventArgs<TPub, TSub>> ContentAdded;

		public event EventHandler<ContentRemovedEventArgs<TPub, TSub>> ContentRemoved;

		public event EventHandler<PublishingEventArgs<TPub, TSub>> Publishing;

		public event EventHandler<TopicClosedEventArgs<TPub, TSub>> TopicClosed;

		public Broker(short topic)
		{
			this.Topic = topic;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (this.TopicClosed != null)
				{
					this.TopicClosed(this, new TopicClosedEventArgs<TPub, TSub>(this.Topic, this.Publishers, this.Subscribers));
				}
				this.publishers = null;
				this.PublisherRemoved = null;
				this.subscribers = null;
				this.SubscriberRemoved = null;
				this.ContentAdded = null;
				this.ContentRemoved = null;
				this.Publishing = null;
				this.TopicClosed = null;
				this.disposed = true;
			}
		}

		~Broker()
		{
			this.Dispose(false);
		}

		private void AddPublisher(TPub publisher)
		{
			if (!this.publishers.ContainsKey(publisher))
			{
				this.publishers.Add(publisher, new HashSet<TSub>());
			}
		}

		public void RemovePublisher(TPub publisher)
		{
			HashSet<TSub> hashSet;
			if (this.publishers.TryGetValue(publisher, out hashSet))
			{
				this.publishers.Remove(publisher);
				IEnumerable<TSub> enumerable = hashSet;
				if (hashSet.Count < 1)
				{
					enumerable = new TSub[0];
				}
				foreach (TSub key in enumerable)
				{
					HashSet<TPub> hashSet2;
					if (this.subscribers.TryGetValue(key, out hashSet2) && hashSet2.Remove(publisher))
					{
						hashSet2.TrimExcess();
					}
				}
				if (this.PublisherRemoved != null)
				{
					this.PublisherRemoved(this, new PublisherRemovedEventArgs<TPub, TSub>(publisher, enumerable));
				}
			}
		}

		private void AddSubscriber(TSub subscriber)
		{
			if (!this.subscribers.ContainsKey(subscriber))
			{
				this.subscribers.Add(subscriber, new HashSet<TPub>());
			}
		}

		public void RemoveSubscriber(TSub subscriber)
		{
			HashSet<TPub> hashSet;
			if (this.subscribers.TryGetValue(subscriber, out hashSet))
			{
				this.subscribers.Remove(subscriber);
				IEnumerable<TPub> enumerable = hashSet;
				if (hashSet.Count < 1)
				{
					enumerable = new TPub[0];
				}
				foreach (TPub key in enumerable)
				{
					HashSet<TSub> hashSet2;
					if (this.publishers.TryGetValue(key, out hashSet2) && hashSet2.Remove(subscriber))
					{
						hashSet2.TrimExcess();
					}
				}
				if (this.SubscriberRemoved != null)
				{
					this.SubscriberRemoved(this, new SubscriberRemovedEventArgs<TPub, TSub>(subscriber, enumerable));
				}
			}
		}

		public void AddContent(TPub publisher, TSub subscriber)
		{
			this.AddPublisher(publisher);
			HashSet<TSub> hashSet = this.publishers[publisher];
			this.AddSubscriber(subscriber);
			HashSet<TPub> hashSet2 = this.subscribers[subscriber];
			hashSet2.Add(publisher);
			hashSet.Add(subscriber);
			if (this.ContentAdded != null)
			{
				this.ContentAdded(this, new ContentAddedEventArgs<TPub, TSub>(publisher, subscriber));
			}
		}

		public void RemoveContent(TPub publisher, TSub subscriber)
		{
			HashSet<TSub> hashSet;
			if (!this.publishers.TryGetValue(publisher, out hashSet))
			{
				return;
			}
			HashSet<TPub> hashSet2;
			if (!this.subscribers.TryGetValue(subscriber, out hashSet2))
			{
				return;
			}
			if (hashSet2.Remove(publisher))
			{
				hashSet2.TrimExcess();
			}
			if (hashSet.Remove(subscriber))
			{
				hashSet.TrimExcess();
			}
			if (this.ContentRemoved != null)
			{
				this.ContentRemoved(this, new ContentRemovedEventArgs<TPub, TSub>(publisher, subscriber));
			}
			if (hashSet.Count == 0)
			{
				this.RemovePublisher(publisher);
			}
			if (hashSet2.Count == 0)
			{
				this.RemoveSubscriber(subscriber);
			}
		}

		public void Publish(TPub publisher, object obj)
		{
			HashSet<TSub> hashSet;
			if (!this.publishers.TryGetValue(publisher, out hashSet))
			{
				return;
			}
			if (this.Publishing != null)
			{
				this.Publishing(this, new PublishingEventArgs<TPub, TSub>(publisher, hashSet, obj));
			}
		}

		private Dictionary<TPub, HashSet<TSub>> publishers = new Dictionary<TPub, HashSet<TSub>>();

		private Dictionary<TSub, HashSet<TPub>> subscribers = new Dictionary<TSub, HashSet<TPub>>();

		private bool disposed;
	}
}
