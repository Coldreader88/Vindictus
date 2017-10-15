using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace ServiceCore.ItemServiceOperations
{
	public class ItemClassExBuilder
	{
		public static string Build(string itemClass, ICollection<ItemAttributeElement> attributes)
		{
			if (attributes.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(itemClass);
				stringBuilder.Append("[");
				stringBuilder.Append(ItemClassExBuilder.Build(attributes));
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
			return itemClass;
		}

		public static string Build(string itemClass, IDictionary<string, ItemAttributeElement> attributes)
		{
			return ItemClassExBuilder.Build(itemClass, attributes.Values);
		}

		public static string Build(string itemClass, int enhanceLevel)
		{
			if (enhanceLevel > 0)
			{
				return string.Format("{0}[ENHANCE:{1}]", itemClass, enhanceLevel);
			}
			return itemClass;
		}

		public static string Build(string itemClass, int enhanceLevel, int qualityLevel)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(itemClass);
			if (enhanceLevel > 0)
			{
				stringBuilder.AppendFormat("[ENHANCE:{0}]", enhanceLevel);
			}
			stringBuilder.AppendFormat("[QUALITY:({0})]", qualityLevel);
			return stringBuilder.ToString();
		}

		public static string Build(IDictionary<string, ItemAttributeElement> attributes)
		{
			return ItemClassExBuilder.Build(attributes.Values);
		}

		public static string Build(ICollection<ItemAttributeElement> attributes)
		{
			if (attributes.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (ItemAttributeElement itemAttributeElement in attributes)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(itemAttributeElement.AttributeName);
					stringBuilder.Append(":");
					stringBuilder.Append(itemAttributeElement.Value);
					if (itemAttributeElement.Arg != 0)
					{
						stringBuilder.Append("(");
						stringBuilder.Append(itemAttributeElement.Arg);
						stringBuilder.Append(")");
					}
					if (itemAttributeElement.Arg2 != 0)
					{
						stringBuilder.Append("<");
						stringBuilder.Append(itemAttributeElement.Arg2);
						stringBuilder.Append(">");
					}
				}
				return stringBuilder.ToString();
			}
			return "";
		}

		public static bool Parse(string itemClassEx, out string itemClass)
		{
			string[] array = itemClassEx.Split(new char[]
			{
				',',
				'[',
				']'
			});
			if (array.Length == 0)
			{
				Log<ItemClassExBuilder>.Logger.WarnFormat("No Data [{0}]", itemClassEx);
				itemClass = null;
				return false;
			}
			itemClass = array[0];
			return true;
		}

		public static bool Parse(string itemClassEx, out string itemClass, out Dictionary<string, ItemAttributeElement> attributes)
		{
			string[] array = itemClassEx.Split(new char[]
			{
				',',
				'[',
				']'
			});
			if (array.Length == 0)
			{
				Log<ItemClassExBuilder>.Logger.WarnFormat("No Data [{0}]", itemClassEx);
				itemClass = null;
				attributes = null;
				return false;
			}
			itemClass = array[0];
			attributes = new Dictionary<string, ItemAttributeElement>();
			foreach (string text in array.Skip(1))
			{
				if (!(text == ""))
				{
					ItemAttributeElement itemAttributeElement = ItemClassExBuilder.ParseSingleAttribute(text);
					if (itemAttributeElement != null)
					{
						try
						{
							attributes.Add(itemAttributeElement.AttributeName, itemAttributeElement);
						}
						catch (Exception arg)
						{
							throw new Exception(string.Format("Item Parse Error [ItemClass : {1}] [AttributeName : {2}]- {0}", arg, itemClassEx, itemAttributeElement.AttributeName));
						}
					}
				}
			}
			return true;
		}

		public static bool Parse(string attrEx, out Dictionary<string, ItemAttributeElement> attributes)
		{
			string[] array = attrEx.Split(new char[]
			{
				',',
				'[',
				']'
			});
			if (array.Length == 0)
			{
				attributes = new Dictionary<string, ItemAttributeElement>();
				return true;
			}
			attributes = new Dictionary<string, ItemAttributeElement>();
			foreach (string text in array)
			{
				if (!(text == ""))
				{
					ItemAttributeElement itemAttributeElement = ItemClassExBuilder.ParseSingleAttribute(text);
					if (itemAttributeElement != null)
					{
						try
						{
							attributes.Add(itemAttributeElement.AttributeName, itemAttributeElement);
						}
						catch (Exception arg)
						{
							throw new Exception(string.Format("Item Parse Error [attrEx : {1}] [AttributeName : {2}]- {0}", arg, attrEx, itemAttributeElement.AttributeName));
						}
					}
				}
			}
			return true;
		}

		public static ItemAttributeElement ParseSingleAttribute(string attrStr)
		{
			string text = null;
			string text2 = null;
			int arg = 0;
			int arg2 = 0;
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < attrStr.Length)
			{
				char c = attrStr[i];
				char c2 = c;
				switch (c2)
				{
				case '(':
					if (num2 == 1)
					{
						text2 = attrStr.Substring(num, i - num).Trim();
					}
					num2 = 2;
					num = i + 1;
					break;
				case ')':
					if (num2 == 2)
					{
						arg = ItemClassExBuilder.ToArgInt(attrStr.Substring(num, i - num));
					}
					num2 = -1;
					break;
				default:
					switch (c2)
					{
					case ':':
						if (num2 != 0)
						{
							Log<ItemClassExBuilder>.Logger.WarnFormat("parse Error [{0}, {1}]", attrStr, i);
							return null;
						}
						text = attrStr.Substring(num, i - num).Trim();
						num2 = 1;
						num = i + 1;
						break;
					case '<':
						if (num2 == 1)
						{
							text2 = attrStr.Substring(num, i - num).Trim();
						}
						num2 = 3;
						num = i + 1;
						break;
					case '>':
						if (num2 == 3)
						{
							arg2 = ItemClassExBuilder.ToArgInt(attrStr.Substring(num, i - num));
						}
						num2 = -1;
						break;
					}
					break;
				}
				i++;
			}
			if (num2 == 1)
			{
				text2 = attrStr.Substring(num, i - num).Trim();
			}
			if (text != null && text2 != null)
			{
				return new ItemAttributeElement(text, text2, arg, arg2);
			}
			return null;
		}

		private static int ToArgInt(string str)
		{
			if (str == null)
			{
				return 0;
			}
			int num;
			if (int.TryParse(str, out num))
			{
				return num;
			}
			DateTime value;
			if (DateTime.TryParse(str, out value))
			{
				return value.ToInt32();
			}
			if (!str.EndsWith("sec"))
			{
				Log<ItemClassExBuilder>.Logger.WarnFormat("Invalid Argument [{0}]", str);
				return 0;
			}
			string s = str.Substring(0, str.Length - 3);
			if (int.TryParse(s, out num))
			{
				return DateTime.Now.AddSeconds((double)num).ToInt32();
			}
			Log<ItemClassExBuilder>.Logger.WarnFormat("Invalid Argument [{0}]", str);
			return 0;
		}

		public static bool IsSameItemClassEx(string itemClass1, Dictionary<string, ItemAttributeElement> attrDict1, string itemClass2, Dictionary<string, ItemAttributeElement> attrDict2)
		{
			if (itemClass1 != itemClass2)
			{
				return false;
			}
			if (attrDict1.Count != attrDict2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, ItemAttributeElement> keyValuePair in attrDict1)
			{
				if (!attrDict2.ContainsKey(keyValuePair.Key))
				{
					return false;
				}
				ItemAttributeElement itemAttributeElement = attrDict2.TryGetValue(keyValuePair.Key);
				if (itemAttributeElement.Value != keyValuePair.Value.Value)
				{
					return false;
				}
				if (itemAttributeElement.Arg != keyValuePair.Value.Arg)
				{
					return false;
				}
				if (itemAttributeElement.Arg2 != keyValuePair.Value.Arg2)
				{
					return false;
				}
			}
			return true;
		}
	}
}
