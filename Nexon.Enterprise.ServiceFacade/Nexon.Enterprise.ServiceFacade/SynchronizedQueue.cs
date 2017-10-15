using System;
using System.Collections.Generic;
using System.Threading;

namespace Nexon.Enterprise.ServiceFacade
{
	public class SynchronizedQueue<TItem> where TItem : class
	{
		public SynchronizedQueue(int maxLength)
		{
			this.lockObject = new object();
			this.queue = new Queue<TItem>();
			this.itemAvailableEvent = new ManualResetEvent(false);
			this.spaceAvailableEvent = new ManualResetEvent(true);
			this.maxLength = maxLength;
		}

		public virtual bool TryDequeue(TimeoutHelper timeout, out TItem item)
		{
			item = default(TItem);
			if (this.ItemAvailableEvent.WaitOne(TimeoutHelper.ToMilliseconds(timeout.RemainingTime()), false))
			{
				item = this.Dequeue();
				return item != null;
			}
			return false;
		}

		public virtual TItem Dequeue()
		{
			TItem result;
			lock (this.lockObject)
			{
				result = this.queue.Dequeue();
				if (this.Count == 0)
				{
					this.itemAvailableEvent.Reset();
				}
				this.spaceAvailableEvent.Set();
			}
			return result;
		}

		public virtual bool TryEnqueue(TimeoutHelper timeout, TItem item)
		{
			if (this.SpaceAvailableEvent.WaitOne(TimeoutHelper.ToMilliseconds(timeout.RemainingTime()), false))
			{
				this.Enqueue(item);
				return true;
			}
			return false;
		}

		public virtual void Enqueue(TItem item)
		{
			lock (this.lockObject)
			{
				this.queue.Enqueue(item);
				this.itemAvailableEvent.Set();
				if (this.MaxLengthReached)
				{
					this.SpaceAvailableEvent.Reset();
				}
			}
		}

		public virtual int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		public virtual int Count
		{
			get
			{
				return this.queue.Count;
			}
		}

		public virtual ManualResetEvent ItemAvailableEvent
		{
			get
			{
				return this.itemAvailableEvent;
			}
		}

		public virtual ManualResetEvent SpaceAvailableEvent
		{
			get
			{
				return this.spaceAvailableEvent;
			}
		}

		public virtual bool MaxLengthReached
		{
			get
			{
				return this.Count >= this.maxLength;
			}
		}

		private Queue<TItem> queue;

		private object lockObject;

		private ManualResetEvent itemAvailableEvent;

		private ManualResetEvent spaceAvailableEvent;

		private int maxLength;
	}
}
