using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nexon.Com
{
	public static class ParseUtil
	{
		public static TReturnType Do<TSource, TReturnType>(this TSource obj, Func<TSource, TReturnType> action)
		{
			return action(obj);
		}

		public static T Parse<T>(this object obj)
		{
			return obj.Parse(default(T));
		}

		public static T Parse<T>(this object obj, T defaultVal)
		{
			try
			{
				if (defaultVal is Enum)
				{
					if (obj is string)
					{
						return (T)((object)Enum.Parse(typeof(T), obj.ToString()));
					}
					return (T)((object)obj);
				}
				else if (defaultVal is string)
				{
					if (!string.IsNullOrEmpty((string)Convert.ChangeType(obj, typeof(T))))
					{
						return (T)((object)Convert.ChangeType(obj, typeof(T)));
					}
				}
				else
				{
					if (defaultVal == null)
					{
						return (T)((object)obj);
					}
					return (T)((object)Convert.ChangeType(obj, typeof(T)));
				}
			}
			catch (Exception)
			{
			}
			return defaultVal;
		}

		public static bool IsSameDay(this DateTime dt, DateTime dt2)
		{
			return dt.Year == dt2.Year && dt.Month == dt2.Month && dt.Day == dt2.Day;
		}

		public static DateTime? ConvertNullable(this DateTime dt)
		{
			if (dt == DateTime.MinValue)
			{
				return null;
			}
			return new DateTime?(dt);
		}

		public static DateTime ConvertNotNullable(this DateTime? dt)
		{
			if (dt == null)
			{
				return DateTime.MinValue;
			}
			return Convert.ToDateTime(dt);
		}

		public static string GetFormattedDate(this DateTime dt)
		{
			string text = string.Empty;
			if (DateTime.Today.IsSameDay(dt))
			{
				text = dt.ToString("tt hh:mm", DateTimeFormatInfo.InvariantInfo);
				text = text.Replace("오전", "AM").Replace("오후", "PM");
			}
			else
			{
				text = dt.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
			}
			return text;
		}

		public static string AddLink(string value)
		{
			if (value != null)
			{
				string pattern = "((https?|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\\\\\))[\\w\\d:#%/;$()~_?\\-=\\\\\\.&]*)";
				Regex regex = new Regex(pattern);
				return regex.Replace(value, (Match m) => string.Format("<a href='{0}' target='_blank'>{0}</a>", m.ToString()));
			}
			return value;
		}

		public static string ReplaceNewLine(string value)
		{
			return value.Replace("\r\n", "<br/>");
		}

		public static string ReplaceTag(this string value)
		{
			return value.Replace("<", "&lt;").Replace(">", "&gt;");
		}

		public static IEnumerable<TSource> GetRandom<TSource>(this IEnumerable<TSource> source, int n4Count)
		{
			Random rnd = new Random();
			return (from pOrder in source
			orderby rnd.Next()
			select pOrder).Take(n4Count);
		}

		public static string GetFileContents(this FileInfo fileInfo)
		{
			if (fileInfo.Exists)
			{
				using (StreamReader streamReader = fileInfo.OpenText())
				{
					return streamReader.ReadToEnd();
				}
			}
			return string.Empty;
		}

		public static string GetShortText(this string strValue, int count)
		{
			string result = strValue;
			int num = 0;
			int length = 0;
			for (int i = 0; i < strValue.Length; i++)
			{
				if (Convert.ToInt32(Convert.ToChar(strValue.Substring(i, 1))) > 255)
				{
					num += 2;
				}
				else if (Convert.ToInt32(Convert.ToChar(strValue.Substring(i, 1))) >= 97 && Convert.ToInt32(Convert.ToChar(strValue.Substring(i, 1))) <= 122)
				{
					if (Convert.ToInt32(Convert.ToChar(strValue.Substring(i, 1))) == 109 || Convert.ToInt32(Convert.ToChar(strValue.Substring(i, 1))) == 119)
					{
						num += 2;
					}
					else
					{
						num++;
					}
				}
				else if (Convert.ToInt32(Convert.ToChar(strValue.Substring(i, 1))) >= 48 && Convert.ToInt32(Convert.ToChar(strValue.Substring(i, 1))) <= 57)
				{
					num++;
				}
				else if (ParseUtil.isNarrowCharacter(Convert.ToChar(strValue.Substring(i, 1))))
				{
					num++;
				}
				else
				{
					num += 2;
				}
				if (num <= count)
				{
					length = i;
				}
			}
			if (num > count)
			{
				result = strValue.Substring(0, length) + "...";
			}
			return result;
		}

		private static bool isNarrowCharacter(char ch)
		{
			int num = Convert.ToInt32(ch);
			if (num <= 59)
			{
				switch (num)
				{
				case 33:
				case 34:
				case 39:
				case 40:
				case 41:
				case 42:
				case 44:
				case 45:
				case 46:
				case 47:
					break;
				case 35:
				case 36:
				case 37:
				case 38:
				case 43:
					return false;
				default:
					switch (num)
					{
					case 58:
					case 59:
						break;
					default:
						return false;
					}
					break;
				}
			}
			else
			{
				switch (num)
				{
				case 91:
				case 93:
				case 95:
					break;
				case 92:
				case 94:
					return false;
				default:
					switch (num)
					{
					case 123:
					case 124:
					case 125:
						break;
					default:
						return false;
					}
					break;
				}
			}
			return true;
		}
	}
}
