using System;
using System.Security.Cryptography;
using System.Text;

namespace Nexon.Com.Encryption
{
	public class RSASimpleWrapper
	{
		public static RSAParameters GenerateKeyPair()
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
			{
				Flags = CspProviderFlags.UseMachineKeyStore
			});
			rsacryptoServiceProvider.PersistKeyInCsp = false;
			RSAParameters result = rsacryptoServiceProvider.ExportParameters(true);
			rsacryptoServiceProvider.Clear();
			return result;
		}

		public static RSAKeyEncoded KeyEncode(RSAParameters key, ByteEncodeMethod byteEncodeMethod)
		{
			RSAKeyEncoded rsakeyEncoded = new RSAKeyEncoded(byteEncodeMethod);
			rsakeyEncoded.FromRSAParameters(key);
			return rsakeyEncoded;
		}

		public static RSAParameters KeyDecode(RSAKeyEncoded encoded, ByteEncodeMethod byteEncodeMethod)
		{
			return encoded.ToRSAParameters(byteEncodeMethod);
		}

		public static string Encrypt(RSAKeyEncoded encodedKey, string plainText, ByteEncodeMethod ByteEncode, Encoding TextEncode)
		{
			RSAParameters key = encodedKey.ToRSAParameters();
			byte[] bytes = TextEncode.GetBytes(plainText);
			byte[] byteArray = RSASimpleWrapper.Encrypt(key, bytes);
			return ByteArrayEncoder.Encode(byteArray, ByteEncode);
		}

		public static byte[] Encrypt(RSAParameters key, byte[] plainText)
		{
			if (key.Modulus == null || key.Exponent == null)
			{
				throw new InvalidParameterException("key", key);
			}
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
			{
				Flags = CspProviderFlags.UseMachineKeyStore
			});
			rsacryptoServiceProvider.PersistKeyInCsp = false;
			rsacryptoServiceProvider.ImportParameters(key);
			byte[] result = rsacryptoServiceProvider.Encrypt(plainText, false);
			rsacryptoServiceProvider.Clear();
			return result;
		}

		public static string Decrypt(RSAKeyEncoded encodedKey, string encryptedText, ByteEncodeMethod ByteEncode, Encoding TextEncode)
		{
			RSAParameters key = encodedKey.ToRSAParameters();
			byte[] encryptedText2 = ByteArrayEncoder.Decode(encryptedText, ByteEncode);
			byte[] bytes = RSASimpleWrapper.Decrypt(key, encryptedText2);
			return TextEncode.GetString(bytes);
		}

		public static byte[] Decrypt(RSAParameters key, byte[] encryptedText)
		{
			if (key.D == null || key.DP == null || key.DQ == null || key.Exponent == null || key.InverseQ == null || key.Modulus == null || key.P == null || key.Q == null)
			{
				throw new InvalidParameterException("key", "private key is not showable");
			}
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
			{
				Flags = CspProviderFlags.UseMachineKeyStore
			});
			rsacryptoServiceProvider.PersistKeyInCsp = false;
			rsacryptoServiceProvider.ImportParameters(key);
			byte[] result = rsacryptoServiceProvider.Decrypt(encryptedText, false);
			rsacryptoServiceProvider.Clear();
			return result;
		}

		public static RSAParameters GetPublicKey(RSAParameters privateKey)
		{
			return new RSAParameters
			{
				Exponent = privateKey.Exponent,
				Modulus = privateKey.Modulus
			};
		}
	}
}
