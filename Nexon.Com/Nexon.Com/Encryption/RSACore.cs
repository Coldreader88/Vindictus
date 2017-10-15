using System;
using System.Configuration;
using System.Security.Cryptography;

namespace Nexon.Com.Encryption
{
	internal class RSACore
	{
		internal static void Init()
		{
			if (RSACore._isInitialized)
			{
				throw new Exception("RSAWrapper already initialized");
			}
			try
			{
				string file = ConfigurationManager.AppSettings["RSAPrivateKeyFilePath"];
				RSACore._rsa = new RSACrypto();
				RSACore._rsa.InitCrypto(file);
				RSACore._rsaParams = RSACore._rsa.ExportParameters(true);
				RSACore._strEncryptExponent = RSAStringUtil.BytesToHexString(RSACore._rsaParams.Exponent);
				RSACore._strModulus = RSAStringUtil.BytesToHexString(RSACore._rsaParams.Modulus);
				RSACore._strDecryptExponent = RSAStringUtil.BytesToHexString(RSACore._rsaParams.D);
				RSACore._isInitialized = true;
			}
			catch
			{
				throw;
			}
		}

		internal static void Release()
		{
			if (!RSACore._isInitialized)
			{
				throw new Exception("RSAWrapper not initialized");
			}
			try
			{
				RSACore._rsa.ReleaseCrypto();
				RSACore._rsa = null;
				RSACore._isInitialized = false;
			}
			catch
			{
				throw;
			}
		}

		internal static string GetEncryptExponent()
		{
			if (!RSACore._isInitialized)
			{
				throw new Exception("RSAWrapper not initialized");
			}
			return RSACore._strEncryptExponent;
		}

		internal static string GetModulus()
		{
			if (!RSACore._isInitialized)
			{
				throw new Exception("RSAWrapper not initialized");
			}
			return RSACore._strModulus;
		}

		internal string GetDecryptExponent()
		{
			if (!RSACore._isInitialized)
			{
				throw new Exception("RSAWrapper not initialized");
			}
			return RSACore._strDecryptExponent;
		}

		internal static string Decrypt(string data)
		{
			if (!RSACore._isInitialized)
			{
				throw new Exception("RSAWrapper not initialized");
			}
			byte[] data2 = RSAStringUtil.HexStringToBytes(data);
			byte[] data3 = RSACore._rsa.Decrypt(data2);
			return RSAStringUtil.ASCIIBytesToString(data3);
		}

		internal static string ComputeHashString(byte[] data)
		{
			if (!RSACore._isInitialized)
			{
				throw new Exception("RSAWrapper not initialized");
			}
			byte[] d = RSACore._rsaParams.D;
			byte[] array = new byte[data.Length + d.Length];
			Buffer.BlockCopy(data, 0, array, 0, data.Length);
			Buffer.BlockCopy(d, 0, array, data.Length, d.Length);
			SHA1Managed sha1Managed = new SHA1Managed();
			return RSAStringUtil.ToBase64(sha1Managed.ComputeHash(array));
		}

		internal static string ConvertASCII(string data)
		{
			return RSAStringUtil.ASCIIBytesToString(RSAStringUtil.FromBase64(data));
		}

		internal static string ConvertUTF8(string data)
		{
			return RSAStringUtil.UTF8BytesToString(RSAStringUtil.FromBase64(data));
		}

		internal static string ConvertUTF16(string data)
		{
			return RSAStringUtil.UTF16BytesToString(RSAStringUtil.FromBase64(data));
		}

		private static RSACrypto _rsa;

		private static RSAParameters _rsaParams;

		private static bool _isInitialized;

		private static string _strEncryptExponent;

		private static string _strModulus;

		private static string _strDecryptExponent;
	}
}
