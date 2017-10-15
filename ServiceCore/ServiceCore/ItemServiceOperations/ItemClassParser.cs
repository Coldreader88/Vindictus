using System;
using Utility;

namespace ServiceCore.ItemServiceOperations
{
	public class ItemClassParser
	{
		public static int GetLevel(string itemClass)
		{
			string[] array = itemClass.Split(new char[]
			{
				'_'
			});
			foreach (string text in array)
			{
				if (text.StartsWith("lvl") && text.Length > 3)
				{
					string value = text.Substring(3);
					try
					{
						return Convert.ToInt32(value);
					}
					catch (Exception ex)
					{
						Log<ItemClassParser>.Logger.InfoFormat("레벨 정보가 없습니다. : {0}  [{1}]", itemClass, ex);
						return 0;
					}
				}
			}
			return 0;
		}
	}
}
