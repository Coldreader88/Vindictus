using System;
using System.Text;

namespace NPL.SSO
{
	internal class Utility
	{
		public static byte ComputeHash(int sn)
		{
			return (byte)(sn >> 24);
		}

		public static byte ComputeHash(string text)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(text.ToLower().Trim());
			byte b = 103;
			foreach (byte b2 in bytes)
			{
				b = (byte)((int)b + ((int)b << 3) + (int)b2);
			}
			return (byte)(b & 127);
		}

		public static string IPToString(uint ip)
		{
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				(ip >> 24 & 255u).ToString(),
				(ip >> 16 & 255u).ToString(),
				(ip >> 8 & 255u).ToString(),
				(ip & 255u).ToString()
			});
		}
	}
}
