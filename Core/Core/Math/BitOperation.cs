using System;

namespace Devcat.Core.Math
{
	public static class BitOperation
	{
		public static int CountLeadingZeroes(int number)
		{
			int num = 32;
			uint num2 = (uint)number;
			if (num2 > 65535u)
			{
				num -= 16;
				num2 >>= 16;
			}
			if (num2 > 255u)
			{
				num -= 8;
				num2 >>= 8;
			}
			if (num2 > 15u)
			{
				num -= 4;
				num2 >>= 4;
			}
			if (num2 > 3u)
			{
				num -= 2;
				num2 >>= 2;
			}
			if (num2 > 1u)
			{
				num--;
				num2 >>= 1;
			}
			return num - (int)num2;
		}

		public static int CountBit(uint x)
		{
			uint num = x >> 1 & 2004318071u;
			x -= num;
			num = (num >> 1 & 2004318071u);
			x -= num;
			num = (num >> 1 & 2004318071u);
			x -= num;
			x = (x + (x >> 4) & 252645135u);
			x *= 16843009u;
			return (int)(x >> 24);
		}

		public static uint SmallestPow2(uint x)
		{
			x -= 1u;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1u;
		}

		public static uint LargestPow2(uint x)
		{
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x - (x >> 1);
		}
	}
}
