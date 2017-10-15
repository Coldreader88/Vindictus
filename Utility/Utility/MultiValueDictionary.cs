using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
	[Serializable]
	public class MultiValueDictionary<K, T> : ICollection<KeyValuePair<K, T>>, IEnumerable<KeyValuePair<K, T>>, IEnumerable
	{
		public MultiValueDictionary()
		{
			this.data = new Dictionary<K, List<T>>();
		}

		private List<T> LookupList(K key)
		{
			List<T> result = null;
			this.data.TryGetValue(key, out result);
			return result;
		}

		public List<T> this[K key]
		{
			get
			{
				List<T> list = this.LookupList(key);
				if (list == null)
				{
					return new List<T>();
				}
				return list;
			}
		}

		public bool Add(K key, T value)
		{
			List<T> list = this.LookupList(key);
			if (list == null)
			{
				this.data.Add(key, new List<T>
				{
					value
				});
			}
			else
			{
				list.Add(value);
			}
			this.count++;
			return true;
		}

		public bool Remove(K key, T value)
		{
			List<T> list = this.LookupList(key);
			if (list == null)
			{
				return false;
			}
			if (list.Remove(value))
			{
				this.count--;
				if (list.Count == 0)
				{
					this.data.Remove(key);
				}
				return true;
			}
			return false;
		}

		public List<T> ToList(K key)
		{
			List<T> list = this.LookupList(key);
			if (list == null)
			{
				return new List<T>();
			}
			return list;
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
				foreach (List<T> list in this.data.Values)
				{
					foreach (T value in list)
					{
						yield return value;
					}
				}
				yield break;
			}
		}

		public IEnumerable<List<T>> Lists
		{
			get
			{
				foreach (List<T> list in this.data.Values)
				{
					yield return list;
				}
				yield break;
			}
		}

		public IEnumerable<KeyValuePair<K, List<T>>> KeyValues
		{
			get
			{
				foreach (KeyValuePair<K, List<T>> keyvaluelist in this.data)
				{
					yield return keyvaluelist;
				}
				yield break;
			}
		}

		public int CountValues(K key)
		{
			List<T> list = this.LookupList(key);
			if (list == null)
			{
				return 0;
			}
			return list.Count;
		}

		public bool ContainsKey(K key)
		{
			return this.data.ContainsKey(key);
		}

		public bool ContainsValue(T value)
		{
			foreach (List<T> list in this.data.Values)
			{
				if (list.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		public bool Contains(K key, T value)
		{
			List<T> list = this.LookupList(key);
			return list != null && list.Contains(value);
		}

		public bool TryGetValue(K key, out List<T> value)
		{
			return this.data.TryGetValue(key, out value);
		}

		public bool TryGetFirstValue(K key, out T value)
		{
			List<T> source;
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
			List<T> list = this.LookupList(item.Key);
			return list != null && list.Contains(item.Value);
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
			foreach (KeyValuePair<K, List<T>> keyValuePair in this.data)
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
			return new MultiValueDictionary<K, T>.MultiValueDictionaryEnumerator<K, T>(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private Dictionary<K, List<T>> data;

		private int count;

		private struct MultiValueDictionaryEnumerator<KE, TE> : IEnumerator<KeyValuePair<KE, TE>>, IDisposable, IEnumerator
		{
			public MultiValueDictionaryEnumerator(MultiValueDictionary<KE, TE> Parent)
			{
				this.Parent = Parent;
				this.DictionaryEnumerator = this.Parent.data.GetEnumerator();
				this.ListEnumerator = null;
			}

			public KeyValuePair<KE, TE> Current
			{
				get
				{
					if (this.ListEnumerator == null)
					{
						throw new InvalidOperationException("MoveNext를 적어도 한번 호출해야 Current를 조회할 수 있습니다.");
					}
					KeyValuePair<KE, List<TE>> keyValuePair = this.DictionaryEnumerator.Current;
					return new KeyValuePair<KE, TE>(keyValuePair.Key, this.ListEnumerator.Current);
				}
			}

			public void Dispose()
			{
				if (this.DictionaryEnumerator != null)
				{
					this.DictionaryEnumerator.Dispose();
				}
				if (this.ListEnumerator != null)
				{
					this.ListEnumerator.Dispose();
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
				if (this.ListEnumerator != null && this.ListEnumerator.MoveNext())
				{
					return true;
				}
				if (this.DictionaryEnumerator.MoveNext())
				{
					if (this.ListEnumerator != null)
					{
						this.ListEnumerator.Dispose();
					}
					KeyValuePair<KE, List<TE>> keyValuePair = this.DictionaryEnumerator.Current;
					this.ListEnumerator = keyValuePair.Value.GetEnumerator();
					this.ListEnumerator.MoveNext();
					return true;
				}
				return false;
			}

			public void Reset()
			{
				this.Dispose();
				this.DictionaryEnumerator = this.Parent.data.GetEnumerator();
				this.ListEnumerator = null;
			}

			private MultiValueDictionary<KE, TE> Parent;

			private IEnumerator<KeyValuePair<KE, List<TE>>> DictionaryEnumerator;

			private IEnumerator<TE> ListEnumerator;
		}
	}
}
