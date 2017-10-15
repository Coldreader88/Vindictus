using System;

namespace Devcat.Core.Net.Message
{
	internal sealed class Isaac
	{
		public Isaac()
		{
			this.Init(false);
		}

		public Isaac(int seed)
		{
			this.rsl[0] = seed;
			this.Init(true);
		}

		private void Generate()
		{
			this.b += ++this.c;
			int i = 0;
			int j = 128;
			while (i < 128)
			{
				int num = this.mem[i];
				this.a ^= this.a << 13;
				this.a += this.mem[j++];
				int num2 = this.mem[i] = this.mem[(num & 1020) >> 2] + this.a + this.b;
				this.rsl[i++] = (this.b = this.mem[(num2 >> 8 & 1020) >> 2] + num);
				num = this.mem[i];
				this.a ^= (int)((uint)this.a >> 6);
				this.a += this.mem[j++];
				num2 = (this.mem[i] = this.mem[(num & 1020) >> 2] + this.a + this.b);
				this.rsl[i++] = (this.b = this.mem[(num2 >> 8 & 1020) >> 2] + num);
				num = this.mem[i];
				this.a ^= this.a << 2;
				this.a += this.mem[j++];
				num2 = (this.mem[i] = this.mem[(num & 1020) >> 2] + this.a + this.b);
				this.rsl[i++] = (this.b = this.mem[(num2 >> 8 & 1020) >> 2] + num);
				num = this.mem[i];
				this.a ^= (int)((uint)this.a >> 16);
				this.a += this.mem[j++];
				num2 = (this.mem[i] = this.mem[(num & 1020) >> 2] + this.a + this.b);
				this.rsl[i++] = (this.b = this.mem[(num2 >> 8 & 1020) >> 2] + num);
			}
			j = 0;
			while (j < 128)
			{
				int num = this.mem[i];
				this.a ^= this.a << 13;
				this.a += this.mem[j++];
				int num2 = this.mem[i] = this.mem[(num & 1020) >> 2] + this.a + this.b;
				this.rsl[i++] = (this.b = this.mem[(num2 >> 8 & 1020) >> 2] + num);
				num = this.mem[i];
				this.a ^= (int)((uint)this.a >> 6);
				this.a += this.mem[j++];
				num2 = (this.mem[i] = this.mem[(num & 1020) >> 2] + this.a + this.b);
				this.rsl[i++] = (this.b = this.mem[(num2 >> 8 & 1020) >> 2] + num);
				num = this.mem[i];
				this.a ^= this.a << 2;
				this.a += this.mem[j++];
				num2 = (this.mem[i] = this.mem[(num & 1020) >> 2] + this.a + this.b);
				this.rsl[i++] = (this.b = this.mem[(num2 >> 8 & 1020) >> 2] + num);
				num = this.mem[i];
				this.a ^= (int)((uint)this.a >> 16);
				this.a += this.mem[j++];
				num2 = (this.mem[i] = this.mem[(num & 1020) >> 2] + this.a + this.b);
				this.rsl[i++] = (this.b = this.mem[(num2 >> 8 & 1020) >> 2] + num);
			}
		}

		private void Init(bool flag)
		{
			int num8;
			int num7;
			int num6;
			int num5;
			int num4;
			int num3;
			int num2;
			int num = num2 = (num3 = (num4 = (num5 = (num6 = (num7 = (num8 = -1640531527))))));
			for (int i = 0; i < 4; i++)
			{
				num2 ^= num << 11;
				num4 += num2;
				num += num3;
				num ^= (int)((uint)num3 >> 2);
				num5 += num;
				num3 += num4;
				num3 ^= num4 << 8;
				num6 += num3;
				num4 += num5;
				num4 ^= (int)((uint)num5 >> 16);
				num7 += num4;
				num5 += num6;
				num5 ^= num6 << 10;
				num8 += num5;
				num6 += num7;
				num6 ^= (int)((uint)num7 >> 4);
				num2 += num6;
				num7 += num8;
				num7 ^= num8 << 8;
				num += num7;
				num8 += num2;
				num8 ^= (int)((uint)num2 >> 9);
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
				num ^= (int)((uint)num3 >> 2);
				num5 += num;
				num3 += num4;
				num3 ^= num4 << 8;
				num6 += num3;
				num4 += num5;
				num4 ^= (int)((uint)num5 >> 16);
				num7 += num4;
				num5 += num6;
				num5 ^= num6 << 10;
				num8 += num5;
				num6 += num7;
				num6 ^= (int)((uint)num7 >> 4);
				num2 += num6;
				num7 += num8;
				num7 ^= num8 << 8;
				num += num7;
				num8 += num2;
				num8 ^= (int)((uint)num2 >> 9);
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
					num ^= (int)((uint)num3 >> 2);
					num5 += num;
					num3 += num4;
					num3 ^= num4 << 8;
					num6 += num3;
					num4 += num5;
					num4 ^= (int)((uint)num5 >> 16);
					num7 += num4;
					num5 += num6;
					num5 ^= num6 << 10;
					num8 += num5;
					num6 += num7;
					num6 ^= (int)((uint)num7 >> 4);
					num2 += num6;
					num7 += num8;
					num7 ^= num8 << 8;
					num += num7;
					num8 += num2;
					num8 ^= (int)((uint)num2 >> 9);
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

		public int Next()
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

		private int[] rsl = new int[256];

		private int[] mem = new int[256];

		private int a;

		private int b;

		private int c;
	}
}
