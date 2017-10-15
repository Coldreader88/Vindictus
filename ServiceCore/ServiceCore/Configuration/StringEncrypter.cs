using System;
using System.Security.Cryptography;
using System.Text;

namespace ServiceCore.Configuration
{
	public class StringEncrypter
	{
		public static string Decrypt(string crypt, string keyString, bool forceDecryptWithoutPrefix)
		{
			if (!crypt.StartsWith("crypt::") && !forceDecryptWithoutPrefix)
			{
				return crypt;
			}
			string text = crypt;
			if (!forceDecryptWithoutPrefix)
			{
				text = crypt.Substring(7);
			}
			Random random = new Random((keyString.GetHashCode() + 126991) % 1048576);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length; i += 2)
			{
				char c = text[i];
				char c2 = text[i + 1];
				int num;
				if (c >= 'A')
				{
					num = (int)(c - 'A' + '\n');
				}
				else
				{
					num = (int)(c - '0');
				}
				int num2;
				if (c2 >= 'A')
				{
					num2 = (int)(c2 - 'A' + '\n');
				}
				else
				{
					num2 = (int)(c2 - '0');
				}
				int num3 = num * 16 + num2;
				num3 = (num3 + 128 - random.Next(128)) % 128;
				stringBuilder.Append((char)num3);
			}
			return stringBuilder.ToString();
		}

        public static string Encrypt(string plaintext, string keyString, bool ignorePrefix)
        {
            StringBuilder stringBuilder;
            if (ignorePrefix)
            {
                stringBuilder = new StringBuilder("");
            }
            else
            {
                stringBuilder = new StringBuilder("crypt::");
            }
            Random random = new Random((keyString.GetHashCode() + 126991) % 1048576);
            for (int i = 0; i < plaintext.Length; i++)
            {
                int num = (int)plaintext[i];
                num = (num + random.Next(128)) % 128;
                stringBuilder.Append(StringEncrypter.hexdic[num / 16]);
                stringBuilder.Append(StringEncrypter.hexdic[num % 16]);
            }
            return stringBuilder.ToString();
        }

        public static string Hashing(string plaintext)
		{
			SHA512 sha = new SHA512Managed();
			byte[] array = sha.ComputeHash(Encoding.ASCII.GetBytes(plaintext));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		private static char[] hexdic = new char[]
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
	}
}
