using System;

namespace Devcat.Core.Collections
{
	public class SingleFreeQueue4<T>
	{
		public SingleFreeQueue4()
		{
			this.head = new SingleFreeQueue4<T>.Element();
			this.tail = this.head;
		}

		public bool Empty
		{
			get
			{
				return this.head.Next == null;
			}
		}

		public void Enqueue(T value)
		{
			SingleFreeQueue4<T>.Element element = new SingleFreeQueue4<T>.Element();
			element.Value = value;
			this.tail.Next = element;
			this.tail = this.tail.Next;
		}

		public T Dequeue()
		{
			if (this.Empty)
			{
				throw new InvalidOperationException("Queue is Empty.");
			}
			this.head = this.head.Next;
			T value = this.head.Value;
			this.head.Value = default(T);
			return value;
		}

		public void Clear()
		{
		}

		private SingleFreeQueue4<T>.Element head;

		private SingleFreeQueue4<T>.Element tail;

		private class Element
		{
			public T Value;

			public SingleFreeQueue4<T>.Element Next;
		}
	}
}
