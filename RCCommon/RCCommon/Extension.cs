using System;
using System.Collections.Generic;

namespace RemoteControlSystem
{
	public static class Extension
	{
		public static IDictionary<Type, int> GetConverter(this IEnumerable<Type> types)
		{
			return types.GetConverter(1);
		}

		public static IDictionary<Type, int> GetConverter(this IEnumerable<Type> types, int startCategoryId)
		{
			Dictionary<Type, int> dictionary = new Dictionary<Type, int>();
			foreach (Type type in types)
			{
				if (type.IsSealed)
				{
					dictionary.Add(type, startCategoryId++);
				}
				else
				{
					dictionary.Add(type, 0);
				}
			}
			return dictionary;
		}

		public static string ParseCommand(this string t, IList<string> arguments)
		{
			string text = t.Trim();
			arguments.Clear();
			int num = text.IndexOfAny(Extension.seperator);
			if (num == -1)
			{
				return text;
			}
			string result = text.Substring(0, num);
			while (text.Length > num)
			{
				text = text.Substring(num + 1).TrimStart(new char[0]);
				if (text.Length == 0)
				{
					break;
				}
				char value = ' ';
				if (text[0] == '\'' && text.Length > 1)
				{
					value = '\'';
					text = text.Substring(1);
				}
				else if (text[0] == '"' && text.Length > 1)
				{
					value = '"';
					text = text.Substring(1);
				}
				num = text.IndexOf(value);
				if (num == -1)
				{
					arguments.Add(text);
					break;
				}
				arguments.Add(text.Substring(0, num));
			}
			return result;
		}

		private static readonly char[] seperator = new char[]
		{
			' ',
			'\'',
			'"'
		};
	}
}
