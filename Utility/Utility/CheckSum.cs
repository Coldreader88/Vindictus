using System;

namespace Utility
{
	public class CheckSum
	{
		public static ulong Make(byte[] bytes, int size)
		{
			return CheckSum.Make(bytes, size, -1);
		}

		public static ulong Make(byte[] bytes, int size, int baseValue)
		{
			ulong num = 0UL;
			if (baseValue < 0)
			{
				baseValue = (int)bytes[0];
			}
			int num2 = baseValue % 31 % size;
			num2 = (int)(bytes[num2] % 32);
			ulong num3 = (ulong)((long)num2);
			if (size > 40)
			{
				num3 = BitConverter.ToUInt64(bytes, num2);
			}
			int num4 = size / 8;
			int i = size % 8;
			for (int j = 0; j < num4; j++)
			{
				num += BitConverter.ToUInt64(bytes, j * 8);
			}
			while (i > 0)
			{
				num += (ulong)bytes[size - i];
				i--;
			}
			return num ^ num3;
		}
	}
}
