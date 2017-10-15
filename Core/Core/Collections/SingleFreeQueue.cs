using System;
using System.Collections.Generic;

namespace Devcat.Core.Collections
{
	public class SingleFreeQueue<T>
	{
		public SingleFreeQueue()
		{
			this.firstList = new SingleFreeQueue<T>.ElementList(16);
			this.lastList = this.firstList;
		}

		public bool Empty
		{
			get
			{
				return this.firstList.Head == this.firstList.EndInfo.Tail && this.firstList == this.lastList;
			}
		}

		public T Head
		{
			get
			{
				SingleFreeQueue<T>.EndInfo endInfo = this.firstList.EndInfo;
				if (this.firstList.Head == endInfo.Tail)
				{
					if (endInfo.NextList == null)
					{
						throw new InvalidOperationException("Queue is Empty.");
					}
					this.firstList = endInfo.NextList;
				}
				return this.firstList.ValueList[this.GetIndex(this.firstList.Head, this.firstList.ValueList.Length)];
			}
		}

		private int GetIndex(int position, int length)
		{
			return position % length;
		}

		public void Enqueue(T value)
		{
			if (this.lastList.Head + this.lastList.ValueList.Length != this.lastList.EndInfo.Tail)
			{
				this.lastList.ValueList[this.GetIndex(this.lastList.EndInfo.Tail, this.lastList.ValueList.Length)] = value;
				this.lastList.EndInfo.Tail++;
				return;
			}
			int num = this.lastList.ValueList.Length << 1;
			if (num == -2147483648)
			{
				throw new OutOfMemoryException("Can't reserve more entry.");
			}
			SingleFreeQueue<T>.ElementList elementList = new SingleFreeQueue<T>.ElementList(num);
			elementList.ValueList[0] = value;
			elementList.EndInfo.Tail = 1;
			this.lastList.EndInfo.NextList = elementList;
			this.lastList = elementList;
		}

		public void Enqueue(IEnumerable<T> collection)
		{
			int num = this.lastList.Head;
			int num2 = this.lastList.EndInfo.Tail;
			T[] valueList = this.lastList.ValueList;
			SingleFreeQueue<T>.EndInfo endInfo = null;
			SingleFreeQueue<T>.ElementList elementList = this.lastList;
			foreach (T t in collection)
			{
				if (num + valueList.Length == num2)
				{
					int num3 = valueList.Length << 1;
					if (num3 == -2147483648)
					{
						throw new OutOfMemoryException("Can't reserve more entry.");
					}
					SingleFreeQueue<T>.ElementList elementList2 = new SingleFreeQueue<T>.ElementList(num3);
					elementList2.ValueList[0] = t;
					elementList2.EndInfo.Tail = 1;
					if (endInfo == null)
					{
						endInfo = new SingleFreeQueue<T>.EndInfo();
						endInfo.Tail = num2;
						endInfo.NextList = elementList2;
						elementList = elementList2;
					}
					else
					{
						elementList.EndInfo.Tail = num2;
						elementList.EndInfo.NextList = elementList2;
						elementList = elementList2;
					}
					num = 0;
					num2 = 1;
					valueList = elementList2.ValueList;
				}
				else
				{
					valueList[this.GetIndex(num2, valueList.Length)] = t;
					num2++;
				}
			}
			if (endInfo == null)
			{
				endInfo = new SingleFreeQueue<T>.EndInfo();
				endInfo.Tail = num2;
			}
			else
			{
				elementList.EndInfo.Tail = num2;
			}
			this.lastList.EndInfo = endInfo;
			this.lastList = elementList;
		}

		public T Dequeue()
		{
			SingleFreeQueue<T>.EndInfo endInfo = this.firstList.EndInfo;
			if (this.firstList.Head == endInfo.Tail)
			{
				if (endInfo.NextList == null)
				{
					throw new InvalidOperationException("Queue is Empty.");
				}
				this.firstList = endInfo.NextList;
			}
			int index = this.GetIndex(this.firstList.Head, this.firstList.ValueList.Length);
			T result = this.firstList.ValueList[index];
			this.firstList.ValueList[index] = default(T);
			this.firstList.Head++;
			return result;
		}

		public bool TryDequeue(out T value)
		{
			value = default(T);
			SingleFreeQueue<T>.EndInfo endInfo = this.firstList.EndInfo;
			if (this.firstList.Head == endInfo.Tail)
			{
				if (endInfo.NextList == null)
				{
					return false;
				}
				this.firstList = endInfo.NextList;
			}
			int index = this.GetIndex(this.firstList.Head, this.firstList.ValueList.Length);
			value = this.firstList.ValueList[index];
			this.firstList.ValueList[index] = default(T);
			this.firstList.Head++;
			return true;
		}

		public void Clear()
		{
			SingleFreeQueue<T>.EndInfo endInfo = this.firstList.EndInfo;
			while (endInfo.NextList != null)
			{
				this.firstList = endInfo.NextList;
				endInfo = this.firstList.EndInfo;
			}
			int head = this.firstList.Head;
			int index = this.GetIndex(head, this.firstList.ValueList.Length);
			int tail = endInfo.Tail;
			int index2 = this.GetIndex(tail, this.firstList.ValueList.Length);
			if (index < index2)
			{
				Array.Clear(this.firstList.ValueList, index, tail - head);
			}
			else if (tail - head > 0)
			{
				Array.Clear(this.firstList.ValueList, 0, index2);
				Array.Clear(this.firstList.ValueList, index, this.firstList.ValueList.Length - index);
			}
			this.firstList.Head = tail;
		}

		private SingleFreeQueue<T>.ElementList firstList;

		private SingleFreeQueue<T>.ElementList lastList;

		private class ElementList
		{
			public ElementList(int capacity)
			{
				this.ValueList = new T[capacity];
				this.Head = 0;
				this.EndInfo = new SingleFreeQueue<T>.EndInfo();
			}

			public T[] ValueList;

			public int Head;

			public SingleFreeQueue<T>.EndInfo EndInfo;
		}

		private class EndInfo
		{
			public int Tail;

			public SingleFreeQueue<T>.ElementList NextList;
		}
	}
}
