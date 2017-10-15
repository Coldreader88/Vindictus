using System;
using System.Collections.Generic;

namespace Executer
{
	internal static class Utility
	{
		public static string ParseCommand(this string t, IList<string> arguments)
		{
			string text = t.Trim();
			arguments.Clear();
			int num = text.IndexOfAny(Utility.seperator);
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
