using System;
using System.Net;
using System.Text;

namespace Nexon.CafeAuthJPN
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
			int num = (value == null) ? 0 : Encoding.Default.GetByteCount(value);
			return 1 + num;
		}

		public static int CalculateStructureSize(this IPAddress value)
		{
			return 4;
		}

		public static int CalculateStructureSize(this MachineID value)
		{
			return 16;
		}
	}
}
