using System;
using System.Collections.Generic;

namespace Utility
{
	public static class DictionaryExtension
	{
		public static bool AddOrIncrease<T>(this IDictionary<T, int> dictionary, T key, int value)
		{
			if (dictionary.ContainsKey(key))
			{
				dictionary[key] += value;
				return false;
			}
			dictionary.Add(key, value);
			return true;
		}

		public static V TryGetValue<T, V>(this IDictionary<T, V> dictionary, T key)
		{
			V result;
			dictionary.TryGetValue(key, out result);
			return result;
		}

		public static HashSet<V> TryGetValue<T, V>(this MultiDictionary<T, V> dictionary, T key)
		{
			HashSet<V> result;
			dictionary.TryGetValue(key, out result);
			return result;
		}

		public static V TryGetFirstValue<T, V>(this MultiDictionary<T, V> dictionary, T key)
		{
			V result;
			dictionary.TryGetFirstValue(key, out result);
			return result;
		}

		public static void Add<K1, K2, V>(this Dictionary<K1, Dictionary<K2, V>> dictionary, K1 key1, K2 key2, V value)
		{
			if (dictionary.ContainsKey(key1))
			{
				dictionary[key1].Add(key2, value);
				return;
			}
			dictionary[key1] = new Dictionary<K2, V>
			{
				{
					key2,
					value
				}
			};
		}

		public static bool ContainsKey<K1, K2, V>(this Dictionary<K1, Dictionary<K2, V>> dictionary, K1 key1, K2 key2)
		{
			return dictionary.ContainsKey(key1) && dictionary[key1].ContainsKey(key2);
		}

		public static V TryGetValue<K1, K2, V>(this Dictionary<K1, Dictionary<K2, V>> dictionary, K1 key1, K2 key2)
		{
			Dictionary<K2, V> dictionary2 = dictionary.TryGetValue(key1);
			if (dictionary2 != null)
			{
				return dictionary2.TryGetValue(key2);
			}
			return default(V);
		}

		public static bool Remove<K1, K2, V>(this Dictionary<K1, Dictionary<K2, V>> dictionary, K1 key1, K2 key2)
		{
			Dictionary<K2, V> dictionary2 = dictionary.TryGetValue(key1);
			return dictionary2 != null && dictionary2.Remove(key2);
		}

		public static void Add<K1, K2, K3, V>(this Dictionary<K1, Dictionary<K2, Dictionary<K3, V>>> dictionary, K1 key1, K2 key2, K3 key3, V value)
		{
			if (dictionary.ContainsKey(key1))
			{
				dictionary[key1].Add(key2, key3, value);
				return;
			}
			dictionary[key1] = new Dictionary<K2, Dictionary<K3, V>>();
			dictionary[key1].Add(key2, key3, value);
		}

		public static bool ContainsKey<K1, K2, K3, V>(this Dictionary<K1, Dictionary<K2, Dictionary<K3, V>>> dictionary, K1 key1, K2 key2, K3 key3)
		{
			return dictionary.ContainsKey(key1) && dictionary[key1].ContainsKey(key2, key3);
		}

		public static V TryGetValue<K1, K2, K3, V>(this Dictionary<K1, Dictionary<K2, Dictionary<K3, V>>> dictionary, K1 key1, K2 key2, K3 key3)
		{
			Dictionary<K2, Dictionary<K3, V>> dictionary2 = dictionary.TryGetValue(key1);
			if (dictionary2 != null)
			{
				return dictionary2.TryGetValue(key2, key3);
			}
			return default(V);
		}

		public static bool Remove<K1, K2, K3, V>(this Dictionary<K1, Dictionary<K2, Dictionary<K3, V>>> dictionary, K1 key1, K2 key2, K3 key3)
		{
			Dictionary<K2, Dictionary<K3, V>> dictionary2 = dictionary.TryGetValue(key1);
			return dictionary2 != null && dictionary2.Remove(key2, key3);
		}
	}
}
