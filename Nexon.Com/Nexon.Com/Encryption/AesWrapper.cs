using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nexon.Com.Encryption
{
	public class AesWrapper
	{
		public static KeyNIvSet GenerateKey()
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.GenerateKey();
			rijndaelManaged.GenerateIV();
			return new KeyNIvSet(rijndaelManaged.Key, rijndaelManaged.IV);
		}

		public static byte[] Encrypt(byte[] Key, byte[] Iv, CipherMode Mode, PaddingMode Padding, byte[] Plain)
		{
			ICryptoTransform transform = new RijndaelManaged
			{
				Mode = Mode,
				Padding = Padding
			}.CreateEncryptor(Key, Iv);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(Plain, 0, Plain.Length);
			cryptoStream.FlushFinalBlock();
			cryptoStream.Close();
			memoryStream.Close();
			return memoryStream.ToArray();
		}

		public static string Encrypt(string Key, string Iv, CipherMode Mode, PaddingMode Padding, string Plain, ByteEncodeMethod ByteEncode, Encoding TextEncode)
		{
			byte[] bytes = TextEncode.GetBytes(Plain);
			byte[] key = ByteArrayEncoder.Decode(Key, ByteEncode);
			byte[] iv = ByteArrayEncoder.Decode(Iv, ByteEncode);
			byte[] byteArray = AesWrapper.Encrypt(key, iv, Mode, Padding, bytes);
			return ByteArrayEncoder.Encode(byteArray, ByteEncode);
		}

		public static string Encrypt(string Key, string Iv, string Plain)
		{
			return AesWrapper.Encrypt(Key, Iv, CipherMode.CBC, PaddingMode.PKCS7, Plain, ByteEncodeMethod.Base64, Encoding.UTF8);
		}

		public static string Encrypt(string Key, string Plain)
		{
			return AesWrapper.Encrypt(Key, string.Empty, CipherMode.ECB, PaddingMode.PKCS7, Plain, ByteEncodeMethod.Base64, Encoding.UTF8);
		}

		public static byte[] Decrypt(byte[] Key, byte[] Iv, CipherMode Mode, PaddingMode Padding, byte[] Encrypted)
		{
			ICryptoTransform transform = new RijndaelManaged
			{
				Mode = Mode,
				Padding = Padding
			}.CreateDecryptor(Key, Iv);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(Encrypted, 0, Encrypted.Length);
			cryptoStream.FlushFinalBlock();
			cryptoStream.Close();
			memoryStream.Close();
			return memoryStream.ToArray();
		}

		public static string Decrypt(string Key, string Iv, CipherMode Mode, PaddingMode Padding, string Encrypted, ByteEncodeMethod ByteEncode, Encoding TextEncode)
		{
			byte[] encrypted = ByteArrayEncoder.Decode(Encrypted, ByteEncode);
			byte[] key = ByteArrayEncoder.Decode(Key, ByteEncode);
			byte[] iv = ByteArrayEncoder.Decode(Iv, ByteEncode);
			byte[] bytes = AesWrapper.Decrypt(key, iv, Mode, Padding, encrypted);
			return TextEncode.GetString(bytes);
		}

		public static string Decrypt(string Key, string Iv, string Encrypted)
		{
			return AesWrapper.Decrypt(Key, Iv, CipherMode.CBC, PaddingMode.PKCS7, Encrypted, ByteEncodeMethod.Base64, Encoding.UTF8);
		}

		public static string Decrypt(string Key, string Encrypted)
		{
			return AesWrapper.Decrypt(Key, string.Empty, CipherMode.ECB, PaddingMode.PKCS7, Encrypted, ByteEncodeMethod.Base64, Encoding.UTF8);
		}
	}
}
