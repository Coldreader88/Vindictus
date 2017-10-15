using System;
using System.Collections.Generic;
using Devcat.Core.Memory;

namespace Devcat.Core.Collections
{
	public class SingleFreeQueue3<T>
	{
		public bool Empty
		{
			get
			{
				return !this.head.Next.Valid;
			}
		}

		public T Head
		{
			get
			{
				if (this.Empty)
				{
					throw new InvalidOperationException("Queue is Empty.");
				}
				return this.head.Value;
			}
		}

		public SingleFreeQueue3()
		{
			ChunkSegment<SingleFreeQueue3<T>.Element>.AllocationSize = 256;
			ArraySegment<SingleFreeQueue3<T>.Element> arraySegment = ChunkSegment<SingleFreeQueue3<T>.Element>.Get(1);
			arraySegment.Array[arraySegment.Offset].Next.Invalidate();
			this.head = default(SingleFreeQueue3<T>.Indexer);
			this.head.SetIndex(arraySegment.Array, arraySegment.Offset);
			this.tail = this.head;
		}

		public void Enqueue(T value)
		{
			ArraySegment<SingleFreeQueue3<T>.Element> arraySegment = ChunkSegment<SingleFreeQueue3<T>.Element>.Get(1);
			arraySegment.Array[arraySegment.Offset].Initialize(value);
			this.tail.Attach(arraySegment.Array, arraySegment.Offset);
			this.tail.SetIndex(arraySegment.Array, arraySegment.Offset);
		}

		public void Enqueue(IEnumerable<T> values)
		{
			ChunkSegment<SingleFreeQueue3<T>.Element>.ChunkSegmentContext context = ChunkSegment<SingleFreeQueue3<T>.Element>.Context;
			SingleFreeQueue3<T>.Indexer next = default(SingleFreeQueue3<T>.Indexer);
			SingleFreeQueue3<T>.Indexer index = default(SingleFreeQueue3<T>.Indexer);
			next.Invalidate();
			foreach (T value in values)
			{
				ArraySegment<SingleFreeQueue3<T>.Element> arraySegment = context.Get(1);
				arraySegment.Array[arraySegment.Offset].Initialize(value);
				if (!next.Valid)
				{
					next.SetIndex(arraySegment.Array, arraySegment.Offset);
				}
				else
				{
					index.Attach(arraySegment.Array, arraySegment.Offset);
				}
				index.SetIndex(arraySegment.Array, arraySegment.Offset);
			}
			if (next.Valid)
			{
				this.tail.Attach(next);
				this.tail.SetIndex(index);
			}
		}

		public void Enqueue<U>(IEnumerable<U> values, Converter<U, T> converter)
		{
			ChunkSegment<SingleFreeQueue3<T>.Element>.ChunkSegmentContext context = ChunkSegment<SingleFreeQueue3<T>.Element>.Context;
			SingleFreeQueue3<T>.Indexer next = default(SingleFreeQueue3<T>.Indexer);
			SingleFreeQueue3<T>.Indexer index = default(SingleFreeQueue3<T>.Indexer);
			next.Invalidate();
			foreach (U input in values)
			{
				ArraySegment<SingleFreeQueue3<T>.Element> arraySegment = context.Get(1);
				arraySegment.Array[arraySegment.Offset].Initialize(converter(input));
				if (!next.Valid)
				{
					next.SetIndex(arraySegment.Array, arraySegment.Offset);
				}
				else
				{
					index.Attach(arraySegment.Array, arraySegment.Offset);
				}
				index.SetIndex(arraySegment.Array, arraySegment.Offset);
			}
			if (next.Valid)
			{
				this.tail.Attach(next);
				this.tail.SetIndex(index);
			}
		}

		public void Enqueue<U>(ArraySegment<U> values, Converter<U, T> converter)
		{
			ChunkSegment<SingleFreeQueue3<T>.Element>.ChunkSegmentContext context = ChunkSegment<SingleFreeQueue3<T>.Element>.Context;
			SingleFreeQueue3<T>.Indexer next = default(SingleFreeQueue3<T>.Indexer);
			SingleFreeQueue3<T>.Indexer index = default(SingleFreeQueue3<T>.Indexer);
			next.Invalidate();
			for (int i = 0; i < values.Count; i++)
			{
				ArraySegment<SingleFreeQueue3<T>.Element> arraySegment = context.Get(1);
				arraySegment.Array[arraySegment.Offset].Initialize(converter(values.Array[values.Offset + i]));
				if (!next.Valid)
				{
					next.SetIndex(arraySegment.Array, arraySegment.Offset);
				}
				else
				{
					index.Attach(arraySegment.Array, arraySegment.Offset);
				}
				index.SetIndex(arraySegment.Array, arraySegment.Offset);
			}
			if (next.Valid)
			{
				this.tail.Attach(next);
				this.tail.SetIndex(index);
			}
		}

		public T Dequeue()
		{
			if (this.Empty)
			{
				throw new InvalidOperationException("Queue is Empty.");
			}
			SingleFreeQueue3<T>.Indexer next = this.head.Next;
			this.head.Detach();
			this.head = next;
			T value = this.head.Value;
			this.head.Value = default(T);
			return value;
		}

		public bool TryPeek(out T value)
		{
			if (this.Empty)
			{
				value = default(T);
				return false;
			}
			value = this.head.Value;
			return true;
		}

		public bool TryDequeue(out T value)
		{
			if (this.Empty)
			{
				value = default(T);
				return false;
			}
			value = this.Dequeue();
			return true;
		}

		public void Clear()
		{
			while (this.head.Next.Valid)
			{
				SingleFreeQueue3<T>.Indexer next = this.head.Next;
				this.head.Detach();
				this.head = next;
				this.head.Value = default(T);
			}
		}

		private SingleFreeQueue3<T>.Indexer head;

		private SingleFreeQueue3<T>.Indexer tail;

		private struct Element
		{
			public void Initialize(T value)
			{
				this.Value = value;
				this.Next.Invalidate();
			}

			public T Value;

			public SingleFreeQueue3<T>.Indexer Next;
		}

		private struct Indexer
		{
			public bool Valid
			{
				get
				{
					return this.Offset != -1;
				}
			}

			public SingleFreeQueue3<T>.Indexer Next
			{
				get
				{
					return this.Array[this.Offset].Next;
				}
			}

			public T Value
			{
				get
				{
					return this.Array[this.Offset].Value;
				}
				set
				{
					this.Array[this.Offset].Value = value;
				}
			}

			public void Invalidate()
			{
				this.Array = null;
				this.Offset = -1;
			}

			public void SetIndex(SingleFreeQueue3<T>.Element[] array, int offset)
			{
				this.Array = array;
				this.Offset = offset;
			}

			public void SetIndex(SingleFreeQueue3<T>.Indexer next)
			{
				this.SetIndex(next.Array, next.Offset);
			}

			public void Attach(SingleFreeQueue3<T>.Element[] array, int offset)
			{
				this.Array[this.Offset].Next.SetIndex(array, offset);
			}

			public void Attach(SingleFreeQueue3<T>.Indexer next)
			{
				this.Attach(next.Array, next.Offset);
			}

			public void Detach()
			{
				this.Array[this.Offset].Next.Invalidate();
			}

			private SingleFreeQueue3<T>.Element[] Array;

			private int Offset;
		}
	}
}
