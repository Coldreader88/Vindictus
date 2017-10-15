using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
	public class LRUCache<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		public LRUCache(int capacity)
		{
			if (capacity <= 0)
			{
				throw new ArgumentException("capacity should always be greater then 0");
			}
			this.data = new Dictionary<TKey, TValue>();
			this.lruList = new LRUCache<TKey, TValue>.IndexedLinkedList<TKey>();
			this.capacity = capacity;
		}

		public void Add(TKey key, TValue value)
		{
			if (!this.ContainsKey(key))
			{
				this[key] = value;
				return;
			}
			throw new ArgumentException("duplicated key");
		}

		public bool ContainsKey(TKey key)
		{
			return this.data.ContainsKey(key);
		}

		public ICollection<TKey> Keys
		{
			get
			{
				return this.data.Keys;
			}
		}

		public bool Remove(TKey key)
		{
			bool result = this.data.Remove(key);
			this.lruList.Remove(key);
			return result;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			if (this.ContainsKey(key))
			{
				value = this[key];
				return true;
			}
			value = default(TValue);
			return false;
		}

		public ICollection<TValue> Values
		{
			get
			{
				return this.data.Values;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				TValue result = this.data[key];
				this.lruList.Remove(key);
				this.lruList.Add(key);
				return result;
			}
			set
			{
				this.data[key] = value;
				this.lruList.Remove(key);
				this.lruList.Add(key);
				if (this.data.Count > this.capacity && this.lruList.First != null)
				{
					this.Remove(this.lruList.First);
				}
			}
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		public void Clear()
		{
			this.data.Clear();
			this.lruList.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.data.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)this.data).CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			bool flag = ((ICollection<KeyValuePair<TKey, TValue>>)this.data).Remove(item);
			if (flag)
			{
				this.lruList.Remove(item.Key);
			}
			return flag;
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		private Dictionary<TKey, TValue> data;

		private LRUCache<TKey, TValue>.IndexedLinkedList<TKey> lruList;

		private int capacity;

		private class IndexedLinkedList<T>
		{
			public T First
			{
				get
				{
					if (this.data.First == null)
					{
						return default(T);
					}
					return this.data.First.Value;
				}
			}

			public void Add(T value)
			{
				this.index[value] = this.data.AddLast(value);
			}

			public void RemoveFirst()
			{
				this.index.Remove(this.data.First.Value);
				this.data.RemoveFirst();
			}

			public void Remove(T value)
			{
				LinkedListNode<T> node;
				if (this.index.TryGetValue(value, out node))
				{
					this.data.Remove(node);
					this.index.Remove(value);
				}
			}

			public int Count
			{
				get
				{
					return this.data.Count;
				}
			}

			public void Clear()
			{
				this.data.Clear();
				this.index.Clear();
			}

			private LinkedList<T> data = new LinkedList<T>();

			private Dictionary<T, LinkedListNode<T>> index = new Dictionary<T, LinkedListNode<T>>();
		}
	}
}
