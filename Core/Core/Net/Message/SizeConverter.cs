using System;

namespace Devcat.Core.Net.Message
{
    [Obsolete("Packet length is now 4 byte.")]
    internal class SizeConverter
    {
        public static int GetAllocateSize(int compressedSize)
        {
            if (compressedSize <= 8192)
            {
                return compressedSize;
            }
            compressedSize -= 8192;
            int num = 8192;
            int num2 = 4;
            while (compressedSize >= 6144)
            {
                compressedSize -= 6144;
                num <<= 2;
                num2 <<= 2;
                if (num2 > 262144)
                {
                    throw new ArgumentOutOfRangeException("compressedSize");
                }
            }
            return num + compressedSize * num2;
        }

        public static int GetAllocateSize(int originalSize, out int compressedSize)
        {
            if (originalSize <= 8192)
            {
                compressedSize = originalSize;
                return originalSize;
            }
            originalSize -= 8192;
            compressedSize = 8192;
            int num = 8192;
            int num2 = 4;
            while (true)
            {
                int num3 = 6144 * num2;
                if (originalSize <= (num3 - num2))
                {
                    int num4 = ((originalSize + num2) - 1) / num2;
                    compressedSize += num4;
                    return (num + (num4 * num2));
                }
                originalSize -= num3;
                num += num3;
                compressedSize += 6144;
                num2 = num2 << 2;
                if (num2 > 262144)
                {
                    throw new ArgumentOutOfRangeException("originalSize");
                }
            }
        }
    }
}
