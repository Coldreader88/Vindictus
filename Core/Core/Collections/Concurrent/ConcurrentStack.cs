using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace Devcat.Core.Collections.Concurrent
{
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentStack<T> : IProducerConsumerCollection<T>, IEnumerable<T>, ICollection, IEnumerable
	{
		public ConcurrentStack()
		{
		}

		public ConcurrentStack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		public void Clear()
		{
			this.m_head = null;
		}

		private void CopyRemovedItems(ConcurrentStack<T>.Node head, T[] collection, int startIndex, int nodesCount)
		{
			ConcurrentStack<T>.Node node = head;
			for (int i = startIndex; i < startIndex + nodesCount; i++)
			{
				collection[i] = node.m_value;
				node = node.m_next;
			}
		}

		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this.GetEnumerator(this.m_head);
		}

		private IEnumerator<T> GetEnumerator(ConcurrentStack<T>.Node head)
		{
			for (ConcurrentStack<T>.Node next = head; next != null; next = next.m_next)
			{
				yield return next.m_value;
			}
			yield break;
		}

		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			ConcurrentStack<T>.Node node = null;
			foreach (T value in collection)
			{
				ConcurrentStack<T>.Node node2 = new ConcurrentStack<T>.Node(value)
				{
					m_next = node
				};
				node = node2;
			}
			this.m_head = node;
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			ConcurrentStack<T>.Node node = null;
			ConcurrentStack<T>.Node head = null;
			for (int i = 0; i < this.m_serializationArray.Length; i++)
			{
				ConcurrentStack<T>.Node node2 = new ConcurrentStack<T>.Node(this.m_serializationArray[i]);
				if (node == null)
				{
					head = node2;
				}
				else
				{
					node.m_next = node2;
				}
				node = node2;
			}
			this.m_head = head;
			this.m_serializationArray = null;
		}

		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
		}

		public void Push(T item)
		{
			ConcurrentStack<T>.Node node = new ConcurrentStack<T>.Node(item)
			{
				m_next = this.m_head
			};
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node, node.m_next) != node.m_next)
			{
				this.PushCore(node, node);
			}
		}

		private void PushCore(ConcurrentStack<T>.Node head, ConcurrentStack<T>.Node tail)
		{
			SpinWait spinWait = default(SpinWait);
			do
			{
				spinWait.SpinOnce();
				tail.m_next = this.m_head;
			}
			while (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head, tail.m_next) != tail.m_next);
		}

		public void PushRange(T[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.PushRange(items, 0, items.Length);
		}

		public void PushRange(T[] items, int startIndex, int count)
		{
			this.ValidatePushPopRangeInput(items, startIndex, count);
			if (count != 0)
			{
				ConcurrentStack<T>.Node node2;
				ConcurrentStack<T>.Node node = node2 = new ConcurrentStack<T>.Node(items[startIndex]);
				for (int i = startIndex + 1; i < startIndex + count; i++)
				{
					ConcurrentStack<T>.Node node3 = new ConcurrentStack<T>.Node(items[i])
					{
						m_next = node2
					};
					node2 = node3;
				}
				node.m_next = this.m_head;
				if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node2, node.m_next) != node.m_next)
				{
					this.PushCore(node2, node);
				}
			}
		}

		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Push(item);
			return true;
		}

		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return this.TryPop(out item);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			T[] array2 = this.ToArray();
			array2.CopyTo(array, index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public T[] ToArray()
		{
			return this.ToList().ToArray();
		}

		private List<T> ToList()
		{
			List<T> list = new List<T>();
			for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
			{
				list.Add(node.m_value);
			}
			return list;
		}

		public bool TryPeek(out T result)
		{
			ConcurrentStack<T>.Node head = this.m_head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			result = head.m_value;
			return true;
		}

		public bool TryPop(out T result)
		{
			ConcurrentStack<T>.Node head = this.m_head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head.m_next, head) == head)
			{
				result = head.m_value;
				return true;
			}
			return this.TryPopCore(out result);
		}

		private bool TryPopCore(out T result)
		{
			ConcurrentStack<T>.Node node;
			if (this.TryPopCore(1, out node) == 1)
			{
				result = node.m_value;
				return true;
			}
			result = default(T);
			return false;
		}

        private int TryPopCore(int count, out ConcurrentStack<T>.Node poppedHead)
        {
            ConcurrentStack<T>.Node mHead;
            int i;
            SpinWait spinWait = new SpinWait();
            int num = 1;
            Random random = new Random(Environment.TickCount & 2147483647);
            while (true)
            {
                mHead = this.m_head;
                if (mHead == null)
                {
                    poppedHead = null;
                    return 0;
                }
                ConcurrentStack<T>.Node mNext = mHead;
                for (i = 1; i < count && mNext.m_next != null; i++)
                {
                    mNext = mNext.m_next;
                }
                if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, mNext.m_next, mHead) == mHead)
                {
                    break;
                }
                for (int j = 0; j < num; j++)
                {
                    spinWait.SpinOnce();
                }
                num = (spinWait.NextSpinWillYield ? random.Next(1, 8) : num * 2);
            }
            poppedHead = mHead;
            return i;
        }

        public int TryPopRange(T[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			return this.TryPopRange(items, 0, items.Length);
		}

		public int TryPopRange(T[] items, int startIndex, int count)
		{
			this.ValidatePushPopRangeInput(items, startIndex, count);
			if (count == 0)
			{
				return 0;
			}
			ConcurrentStack<T>.Node head;
			int num = this.TryPopCore(count, out head);
			if (num > 0)
			{
				this.CopyRemovedItems(head, items, startIndex, num);
			}
			return num;
		}

		private void ValidatePushPopRangeInput(T[] items, int startIndex, int count)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment2.GetResourceString("ConcurrentStack_PushPopRange_CountOutOfRange"));
			}
			int num = items.Length;
			if (startIndex >= num || startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment2.GetResourceString("ConcurrentStack_PushPopRange_StartOutOfRange"));
			}
			if (num - count < startIndex)
			{
				throw new ArgumentException(Environment2.GetResourceString("ConcurrentStack_PushPopRange_InvalidCount"));
			}
		}

		public int Count
		{
			get
			{
				int num = 0;
				for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
				{
					num++;
				}
				return num;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.m_head == null;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException(Environment2.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
			}
		}

		private const int BACKOFF_MAX_YIELDS = 8;

		[NonSerialized]
		private volatile ConcurrentStack<T>.Node m_head;

		private T[] m_serializationArray;

		private class Node
		{
			internal Node(T value)
			{
				this.m_value = value;
				this.m_next = null;
			}

			internal ConcurrentStack<T>.Node m_next;

			internal T m_value;
		}
	}
}
