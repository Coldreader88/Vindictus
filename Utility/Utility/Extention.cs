using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Utility
{
	public static class Extention
	{
		public static int ToInt32(this DateTime value)
		{
			DateTime d = (value.Kind == DateTimeKind.Local) ? value.ToUniversalTime() : value;
			return (int)((d - Extention.epoch).Ticks / 10000000L);
		}

		public static DateTime ToDateTime(this int value)
		{
			return Extention.epoch + value.ToTimeSpan();
		}

		public static TimeSpan ToTimeSpan(this int value)
		{
			return new TimeSpan(0, 0, value);
		}

        public static bool IsPrivateNetwork(this IPAddress ipAddress)
        {
            byte[] addressBytes = ipAddress.GetAddressBytes();
            byte[][,] array = Extention.privateNetwork;
            for (int i = 0; i < array.Length; i++)
            {
                byte[,] array2 = array[i];
                int num = array2.GetLowerBound(0);
                int upperBound = array2.GetUpperBound(0);
                while (num <= upperBound && (addressBytes[num] & array2[num, 1]) == array2[num, 0])
                {
                    if (num == upperBound)
                    {
                        return true;
                    }
                    num++;
                }
            }
            return false;
        }

        public static List<T> Shuffle<T>(this List<T> list, Random random)
		{
			return list.ShuffleFirstNthElement(random, list.Count);
		}

		public static List<T> ShuffleFirstNthElement<T>(this List<T> list, Random random, int n)
		{
			if (list.Count < n)
			{
				n = list.Count;
			}
			foreach (int num in Enumerable.Range(0, n))
			{
				int index = random.Next(num, list.Count);
				T value = list[num];
				list[num] = list[index];
				list[index] = value;
			}
			return list;
		}

		public static string ToFormatableString(DateTime dateTime)
		{
			return dateTime.Ticks.ToString();
		}

		public static DateTime? ToFormatableDateTime(string strTicks)
		{
			long ticks = 0L;
			if (long.TryParse(strTicks, out ticks))
			{
				return new DateTime?(new DateTime(ticks));
			}
			return null;
		}

		public static string ToHex(this byte[] data)
		{
			return data.ToHex("");
		}

		public static string ToHex(this byte[] data, string prefix)
		{
			char[] array = new char[]
			{
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9',
				'A',
				'B',
				'C',
				'D',
				'E',
				'F'
			};
			int i = 0;
			int num = prefix.Length;
			int num2 = data.Length;
			char[] array2 = new char[num2 * 2 + num];
			while (i < num)
			{
				array2[i] = prefix[i];
				i++;
			}
			i = -1;
			num2--;
			num--;
			while (i < num2)
			{
				byte b = data[++i];
				array2[++num] = array[(int)(b / 16)];
				array2[++num] = array[(int)(b % 16)];
			}
			return new string(array2, 0, array2.Length);
		}

		public static byte[] FromHex(this string str)
		{
			return str.FromHex(0, 0, 0);
		}

		public static byte[] FromHex(this string str, int offset, int step)
		{
			return str.FromHex(offset, step, 0);
		}

		public static byte[] FromHex(this string str, int offset, int step, int tail)
		{
			byte[] array = new byte[(str.Length - offset - tail + step) / (2 + step)];
			int num = str.Length - tail;
			int num2 = step + 1;
			int num3 = 0;
			for (int i = offset; i < num; i += num2)
			{
				byte b = (byte)str[i];
				if (b > 96)
				{
					b -= 87;
				}
				else if (b > 64)
				{
					b -= 55;
				}
				else
				{
					b -= 48;
				}
				byte b2 = (byte)str[++i];
				if (b2 > 96)
				{
					b2 -= 87;
				}
				else if (b2 > 64)
				{
					b2 -= 55;
				}
				else
				{
					b2 -= 48;
				}
				array[num3] = (byte)(((int)b << 4) + (int)b2);
				num3++;
			}
			return array;
		}

		static Extention()
		{
			// Note: this type is marked as 'beforefieldinit'.
			byte[][,] array = new byte[4][,];
			byte[][,] array2 = array;
			int num = 0;
			byte[,] array3 = new byte[1, 2];
			array3[0, 0] = 10;
			array3[0, 1] = byte.MaxValue;
			array2[num] = array3;
			array[1] = new byte[,]
			{
				{
					172,
					byte.MaxValue
				},
				{
					16,
					240
				}
			};
			array[2] = new byte[,]
			{
				{
					192,
					byte.MaxValue
				},
				{
					168,
					byte.MaxValue
				}
			};
			array[3] = new byte[,]
			{
				{
					169,
					byte.MaxValue
				},
				{
					254,
					byte.MaxValue
				}
			};
			Extention.privateNetwork = array;
		}

		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private static readonly byte[][,] privateNetwork;
	}
}
