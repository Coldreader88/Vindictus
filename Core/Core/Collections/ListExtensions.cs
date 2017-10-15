using System;
using System.Collections.Generic;

namespace Devcat.Core.Collections
{
	public class ListExtensions
	{
		public static int BinarySearch<T, U>(IList<T> list, U value, IComparer<T, U> comparer)
		{
			int i = 0;
			int num = list.Count;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				int num3 = comparer.Compare(list[num2], value);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2;
				}
			}
			return ~i;
		}

		public static int BinarySearch<T, U>(IList<T> list, U value, Comparison<T, U> comparison)
		{
			int i = 0;
			int num = list.Count;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				int num3 = comparison(list[num2], value);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2;
				}
			}
			return ~i;
		}
	}
}
