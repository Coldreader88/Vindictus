using System;
using System.Net;

namespace Nexon.Nisms
{
	internal static class Extention
	{
		public static int CalculateStructureSize(this bool value)
		{
			return 1;
		}

		public static int CalculateStructureSize(this byte value)
		{
			return 1;
		}

		public static int CalculateStructureSize(this short value)
		{
			return 2;
		}

		public static int CalculateStructureSize(this int value)
		{
			return 4;
		}

		public static int CalculateStructureSize(this long value)
		{
			return 8;
		}

		public static int CalculateStructureSize(this string value)
		{
			int num = (value == null) ? 0 : Connection.EncodeType.GetByteCount(value);
			return 2 + num;
		}

		public static int CalculateStructureSize(this DateTime value)
		{
			return 8;
		}

		public static int CalculateStructureSize(this Guid value)
		{
			return value.ToString().CalculateStructureSize();
		}

		public static int CalculateStructureSize(this IPAddress value)
		{
			return 4;
		}
	}
}
