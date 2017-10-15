using System;
using System.Collections.Generic;
using System.Threading;

namespace Devcat.Core.Collections
{
	public class WriteFreeQueue3<T>
	{
		private static KeyValuePair<int, T> AttachPriority(T value)
		{
			return new KeyValuePair<int, T>(WriteFreeQueue3<T>.currentPriority, value);
		}

		public WriteFreeQueue3()
		{
			this.threadLocalQueue = new SingleFreeQueue3<KeyValuePair<int, T>>[32];
			this.queueIndices = new int[32];
			this.queueLength = 0;
			this.nextIndex = -1;
		}

		public bool Empty
		{
			get
			{
				for (int i = 0; i < this.queueLength; i++)
				{
					if (!this.threadLocalQueue[this.queueIndices[i]].Empty)
					{
						return false;
					}
				}
				return true;
			}
		}

		public T Head
		{
			get
			{
				this.SetPriorSingleFreeQueueIndex();
				if (this.nextIndex == -1)
				{
					throw new InvalidOperationException("Queue is Empty.");
				}
				return this.threadLocalQueue[this.nextIndex].Head.Value;
			}
		}

		private void ExtendThreadQueue(int threadID)
		{
			int i;
			for (i = this.threadLocalQueue.Length * 2; i <= threadID; i *= 2)
			{
			}
			SingleFreeQueue3<KeyValuePair<int, T>>[] destinationArray = new SingleFreeQueue3<KeyValuePair<int, T>>[i];
			Array.Copy(this.threadLocalQueue, destinationArray, this.threadLocalQueue.Length);
			this.threadLocalQueue = destinationArray;
		}

		private void AllocateThreadQueue(int threadID)
		{
			this.threadLocalQueue[threadID] = new SingleFreeQueue3<KeyValuePair<int, T>>();
			if (this.queueLength == this.queueIndices.Length)
			{
				int[] dst = new int[this.queueIndices.Length * 2];
				Buffer.BlockCopy(this.queueIndices, 0, dst, 0, 4 * this.queueIndices.Length);
				this.queueIndices = dst;
			}
			this.queueIndices[this.queueLength] = threadID;
			this.queueLength++;
		}

        private SingleFreeQueue3<KeyValuePair<int, T>> GetSingleFreeQueue()
        {
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            if ((int)this.threadLocalQueue.Length <= managedThreadId)
            {
                lock (this)
                {
                    this.ExtendThreadQueue(managedThreadId);
                    this.AllocateThreadQueue(managedThreadId);
                }
            }
            else if (this.threadLocalQueue[managedThreadId] == null)
            {
                lock (this)
                {
                    this.AllocateThreadQueue(managedThreadId);
                }
            }
            return this.threadLocalQueue[managedThreadId];
        }

        public void Enqueue(T value)
		{
			this.priority++;
			KeyValuePair<int, T> value2 = new KeyValuePair<int, T>(this.priority, value);
			this.GetSingleFreeQueue().Enqueue(value2);
		}

		public void Enqueue(IEnumerable<T> collection)
		{
			WriteFreeQueue3<T>.currentPriority = ++this.priority;
			this.GetSingleFreeQueue().Enqueue<T>(collection, WriteFreeQueue3<T>.priorityAttacher);
		}

		public void Enqueue(ArraySegment<T> collection)
		{
			WriteFreeQueue3<T>.currentPriority = ++this.priority;
			this.GetSingleFreeQueue().Enqueue<T>(collection, WriteFreeQueue3<T>.priorityAttacher);
		}

		private void SetPriorSingleFreeQueueIndex()
		{
			KeyValuePair<int, T> keyValuePair;
			if (this.nextIndex != -1 && this.threadLocalQueue[this.nextIndex].TryPeek(out keyValuePair) && keyValuePair.Key == this.nextPriority)
			{
				return;
			}
			this.nextIndex = -1;
			for (int i = 0; i < this.queueLength; i++)
			{
				if (this.threadLocalQueue[this.queueIndices[i]].TryPeek(out keyValuePair))
				{
					int key = keyValuePair.Key;
					if (this.nextIndex == -1 || this.nextPriority - key > 0)
					{
						this.nextIndex = this.queueIndices[i];
						this.nextPriority = key;
					}
				}
			}
		}

		public T Dequeue()
		{
			this.SetPriorSingleFreeQueueIndex();
			if (this.nextIndex == -1)
			{
				throw new InvalidOperationException("Queue is Empty.");
			}
			return this.threadLocalQueue[this.nextIndex].Dequeue().Value;
		}

		public bool TryDequeue(out T value)
		{
			this.SetPriorSingleFreeQueueIndex();
			if (this.nextIndex == -1)
			{
				value = default(T);
				return false;
			}
			KeyValuePair<int, T> keyValuePair;
			bool result = this.threadLocalQueue[this.nextIndex].TryDequeue(out keyValuePair);
			value = keyValuePair.Value;
			return result;
		}

		public void Clear()
		{
			for (int i = 0; i < this.queueLength; i++)
			{
				this.threadLocalQueue[this.queueIndices[i]].Clear();
			}
		}

		[ThreadStatic]
		private static int currentPriority;

		private static Converter<T, KeyValuePair<int, T>> priorityAttacher = new Converter<T, KeyValuePair<int, T>>(WriteFreeQueue3<T>.AttachPriority);

		private SingleFreeQueue3<KeyValuePair<int, T>>[] threadLocalQueue;

		private int[] queueIndices;

		private int queueLength;

		private int priority;

		private int nextIndex;

		private int nextPriority;
	}
}
