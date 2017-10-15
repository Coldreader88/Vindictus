using System;
using System.Collections.Generic;
using System.Threading;
using Devcat.Core.WinNative;

namespace Devcat.Core.Collections
{
	public class WriteFreeQueue<T>
	{
		public WriteFreeQueue()
		{
			this.head = new WriteFreeQueue<T>.QueueElement();
			this.tail = this.head;
		}

		public bool Empty
		{
			get
			{
				return this.head.Next == null;
			}
		}

		public T Head
		{
			get
			{
				if (this.head.Next == null)
				{
					throw new InvalidOperationException("Queue is Empty.");
				}
				return this.head.Next.Value;
			}
		}

		public IEnumerable<T> Values
		{
			get
			{
				for (WriteFreeQueue<T>.QueueElement element = this.head.Next; element != null; element = element.Next)
				{
					yield return element.Value;
				}
				yield break;
			}
		}

		public void Enqueue(T value)
		{
			WriteFreeQueue<T>.QueueElement queueElement = new WriteFreeQueue<T>.QueueElement();
			queueElement.Value = value;
			while (Interlocked.CompareExchange<WriteFreeQueue<T>.QueueElement>(ref this.tail.Next, queueElement, null) != null)
			{
				Devcat.Core.WinNative.Thread.SwitchToThread();
			}
			this.tail = queueElement;
		}

		public void Enqueue(IEnumerable<T> collection)
		{
			WriteFreeQueue<T>.QueueElement queueElement = null;
			WriteFreeQueue<T>.QueueElement queueElement2 = null;
			foreach (T value in collection)
			{
				WriteFreeQueue<T>.QueueElement queueElement3 = new WriteFreeQueue<T>.QueueElement();
				queueElement3.Value = value;
				if (queueElement2 == null)
				{
					queueElement = queueElement3;
				}
				else
				{
					queueElement2.Next = queueElement3;
				}
				queueElement2 = queueElement3;
			}
			if (queueElement == null)
			{
				return;
			}
			while (Interlocked.CompareExchange<WriteFreeQueue<T>.QueueElement>(ref this.tail.Next, queueElement, null) != null)
			{
				Devcat.Core.WinNative.Thread.SwitchToThread();
			}
			this.tail = queueElement2;
		}

		public T Peek()
		{
			if (this.head.Next == null)
			{
				throw new InvalidOperationException("Queue is Empty.");
			}
			return this.head.Next.Value;
		}

		public T Dequeue()
		{
			if (this.head.Next == null)
			{
				throw new InvalidOperationException("Queue is Empty.");
			}
			this.head = this.head.Next;
			T value = this.head.Value;
			this.head.Value = default(T);
			return value;
		}

		public bool TryDequeue(out T value)
		{
			bool result;
			if (this.head.Next == null)
			{
				value = default(T);
				result = false;
			}
			else
			{
				this.head = this.head.Next;
				value = this.head.Value;
				this.head.Value = default(T);
				result = true;
			}
			return result;
		}

		public void Clear()
		{
			WriteFreeQueue<T>.QueueElement queueElement = this.tail;
			while (this.head != queueElement)
			{
				WriteFreeQueue<T>.QueueElement next = this.head.Next;
				this.head.Next = null;
				this.head = next;
				this.head.Value = default(T);
			}
		}

		private WriteFreeQueue<T>.QueueElement head;

		private WriteFreeQueue<T>.QueueElement tail;

		private class QueueElement
		{
			public T Value;

			public WriteFreeQueue<T>.QueueElement Next;
		}
	}
}
