using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
	[Serializable]
	public class MultiDictionary<K, T> : ICollection<KeyValuePair<K, T>>, IEnumerable<KeyValuePair<K, T>>, IEnumerable
	{
		public MultiDictionary()
		{
			this.data = new Dictionary<K, HashSet<T>>();
		}

		private HashSet<T> LookupHashSet(K key)
		{
			HashSet<T> result = null;
			this.data.TryGetValue(key, out result);
			return result;
		}

		public HashSet<T> this[K key]
		{
			get
			{
				HashSet<T> hashSet = this.LookupHashSet(key);
				if (hashSet == null)
				{
					return new HashSet<T>();
				}
				return hashSet;
			}
		}

		public bool Add(K key, T value)
		{
			HashSet<T> hashSet = this.LookupHashSet(key);
			if (hashSet == null)
			{
				this.data.Add(key, new HashSet<T>
				{
					value
				});
				this.count++;
				return true;
			}
			if (hashSet.Add(value))
			{
				this.count++;
				return true;
			}
			return false;
		}

		public bool Remove(K key, T value)
		{
			HashSet<T> hashSet = this.LookupHashSet(key);
			if (hashSet == null)
			{
				return false;
			}
			if (hashSet.Remove(value))
			{
				this.count--;
				if (hashSet.Count == 0)
				{
					this.data.Remove(key);
				}
				return true;
			}
			return false;
		}

		public List<T> ToList(K key)
		{
			HashSet<T> hashSet = this.LookupHashSet(key);
			if (hashSet == null)
			{
				return new List<T>();
			}
			return hashSet.ToList<T>();
		}

		public IEnumerable<K> Keys
		{
			get
			{
				return this.data.Keys;
			}
		}

		public IEnumerable<T> Values
		{
			get
			{
				foreach (HashSet<T> set in this.data.Values)
				{
					foreach (T value in set)
					{
						yield return value;
					}
				}
				yield break;
			}
		}

		public IEnumerable<HashSet<T>> Sets
		{
			get
			{
				foreach (HashSet<T> set in this.data.Values)
				{
					yield return set;
				}
				yield break;
			}
		}

		public IEnumerable<KeyValuePair<K, HashSet<T>>> KeySets
		{
			get
			{
				foreach (KeyValuePair<K, HashSet<T>> keyset in this.data)
				{
					yield return keyset;
				}
				yield break;
			}
		}

		public int CountValues(K key)
		{
			HashSet<T> hashSet = this.LookupHashSet(key);
			if (hashSet == null)
			{
				return 0;
			}
			return hashSet.Count;
		}

		public bool ContainsKey(K key)
		{
			return this.data.ContainsKey(key);
		}

		public bool ContainsValue(T value)
		{
			foreach (HashSet<T> hashSet in this.data.Values)
			{
				if (hashSet.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		public bool Contains(K key, T value)
		{
			HashSet<T> hashSet = this.LookupHashSet(key);
			return hashSet != null && hashSet.Contains(value);
		}

		public bool TryGetValue(K key, out HashSet<T> value)
		{
			return this.data.TryGetValue(key, out value);
		}

		public bool TryGetFirstValue(K key, out T value)
		{
			HashSet<T> source;
			if (this.data.TryGetValue(key, out source))
			{
				value = source.FirstOrDefault<T>();
				return value != null;
			}
			value = default(T);
			return false;
		}

		public void Add(KeyValuePair<K, T> item)
		{
			this.Add(item.Key, item.Value);
		}

		public void Clear()
		{
			this.data.Clear();
			this.count = 0;
		}

		public bool Contains(KeyValuePair<K, T> item)
		{
			HashSet<T> hashSet = this.LookupHashSet(item.Key);
			return hashSet != null && hashSet.Contains(item.Value);
		}

		public void CopyTo(KeyValuePair<K, T>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (arrayIndex < 0 || array.Length <= arrayIndex)
			{
				throw new ArgumentOutOfRangeException();
			}
			int num = arrayIndex;
			foreach (KeyValuePair<K, HashSet<T>> keyValuePair in this.data)
			{
				foreach (T value in keyValuePair.Value)
				{
					if (num >= array.Length)
					{
						throw new ArgumentException();
					}
					array[num] = new KeyValuePair<K, T>(keyValuePair.Key, value);
					num++;
				}
			}
		}

		public int Count
		{
			get
			{
				return this.count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public bool Remove(KeyValuePair<K, T> item)
		{
			return this.Remove(item.Key, item.Value);
		}

		public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
		{
			return new MultiDictionary<K, T>.MultiDictionaryEnumerator<K, T>(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private Dictionary<K, HashSet<T>> data;

		private int count;

		private struct MultiDictionaryEnumerator<KE, TE> : IEnumerator<KeyValuePair<KE, TE>>, IDisposable, IEnumerator
		{
			public MultiDictionaryEnumerator(MultiDictionary<KE, TE> Parent)
			{
				this.Parent = Parent;
				this.DictionaryEnumerator = this.Parent.data.GetEnumerator();
				this.HashSetEnumerator = null;
			}

			public KeyValuePair<KE, TE> Current
			{
				get
				{
					if (this.HashSetEnumerator == null)
					{
						throw new InvalidOperationException("MoveNext를 적어도 한번 호출해야 Current를 조회할 수 있습니다.");
					}
					KeyValuePair<KE, HashSet<TE>> keyValuePair = this.DictionaryEnumerator.Current;
					return new KeyValuePair<KE, TE>(keyValuePair.Key, this.HashSetEnumerator.Current);
				}
			}

			public void Dispose()
			{
				if (this.DictionaryEnumerator != null)
				{
					this.DictionaryEnumerator.Dispose();
				}
				if (this.HashSetEnumerator != null)
				{
					this.HashSetEnumerator.Dispose();
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			public bool MoveNext()
			{
				if (this.HashSetEnumerator != null && this.HashSetEnumerator.MoveNext())
				{
					return true;
				}
				if (this.DictionaryEnumerator.MoveNext())
				{
					if (this.HashSetEnumerator != null)
					{
						this.HashSetEnumerator.Dispose();
					}
					KeyValuePair<KE, HashSet<TE>> keyValuePair = this.DictionaryEnumerator.Current;
					this.HashSetEnumerator = keyValuePair.Value.GetEnumerator();
					this.HashSetEnumerator.MoveNext();
					return true;
				}
				return false;
			}

			public void Reset()
			{
				this.Dispose();
				this.DictionaryEnumerator = this.Parent.data.GetEnumerator();
				this.HashSetEnumerator = null;
			}

			private MultiDictionary<KE, TE> Parent;

			private IEnumerator<KeyValuePair<KE, HashSet<TE>>> DictionaryEnumerator;

			private IEnumerator<TE> HashSetEnumerator;
		}
	}
}
