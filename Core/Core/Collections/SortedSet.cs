using System;
using System.Collections;
using System.Collections.Generic;

namespace Devcat.Core.Collections
{
	[Serializable]
	public class SortedSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		public SortedSet() : this(Comparer<T>.Default)
		{
		}

		public SortedSet(IComparer<T> comparer)
		{
			this.treeSet = new TreeSet<T>(comparer);
		}

		public SortedSet(IEnumerable<T> enumerable) : this(enumerable, Comparer<T>.Default)
		{
		}

		public SortedSet(IEnumerable<T> enumerable, IComparer<T> comparer)
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			this.treeSet = new TreeSet<T>(comparer);
			foreach (T item in enumerable)
			{
				this.treeSet.Add(item);
			}
		}

		public void Add(T value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.treeSet.Add(value);
		}

		public bool TryAdd(T value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.Contains(value))
			{
				this.treeSet.Add(value);
				return true;
			}
			return false;
		}

		public void TryAddRange(IEnumerable<T> enumerable)
		{
			foreach (T value in enumerable)
			{
				this.TryAdd(value);
			}
		}

		public void Clear()
		{
			this.treeSet.Clear();
		}

		public bool Contains(T value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.treeSet.Contains(value);
		}

		public void CopyTo(T[] array, int index)
		{
			this.treeSet.CopyTo(array, index);
		}

		public T[] ToArray()
		{
			T[] array = new T[this.Count];
			this.CopyTo(array, 0);
			return array;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this.treeSet.GetEnumerator();
		}

		public bool Remove(T value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.treeSet.Remove(value);
		}

		public IComparer<T> Comparer
		{
			get
			{
				return this.treeSet.Comparer;
			}
		}

		public int Count
		{
			get
			{
				return this.treeSet.Count;
			}
		}

		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private TreeSet<T> treeSet;
	}
}
