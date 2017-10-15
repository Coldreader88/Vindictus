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
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentQueue<T> : IProducerConsumerCollection<T>, IEnumerable<T>, ICollection, IEnumerable
	{
		public ConcurrentQueue()
		{
			this.m_head = (this.m_tail = new ConcurrentQueue<T>.Segment(0L));
		}

		public ConcurrentQueue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		public void Enqueue(T item)
		{
			SpinWait spinWait = default(SpinWait);
			while (!this.m_tail.TryAppend(item, ref this.m_tail))
			{
				spinWait.SpinOnce();
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this.ToList().GetEnumerator();
		}

		private void GetHeadTailPositions(out ConcurrentQueue<T>.Segment head, out ConcurrentQueue<T>.Segment tail, out int headLow, out int tailHigh)
		{
			head = this.m_head;
			tail = this.m_tail;
			headLow = head.Low;
			tailHigh = tail.High;
			SpinWait spinWait = default(SpinWait);
			while (head != this.m_head || tail != this.m_tail || headLow != head.Low || tailHigh != tail.High || head.m_index > tail.m_index)
			{
				spinWait.SpinOnce();
				head = this.m_head;
				tail = this.m_tail;
				headLow = head.Low;
				tailHigh = tail.High;
			}
		}

		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			this.m_head = (this.m_tail = new ConcurrentQueue<T>.Segment(0L));
			int num = 0;
			foreach (T value in collection)
			{
				this.m_tail.UnsafeAdd(value);
				num++;
				if (num >= 32)
				{
					this.m_tail = this.m_tail.UnsafeGrow();
					num = 0;
				}
			}
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.InitializeFromCollection(this.m_serializationArray);
			this.m_serializationArray = null;
		}

		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
		}

		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Enqueue(item);
			return true;
		}

		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return this.TryDequeue(out item);
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
			ConcurrentQueue<T>.Segment segment;
			ConcurrentQueue<T>.Segment segment2;
			int start;
			int end;
			this.GetHeadTailPositions(out segment, out segment2, out start, out end);
			if (segment == segment2)
			{
				return segment.ToList(start, end);
			}
			List<T> list = new List<T>(segment.ToList(start, 31));
			for (ConcurrentQueue<T>.Segment next = segment.Next; next != segment2; next = next.Next)
			{
				list.AddRange(next.ToList(0, 31));
			}
			list.AddRange(segment2.ToList(0, end));
			return list;
		}

		public bool TryDequeue(out T result)
		{
			while (!this.IsEmpty)
			{
				if (this.m_head.TryRemove(out result, ref this.m_head))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		public bool TryPeek(out T result)
		{
			while (!this.IsEmpty)
			{
				if (this.m_head.TryPeek(out result))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		public int Count
		{
			get
			{
				ConcurrentQueue<T>.Segment segment;
				ConcurrentQueue<T>.Segment segment2;
				int num;
				int num2;
				this.GetHeadTailPositions(out segment, out segment2, out num, out num2);
				if (segment == segment2)
				{
					return num2 - num + 1;
				}
				int num3 = 32 - num;
				num3 += 32 * (int)(segment2.m_index - segment.m_index - 1L);
				return num3 + (num2 + 1);
			}
		}

		public bool IsEmpty
		{
			get
			{
				ConcurrentQueue<T>.Segment head = this.m_head;
				if (head.IsEmpty)
				{
					if (head.Next == null)
					{
						return true;
					}
					SpinWait spinWait = default(SpinWait);
					while (head.IsEmpty)
					{
						if (head.Next == null)
						{
							return true;
						}
						spinWait.SpinOnce();
						head = this.m_head;
					}
				}
				return false;
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

		private const int SEGMENT_SIZE = 32;

		[NonSerialized]
		private volatile ConcurrentQueue<T>.Segment m_head;

		private T[] m_serializationArray;

		[NonSerialized]
		private volatile ConcurrentQueue<T>.Segment m_tail;

		private class Segment
		{
			internal Segment(long index)
			{
				this.m_array = new T[32];
				this.m_state = new int[32];
				this.m_high = -1;
				this.m_index = index;
			}

			internal void Grow(ref ConcurrentQueue<T>.Segment tail)
			{
				ConcurrentQueue<T>.Segment next = new ConcurrentQueue<T>.Segment(this.m_index + 1L);
				this.m_next = next;
				tail = this.m_next;
			}

			internal List<T> ToList(int start, int end)
			{
				List<T> list = new List<T>();
				for (int i = start; i <= end; i++)
				{
					SpinWait spinWait = default(SpinWait);
					while (this.m_state[i] == 0)
					{
						spinWait.SpinOnce();
					}
					list.Add(this.m_array[i]);
				}
				return list;
			}

			internal bool TryAppend(T value, ref ConcurrentQueue<T>.Segment tail)
			{
				if (this.m_high >= 31)
				{
					return false;
				}
				int num = 32;
				try
				{
				}
				finally
				{
					num = Interlocked.Increment(ref this.m_high);
					if (num <= 31)
					{
						this.m_array[num] = value;
						this.m_state[num] = 1;
					}
					if (num == 31)
					{
						this.Grow(ref tail);
					}
				}
				return num <= 31;
			}

			internal bool TryPeek(out T result)
			{
				result = default(T);
				int low = this.Low;
				if (low > this.High)
				{
					return false;
				}
				SpinWait spinWait = default(SpinWait);
				while (this.m_state[low] == 0)
				{
					spinWait.SpinOnce();
				}
				result = this.m_array[low];
				return true;
			}

			internal bool TryRemove(out T result, ref ConcurrentQueue<T>.Segment head)
			{
				SpinWait spinWait = default(SpinWait);
				int i = this.Low;
				int high = this.High;
				while (i <= high)
				{
					if (Interlocked.CompareExchange(ref this.m_low, i + 1, i) == i)
					{
						SpinWait spinWait2 = default(SpinWait);
						while (this.m_state[i] == 0)
						{
							spinWait2.SpinOnce();
						}
						result = this.m_array[i];
						this.m_array[i] = default(T);
						if (i + 1 >= 32)
						{
							spinWait2 = default(SpinWait);
							while (this.m_next == null)
							{
								spinWait2.SpinOnce();
							}
							head = this.m_next;
						}
						return true;
					}
					spinWait.SpinOnce();
					i = this.Low;
					high = this.High;
				}
				result = default(T);
				return false;
			}

			internal void UnsafeAdd(T value)
			{
				this.m_high++;
				this.m_array[this.m_high] = value;
				this.m_state[this.m_high] = 1;
			}

			internal ConcurrentQueue<T>.Segment UnsafeGrow()
			{
				ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(this.m_index + 1L);
				this.m_next = segment;
				return segment;
			}

			internal int High
			{
				get
				{
					return System.Math.Min(this.m_high, 31);
				}
			}

			internal bool IsEmpty
			{
				get
				{
					return this.Low > this.High;
				}
			}

			internal int Low
			{
				get
				{
					return System.Math.Min(this.m_low, 32);
				}
			}

			internal ConcurrentQueue<T>.Segment Next
			{
				get
				{
					return this.m_next;
				}
			}

			internal volatile T[] m_array;

			private volatile int m_high;

			internal readonly long m_index;

			private volatile int m_low;

			private volatile ConcurrentQueue<T>.Segment m_next;

			private volatile int[] m_state;
		}
	}
}
