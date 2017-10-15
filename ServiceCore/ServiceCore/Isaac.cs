using System;

namespace ServiceCore
{
	internal sealed class Isaac
	{
		public Isaac()
		{
			this.Init(false);
		}

		public Isaac(uint seed)
		{
			seed *= 4u;
			this.rsl[0] = seed;
			uint num = seed % 64u;
			this.rsl[(int)((UIntPtr)num)] = seed;
			this.Init(true);
		}

		private void Generate()
		{
			this.b += (this.c += 1u);
			uint num = 0u;
			uint num2 = 128u;
			while (num < 128u)
			{
				uint num3 = this.mem[(int)((UIntPtr)num)];
				this.a ^= this.a << 13;
				this.a += this.mem[(int)((UIntPtr)(num2++))];
				uint num4 = this.mem[(int)((UIntPtr)num)] = this.mem[(int)((UIntPtr)((num3 & 1020u) >> 2))] + this.a + this.b;
				this.rsl[(int)((UIntPtr)(num++))] = (this.b = this.mem[(int)((UIntPtr)((num4 >> 8 & 1020u) >> 2))] + num3);
				num3 = this.mem[(int)((UIntPtr)num)];
				this.a ^= this.a >> 6;
				this.a += this.mem[(int)((UIntPtr)(num2++))];
				num4 = (this.mem[(int)((UIntPtr)num)] = this.mem[(int)((UIntPtr)((num3 & 1020u) >> 2))] + this.a + this.b);
				this.rsl[(int)((UIntPtr)(num++))] = (this.b = this.mem[(int)((UIntPtr)((num4 >> 8 & 1020u) >> 2))] + num3);
				num3 = this.mem[(int)((UIntPtr)num)];
				this.a ^= this.a << 2;
				this.a += this.mem[(int)((UIntPtr)(num2++))];
				num4 = (this.mem[(int)((UIntPtr)num)] = this.mem[(int)((UIntPtr)((num3 & 1020u) >> 2))] + this.a + this.b);
				this.rsl[(int)((UIntPtr)(num++))] = (this.b = this.mem[(int)((UIntPtr)((num4 >> 8 & 1020u) >> 2))] + num3);
				num3 = this.mem[(int)((UIntPtr)num)];
				this.a ^= this.a >> 16;
				this.a += this.mem[(int)((UIntPtr)(num2++))];
				num4 = (this.mem[(int)((UIntPtr)num)] = this.mem[(int)((UIntPtr)((num3 & 1020u) >> 2))] + this.a + this.b);
				this.rsl[(int)((UIntPtr)(num++))] = (this.b = this.mem[(int)((UIntPtr)((num4 >> 8 & 1020u) >> 2))] + num3);
			}
			num2 = 0u;
			while (num2 < 128u)
			{
				uint num3 = this.mem[(int)((UIntPtr)num)];
				this.a ^= this.a << 13;
				this.a += this.mem[(int)((UIntPtr)(num2++))];
				uint num4 = this.mem[(int)((UIntPtr)num)] = this.mem[(int)((UIntPtr)((num3 & 1020u) >> 2))] + this.a + this.b;
				this.rsl[(int)((UIntPtr)(num++))] = (this.b = this.mem[(int)((UIntPtr)((num4 >> 8 & 1020u) >> 2))] + num3);
				num3 = this.mem[(int)((UIntPtr)num)];
				this.a ^= this.a >> 6;
				this.a += this.mem[(int)((UIntPtr)(num2++))];
				num4 = (this.mem[(int)((UIntPtr)num)] = this.mem[(int)((UIntPtr)((num3 & 1020u) >> 2))] + this.a + this.b);
				this.rsl[(int)((UIntPtr)(num++))] = (this.b = this.mem[(int)((UIntPtr)((num4 >> 8 & 1020u) >> 2))] + num3);
				num3 = this.mem[(int)((UIntPtr)num)];
				this.a ^= this.a << 2;
				this.a += this.mem[(int)((UIntPtr)(num2++))];
				num4 = (this.mem[(int)((UIntPtr)num)] = this.mem[(int)((UIntPtr)((num3 & 1020u) >> 2))] + this.a + this.b);
				this.rsl[(int)((UIntPtr)(num++))] = (this.b = this.mem[(int)((UIntPtr)((num4 >> 8 & 1020u) >> 2))] + num3);
				num3 = this.mem[(int)((UIntPtr)num)];
				this.a ^= this.a >> 16;
				this.a += this.mem[(int)((UIntPtr)(num2++))];
				num4 = (this.mem[(int)((UIntPtr)num)] = this.mem[(int)((UIntPtr)((num3 & 1020u) >> 2))] + this.a + this.b);
				this.rsl[(int)((UIntPtr)(num++))] = (this.b = this.mem[(int)((UIntPtr)((num4 >> 8 & 1020u) >> 2))] + num3);
			}
		}

		private void Init(bool flag)
		{
			uint num8;
			uint num7;
			uint num6;
			uint num5;
			uint num4;
			uint num3;
			uint num2;
			uint num = num2 = (num3 = (num4 = (num5 = (num6 = (num7 = (num8 = 2654435769u))))));
			for (int i = 0; i < 4; i++)
			{
				num2 ^= num << 11;
				num4 += num2;
				num += num3;
				num ^= num3 >> 2;
				num5 += num;
				num3 += num4;
				num3 ^= num4 << 8;
				num6 += num3;
				num4 += num5;
				num4 ^= num5 >> 16;
				num7 += num4;
				num5 += num6;
				num5 ^= num6 << 10;
				num8 += num5;
				num6 += num7;
				num6 ^= num7 >> 4;
				num2 += num6;
				num7 += num8;
				num7 ^= num8 << 8;
				num += num7;
				num8 += num2;
				num8 ^= num2 >> 9;
				num3 += num8;
				num2 += num;
			}
			for (int i = 0; i < 256; i += 8)
			{
				if (flag)
				{
					num2 += this.rsl[i];
					num += this.rsl[i + 1];
					num3 += this.rsl[i + 2];
					num4 += this.rsl[i + 3];
					num5 += this.rsl[i + 4];
					num6 += this.rsl[i + 5];
					num7 += this.rsl[i + 6];
					num8 += this.rsl[i + 7];
				}
				num2 ^= num << 11;
				num4 += num2;
				num += num3;
				num ^= num3 >> 2;
				num5 += num;
				num3 += num4;
				num3 ^= num4 << 8;
				num6 += num3;
				num4 += num5;
				num4 ^= num5 >> 16;
				num7 += num4;
				num5 += num6;
				num5 ^= num6 << 10;
				num8 += num5;
				num6 += num7;
				num6 ^= num7 >> 4;
				num2 += num6;
				num7 += num8;
				num7 ^= num8 << 8;
				num += num7;
				num8 += num2;
				num8 ^= num2 >> 9;
				num3 += num8;
				num2 += num;
				this.mem[i] = num2;
				this.mem[i + 1] = num;
				this.mem[i + 2] = num3;
				this.mem[i + 3] = num4;
				this.mem[i + 4] = num5;
				this.mem[i + 5] = num6;
				this.mem[i + 6] = num7;
				this.mem[i + 7] = num8;
			}
			if (flag)
			{
				for (int i = 0; i < 256; i += 8)
				{
					num2 += this.mem[i];
					num += this.mem[i + 1];
					num3 += this.mem[i + 2];
					num4 += this.mem[i + 3];
					num5 += this.mem[i + 4];
					num6 += this.mem[i + 5];
					num7 += this.mem[i + 6];
					num8 += this.mem[i + 7];
					num2 ^= num << 11;
					num4 += num2;
					num += num3;
					num ^= num3 >> 2;
					num5 += num;
					num3 += num4;
					num3 ^= num4 << 8;
					num6 += num3;
					num4 += num5;
					num4 ^= num5 >> 16;
					num7 += num4;
					num5 += num6;
					num5 ^= num6 << 10;
					num8 += num5;
					num6 += num7;
					num6 ^= num7 >> 4;
					num2 += num6;
					num7 += num8;
					num7 ^= num8 << 8;
					num += num7;
					num8 += num2;
					num8 ^= num2 >> 9;
					num3 += num8;
					num2 += num;
					this.mem[i] = num2;
					this.mem[i + 1] = num;
					this.mem[i + 2] = num3;
					this.mem[i + 3] = num4;
					this.mem[i + 4] = num5;
					this.mem[i + 5] = num6;
					this.mem[i + 6] = num7;
					this.mem[i + 7] = num8;
				}
			}
			this.Generate();
			this.count = 256;
		}

		public uint Next()
		{
			if (this.count-- == 0)
			{
				this.Generate();
				this.count = 255;
			}
			return this.rsl[this.count];
		}

		private const int SIZEL = 8;

		private const int SIZE = 256;

		private const int MASK = 1020;

		private int count;

		private uint[] rsl = new uint[256];

		private uint[] mem = new uint[256];

		private uint a;

		private uint b;

		private uint c;
	}
}
