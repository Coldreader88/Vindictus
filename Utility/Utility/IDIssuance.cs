using System;
using System.Collections.Generic;

namespace Utility
{
	public class IDIssuance
	{
		public IDIssuance(int min, int max)
		{
			if (min > max || max >= 2147483647 || min <= -2147483648)
			{
				throw new ArgumentOutOfRangeException("IDIssuance : 범위가 잘못되었습니다.", string.Format("min = {0}, max = {1}", min, max));
			}
			this.rangeList = new LinkedList<IDIssuance.Range>();
			this.rangeList.AddFirst(new IDIssuance.Range(min, max));
			this.nextID = min;
		}

		private void checkNode(LinkedListNode<IDIssuance.Range> node)
		{
			if (node.Value.min > node.Value.max)
			{
				this.rangeList.Remove(node);
			}
		}

		public bool HasFreeRange
		{
			get
			{
				return this.rangeList.Count > 0;
			}
		}

		public int ReserveFirst()
		{
			if (this.rangeList.Count == 0)
			{
				throw new InvalidOperationException("범위가 고갈되었습니다!");
			}
			LinkedListNode<IDIssuance.Range> first = this.rangeList.First;
			int min = first.Value.min;
			first.Value = new IDIssuance.Range(first.Value.min + 1, first.Value.max);
			this.checkNode(first);
			this.nextID = min + 1;
			return min;
		}

		public int ReserveNext()
		{
			if (this.TryReserveNextOf(this.nextID, out this.nextID))
			{
				int result = this.nextID;
				this.nextID++;
				if (this.rangeList.Count != 0 && this.nextID > this.rangeList.Last.Value.max)
				{
					this.nextID = this.rangeList.First.Value.min;
				}
				return result;
			}
			return this.nextID = this.ReserveFirst();
		}

		public bool TryReserveNextOf(int target, out int reserved)
		{
			reserved = target;
			for (LinkedListNode<IDIssuance.Range> linkedListNode = this.rangeList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value.min > target)
				{
					reserved = linkedListNode.Value.min;
					linkedListNode.Value = new IDIssuance.Range(linkedListNode.Value.min + 1, linkedListNode.Value.max);
					this.checkNode(linkedListNode);
					return true;
				}
				if (linkedListNode.Value.min == target)
				{
					linkedListNode.Value = new IDIssuance.Range(linkedListNode.Value.min + 1, linkedListNode.Value.max);
					this.checkNode(linkedListNode);
					return true;
				}
				if (linkedListNode.Value.min < target && linkedListNode.Value.max > target)
				{
					IDIssuance.Range value = new IDIssuance.Range(target + 1, linkedListNode.Value.max);
					linkedListNode.Value = new IDIssuance.Range(linkedListNode.Value.min, target - 1);
					this.rangeList.AddAfter(linkedListNode, value);
					return true;
				}
				if (linkedListNode.Value.max == target)
				{
					linkedListNode.Value = new IDIssuance.Range(linkedListNode.Value.min, linkedListNode.Value.max - 1);
					this.checkNode(linkedListNode);
					return true;
				}
			}
			reserved = 0;
			return false;
		}

		public bool TryReserve(int target)
		{
			LinkedListNode<IDIssuance.Range> linkedListNode = this.rangeList.First;
			while (linkedListNode != null && linkedListNode.Value.min <= target)
			{
				if (linkedListNode.Value.min == target)
				{
					linkedListNode.Value = new IDIssuance.Range(linkedListNode.Value.min + 1, linkedListNode.Value.max);
					this.checkNode(linkedListNode);
					return true;
				}
				if (linkedListNode.Value.min < target && linkedListNode.Value.max > target)
				{
					IDIssuance.Range value = new IDIssuance.Range(target + 1, linkedListNode.Value.max);
					linkedListNode.Value = new IDIssuance.Range(linkedListNode.Value.min, target - 1);
					this.rangeList.AddAfter(linkedListNode, value);
					return true;
				}
				if (linkedListNode.Value.max == target)
				{
					linkedListNode.Value = new IDIssuance.Range(linkedListNode.Value.min, linkedListNode.Value.max - 1);
					this.checkNode(linkedListNode);
					return true;
				}
				linkedListNode = linkedListNode.Next;
			}
			return false;
		}

		public bool TryRelease(int target)
		{
			if (this.rangeList.Count == 0)
			{
				this.rangeList.AddFirst(new IDIssuance.Range(target, target));
				return true;
			}
			for (LinkedListNode<IDIssuance.Range> linkedListNode = this.rangeList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value.min - 1 > target)
				{
					IDIssuance.Range value = new IDIssuance.Range(target, target);
					this.rangeList.AddBefore(linkedListNode, value);
					return true;
				}
				if (linkedListNode.Value.min - 1 == target)
				{
					linkedListNode.Value = new IDIssuance.Range(linkedListNode.Value.min - 1, linkedListNode.Value.max);
					return true;
				}
				if (linkedListNode.Value.min <= target && linkedListNode.Value.max >= target)
				{
					return false;
				}
				if (linkedListNode.Value.max + 1 == target)
				{
					linkedListNode.Value = new IDIssuance.Range(linkedListNode.Value.min, linkedListNode.Value.max + 1);
					return true;
				}
			}
			return false;
		}

		private LinkedList<IDIssuance.Range> rangeList;

		private int nextID;

		private struct Range
		{
			public Range(int mi, int ma)
			{
				this.min = mi;
				this.max = ma;
			}

			public readonly int min;

			public readonly int max;
		}
	}
}
