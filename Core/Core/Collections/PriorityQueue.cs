using System;
using System.Collections.Generic;

namespace Devcat.Core.Collections
{
	public class PriorityQueue<T>
	{
		public int Count
		{
			get
			{
				return this.elementList.Count;
			}
		}

		public PriorityQueue()
		{
			this.elementList = new List<PriorityQueueElement<T>>();
		}

		public void Clear()
		{
			this.elementList.Clear();
		}

		public PriorityQueueElement<T> Add(T value, int priority)
		{
			PriorityQueueElement<T> priorityQueueElement = new PriorityQueueElement<T>(value, priority, this.elementList.Count);
			this.Add(priorityQueueElement);
			return priorityQueueElement;
		}

		public void Add(PriorityQueueElement<T> priorityQueueElement)
		{
			priorityQueueElement.Index = this.elementList.Count;
			this.elementList.Add(priorityQueueElement);
			this.Promote(priorityQueueElement);
		}

		public void Change(PriorityQueueElement<T> element, int newPriority)
		{
			int index = element.Index;
			if (!object.ReferenceEquals(this.elementList[index], element))
			{
				throw new ArgumentException("Index of element had been corrupted or Invalid container.");
			}
			int priority = element.Priority;
			element.Priority = newPriority;
			if (this.ComparePriority(newPriority, priority))
			{
				this.Promote(element);
				return;
			}
			if (this.ComparePriority(priority, newPriority))
			{
				this.Demote(element);
			}
		}

		public T RemoveMin()
		{
			if (this.elementList.Count == 0)
			{
				throw new InvalidOperationException("Priority queue is Empty.");
			}
			T value = this.elementList[0].Value;
			this.RemoveAt(0);
			return value;
		}

		public bool TryRemoveMin(out T value)
		{
			value = default(T);
			if (this.elementList.Count == 0)
			{
				return false;
			}
			value = this.elementList[0].Value;
			this.RemoveAt(0);
			return true;
		}

		public T Remove(PriorityQueueElement<T> element)
		{
			int index = element.Index;
			if (!object.ReferenceEquals(this.elementList[index], element))
			{
				throw new ArgumentException("Index of element had been corrupted or Invalid container.");
			}
			this.RemoveAt(index);
			return element.Value;
		}

		public T PeekMin()
		{
			if (this.elementList.Count == 0)
			{
				throw new InvalidOperationException("Priority queue is Empty.");
			}
			return this.elementList[0].Value;
		}

		private void RemoveAt(int index)
		{
			int priority = this.elementList[index].Priority;
			this.elementList[index].Invalidate();
			PriorityQueueElement<T> priorityQueueElement = this.elementList[this.elementList.Count - 1];
			this.elementList.RemoveAt(this.elementList.Count - 1);
			if (!priorityQueueElement.Valid)
			{
				return;
			}
			priorityQueueElement.Index = index;
			if (this.ComparePriority(priorityQueueElement.Priority, priority))
			{
				this.Promote(priorityQueueElement);
				return;
			}
			if (this.ComparePriority(priority, priorityQueueElement.Priority))
			{
				this.Demote(priorityQueueElement);
				return;
			}
			this.elementList[index] = priorityQueueElement;
		}

		private void Promote(PriorityQueueElement<T> element)
		{
			int num = element.Index;
			int priority = element.Priority;
			int num2 = (num - 1) / 2;
			while (num > 0 && this.ComparePriority(priority, this.elementList[num2].Priority))
			{
				this.elementList[num2].Index = num;
				this.elementList[num] = this.elementList[num2];
				num = num2;
				num2 = (num - 1) / 2;
			}
			element.Index = num;
			this.elementList[num] = element;
		}

		private void Demote(PriorityQueueElement<T> element)
		{
			int num = element.Index;
			int priority = element.Priority;
			for (;;)
			{
				int num2 = num * 2 + 1;
				if (num2 >= this.elementList.Count)
				{
					break;
				}
				if (num2 + 1 < this.elementList.Count && this.elementList[num2].Priority > this.elementList[num2 + 1].Priority)
				{
					num2++;
				}
				if (!this.ComparePriority(this.elementList[num2].Priority, priority))
				{
					break;
				}
				this.elementList[num2].Index = num;
				this.elementList[num] = this.elementList[num2];
				num = num2;
			}
			element.Index = num;
			this.elementList[num] = element;
		}

		private bool ComparePriority(int prior, int posterior)
		{
			return prior - posterior < 0;
		}

		private List<PriorityQueueElement<T>> elementList;
	}
}
