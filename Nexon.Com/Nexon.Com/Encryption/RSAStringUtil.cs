using System;
using System.Globalization;
using System.Text;

namespace Nexon.Com.Encryption
{
	internal class RSAStringUtil
	{
		public static byte[] HexStringToBytes(string hex)
		{
			if (hex.Length == 0)
			{
				return new byte[1];
			}
			if (hex.Length % 2 == 1)
			{
				hex = "0" + hex;
			}
			byte[] array = new byte[hex.Length / 2];
			for (int i = 0; i < hex.Length / 2; i++)
			{
				array[i] = byte.Parse(hex.Substring(2 * i, 2), NumberStyles.AllowHexSpecifier);
			}
			return array;
		}

		public static string BytesToHexString(byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			for (int i = 0; i < data.Length; i++)
			{
				stringBuilder.Append(string.Format("{0:X2}", data[i]));
			}
			return stringBuilder.ToString();
		}

		public static string BytesToDecString(byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			for (int i = 0; i < data.Length; i++)
			{
				stringBuilder.Append(string.Format((i == 0) ? "{0:D3}" : "-{0:D3} ", data[i]));
			}
			return stringBuilder.ToString();
		}

		public static string ASCIIBytesToString(byte[] data)
		{
			ASCIIEncoding asciiencoding = new ASCIIEncoding();
			return asciiencoding.GetString(data);
		}

		public static string UTF16BytesToString(byte[] data)
		{
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			return unicodeEncoding.GetString(data);
		}

		public static string UTF8BytesToString(byte[] data)
		{
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			return utf8Encoding.GetString(data);
		}

		public static string ToBase64(byte[] data)
		{
			return Convert.ToBase64String(data);
		}

		public static byte[] FromBase64(string data)
		{
			return Convert.FromBase64String(data);
		}
	}
}
