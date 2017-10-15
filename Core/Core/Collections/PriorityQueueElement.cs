using System;

namespace Devcat.Core.Collections
{
	public sealed class PriorityQueueElement<T>
	{
		public T Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				if (this.Valid)
				{
					throw new InvalidOperationException("Can't change Priority after assigned to PriorityQueue. Use PriorityQueue.Change() instead.");
				}
				this.priority = value;
			}
		}

		public bool Valid
		{
			get
			{
				return this.index != -1;
			}
		}

		internal int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		public PriorityQueueElement()
		{
			this.Invalidate();
		}

		internal PriorityQueueElement(T value, int priority, int index)
		{
			this.Value = value;
			this.Priority = priority;
			this.Index = index;
		}

		internal void Invalidate()
		{
			this.Value = default(T);
			this.Index = -1;
		}

		private T value;

		private int priority;

		private int index;
	}
}
