using System;
using System.Collections.Generic;

namespace ServiceCore
{
	public static class DictionaryExtension
	{
		public static void InsertOrUpdate(this Dictionary<string, int> dict, string k, int v)
		{
			if (dict.ContainsKey(k))
			{
				dict[k] = v;
				return;
			}
			dict.Add(k, v);
		}

		public static void InsertOrUpdate(this Dictionary<string, object> dict, string k, object v)
		{
			if (dict.ContainsKey(k))
			{
				dict[k] = v;
				return;
			}
			dict.Add(k, v);
		}

		public static void InsertOrIncrease(this Dictionary<string, int> dict, string k, int v)
		{
			if (dict.ContainsKey(k))
			{
				dict[k] += v;
				return;
			}
			dict.Add(k, v);
		}

		public static void InsertOrIncrease(this Dictionary<string, object> dict, string k, object v)
		{
			if (dict.ContainsKey(k))
			{
				object obj = dict[k];
				if (obj == null)
				{
					dict[k] = v;
					return;
				}
				if (!(obj.GetType() == v.GetType()))
				{
					throw new Exception("ServiceReportor exception: InsertOrIncrease() failed");
				}
				if (v is int)
				{
					dict[k] = (int)obj + (int)v;
					return;
				}
				if (v is long)
				{
					dict[k] = (long)obj + (long)v;
					return;
				}
			}
			else
			{
				dict.Add(k, v);
			}
		}
	}
}
