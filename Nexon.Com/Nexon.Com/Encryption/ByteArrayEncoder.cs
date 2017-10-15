using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Nexon.Com.Encryption
{
	public class ByteArrayEncoder
	{
		public static string EncodeHex(byte[] ByteArray)
		{
			SoapHexBinary soapHexBinary = new SoapHexBinary(ByteArray);
			return soapHexBinary.ToString();
		}

		public static byte[] DecodeHex(string Encoded)
		{
			SoapHexBinary soapHexBinary = SoapHexBinary.Parse(Encoded);
			return soapHexBinary.Value;
		}

		public static string EncodeBase64(byte[] ByteArray)
		{
			return Convert.ToBase64String(ByteArray);
		}

		public static byte[] DecodeBase64(string Encoded)
		{
			return Convert.FromBase64String(Encoded);
		}

		public static string EncodeBase64UrlSafe(byte[] ByteArray)
		{
			string text = Convert.ToBase64String(ByteArray);
			return text.Replace("=", string.Empty).Replace('+', '-').Replace('/', '_');
		}

		public static byte[] DecodeBase64UrlSafe(string Encoded)
		{
			Encoded = Encoded.PadRight(Encoded.Length + (4 - Encoded.Length % 4) % 4, '=');
			return Convert.FromBase64String(Encoded.Replace('_', '/').Replace('-', '+'));
		}

		public static string Encode(byte[] ByteArray, ByteEncodeMethod Method)
		{
			string result = string.Empty;
			switch (Method)
			{
			case ByteEncodeMethod.Hex:
				result = ByteArrayEncoder.EncodeHex(ByteArray);
				break;
			case ByteEncodeMethod.Base64:
				result = ByteArrayEncoder.EncodeBase64(ByteArray);
				break;
			case ByteEncodeMethod.Base64UrlSafe:
				result = ByteArrayEncoder.EncodeBase64UrlSafe(ByteArray);
				break;
			default:
				result = string.Empty;
				break;
			}
			return result;
		}

		public static byte[] Decode(string Encoded, ByteEncodeMethod Method)
		{
			byte[] result;
			switch (Method)
			{
			case ByteEncodeMethod.Hex:
				result = ByteArrayEncoder.DecodeHex(Encoded);
				break;
			case ByteEncodeMethod.Base64:
				result = ByteArrayEncoder.DecodeBase64(Encoded);
				break;
			case ByteEncodeMethod.Base64UrlSafe:
				result = ByteArrayEncoder.DecodeBase64UrlSafe(Encoded);
				break;
			default:
				result = null;
				break;
			}
			return result;
		}
	}
}
