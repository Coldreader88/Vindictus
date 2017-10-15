using System;
using System.Collections.Generic;
using Utility;

namespace ServiceCore.HeroesContents
{
	public static class IEnumerableExtension
	{
		public static Dictionary<K1, Dictionary<K2, V>> ToDoubleDictionary<T, K1, K2, V>(this IEnumerable<T> list, Func<T, K1> KeyFunc1, Func<T, K2> KeyFunc2, Func<T, V> ValueFunc)
		{
			Dictionary<K1, Dictionary<K2, V>> dictionary = new Dictionary<K1, Dictionary<K2, V>>();
			foreach (T arg in list)
			{
				K1 k = KeyFunc1(arg);
				K2 k2 = KeyFunc2(arg);
				V value = ValueFunc(arg);
				if (dictionary.ContainsKey(k))
				{
					if (dictionary[k].ContainsKey(k2))
					{
						Log<ContentsSqlDataContext>.Logger.ErrorFormat("ToDoubleDictionary : key duplicated {0} {1}", k, k2);
					}
					dictionary[k][k2] = value;
				}
				else
				{
					dictionary.Add(k, new Dictionary<K2, V>
					{
						{
							k2,
							value
						}
					});
				}
			}
			return dictionary;
		}

		public static Dictionary<K1, Dictionary<K2, List<V>>> ToDoubleListDictionary<T, K1, K2, V>(this IEnumerable<T> list, Func<T, K1> KeyFunc1, Func<T, K2> KeyFunc2, Func<T, V> ValueFunc)
		{
			Dictionary<K1, Dictionary<K2, List<V>>> dictionary = new Dictionary<K1, Dictionary<K2, List<V>>>();
			foreach (T arg in list)
			{
				K1 key = KeyFunc1(arg);
				K2 key2 = KeyFunc2(arg);
				V item = ValueFunc(arg);
				if (dictionary.ContainsKey(key))
				{
					Dictionary<K2, List<V>> dictionary2 = dictionary[key];
					if (dictionary2.ContainsKey(key2))
					{
						dictionary2[key2].Add(item);
					}
					else
					{
						dictionary2[key2] = new List<V>
						{
							item
						};
					}
				}
				else
				{
					dictionary.Add(key, new Dictionary<K2, List<V>>
					{
						{
							key2,
							new List<V>
							{
								item
							}
						}
					});
				}
			}
			return dictionary;
		}

		public static Dictionary<K, List<T>> ToListDictionary<T, K>(this IEnumerable<T> list, Func<T, K> KeyFunc)
		{
			Dictionary<K, List<T>> dictionary = new Dictionary<K, List<T>>();
			foreach (T t in list)
			{
				K key = KeyFunc(t);
				if (dictionary.ContainsKey(key))
				{
					dictionary[key].Add(t);
				}
				else
				{
					dictionary.Add(key, new List<T>
					{
						t
					});
				}
			}
			return dictionary;
		}

		public static Dictionary<K, List<V>> ToListDictionary<T, K, V>(this IEnumerable<T> list, Func<T, K> KeyFunc, Func<T, V> ValueFunc)
		{
			Dictionary<K, List<V>> dictionary = new Dictionary<K, List<V>>();
			foreach (T arg in list)
			{
				K key = KeyFunc(arg);
				V item = ValueFunc(arg);
				if (dictionary.ContainsKey(key))
				{
					dictionary[key].Add(item);
				}
				else
				{
					dictionary.Add(key, new List<V>
					{
						item
					});
				}
			}
			return dictionary;
		}

		public static Dictionary<K, T> ToDictionary_OverwriteDuplicated<K, T>(this IEnumerable<T> list, Func<T, K> KeyFunc)
		{
			Dictionary<K, T> dictionary = new Dictionary<K, T>();
			foreach (T t in list)
			{
				K key = KeyFunc(t);
				dictionary[key] = t;
			}
			return dictionary;
		}

		public static MultiDictionary<K, V> ToMultiDictionary<K, V, T>(this IEnumerable<T> list, Func<T, K> KeyFunc, Func<T, V> ValueFunc)
		{
			MultiDictionary<K, V> multiDictionary = new MultiDictionary<K, V>();
			foreach (T arg in list)
			{
				K key = KeyFunc(arg);
				V value = ValueFunc(arg);
				multiDictionary.Add(key, value);
			}
			return multiDictionary;
		}
	}
}
