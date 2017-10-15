using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace Nexon.Com.Encryption
{
	public class RSAKeyEncoded
	{
		public RSAKeyEncoded()
		{
			this.D = string.Empty;
			this.DP = string.Empty;
			this.DQ = string.Empty;
			this.Exponent = string.Empty;
			this.InverseQ = string.Empty;
			this.Modulus = string.Empty;
			this.P = string.Empty;
			this.Q = string.Empty;
			this._encodeMethod = ByteEncodeMethod.Hex;
		}

		public RSAKeyEncoded(ByteEncodeMethod encodeMethod)
		{
			this.D = string.Empty;
			this.DP = string.Empty;
			this.DQ = string.Empty;
			this.Exponent = string.Empty;
			this.InverseQ = string.Empty;
			this.Modulus = string.Empty;
			this.P = string.Empty;
			this.Q = string.Empty;
			this._encodeMethod = encodeMethod;
		}

		public string D { get; set; }

		public string DP { get; set; }

		public string DQ { get; set; }

		public string Exponent { get; set; }

		public string InverseQ { get; set; }

		public string Modulus { get; set; }

		public string P { get; set; }

		public string Q { get; set; }

		public string SerializeToJson()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties())
			{
				string value = propertyInfo.GetValue(this, null) as string;
				if (!string.IsNullOrEmpty(value))
				{
					dictionary.Add(propertyInfo.Name, value);
				}
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(dictionary);
		}

		public void DeserializeFromJson(string json)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			Dictionary<string, string> dictionary = javaScriptSerializer.Deserialize<Dictionary<string, string>>(json);
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties())
			{
				if (dictionary.Keys.Contains(propertyInfo.Name))
				{
					propertyInfo.SetValue(this, dictionary[propertyInfo.Name], null);
				}
				else
				{
					propertyInfo.SetValue(this, string.Empty, null);
				}
			}
		}

		public void FromRSAParameters(RSAParameters param)
		{
			this.FromRSAParameters(param, this._encodeMethod);
		}

		public void FromRSAParameters(RSAParameters param, ByteEncodeMethod byteEncodeMethod)
		{
			if (param.D != null)
			{
				this.D = ByteArrayEncoder.Encode(param.D, byteEncodeMethod);
			}
			if (param.DP != null)
			{
				this.DP = ByteArrayEncoder.Encode(param.DP, byteEncodeMethod);
			}
			if (param.DQ != null)
			{
				this.DQ = ByteArrayEncoder.Encode(param.DQ, byteEncodeMethod);
			}
			if (param.Exponent != null)
			{
				this.Exponent = ByteArrayEncoder.Encode(param.Exponent, byteEncodeMethod);
			}
			if (param.InverseQ != null)
			{
				this.InverseQ = ByteArrayEncoder.Encode(param.InverseQ, byteEncodeMethod);
			}
			if (param.Modulus != null)
			{
				this.Modulus = ByteArrayEncoder.Encode(param.Modulus, byteEncodeMethod);
			}
			if (param.P != null)
			{
				this.P = ByteArrayEncoder.Encode(param.P, byteEncodeMethod);
			}
			if (param.Q != null)
			{
				this.Q = ByteArrayEncoder.Encode(param.Q, byteEncodeMethod);
			}
		}

		public RSAParameters ToRSAParameters()
		{
			return this.ToRSAParameters(this._encodeMethod);
		}

		public RSAParameters ToRSAParameters(ByteEncodeMethod byteEncodeMethod)
		{
			RSAParameters result = default(RSAParameters);
			if (!string.IsNullOrEmpty(this.D))
			{
				result.D = ByteArrayEncoder.Decode(this.D, byteEncodeMethod);
			}
			if (!string.IsNullOrEmpty(this.DP))
			{
				result.DP = ByteArrayEncoder.Decode(this.DP, byteEncodeMethod);
			}
			if (!string.IsNullOrEmpty(this.DQ))
			{
				result.DQ = ByteArrayEncoder.Decode(this.DQ, byteEncodeMethod);
			}
			if (!string.IsNullOrEmpty(this.Exponent))
			{
				result.Exponent = ByteArrayEncoder.Decode(this.Exponent, byteEncodeMethod);
			}
			if (!string.IsNullOrEmpty(this.InverseQ))
			{
				result.InverseQ = ByteArrayEncoder.Decode(this.InverseQ, byteEncodeMethod);
			}
			if (!string.IsNullOrEmpty(this.Modulus))
			{
				result.Modulus = ByteArrayEncoder.Decode(this.Modulus, byteEncodeMethod);
			}
			if (!string.IsNullOrEmpty(this.P))
			{
				result.P = ByteArrayEncoder.Decode(this.P, byteEncodeMethod);
			}
			if (!string.IsNullOrEmpty(this.Q))
			{
				result.Q = ByteArrayEncoder.Decode(this.Q, byteEncodeMethod);
			}
			return result;
		}

		private ByteEncodeMethod _encodeMethod;
	}
}
