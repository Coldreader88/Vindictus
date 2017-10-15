using System;
using System.Text;

namespace Nexon.Com.Encryption
{
	internal class ParamEncoder
	{
		public ParamEncoder()
		{
			this.Clear();
		}

		public ParamEncoder(ushort privateKey)
		{
			this.Clear();
			this.m_uPrivateKey = privateKey;
		}

		public void Clear()
		{
			this.m_nBufferSize = 0;
			this.m_nHashValue = 0L;
			Array.Clear(this.m_aBuffer, 0, this.m_aBuffer.Length);
		}

		public bool SetToken(string sToken)
		{
			if (sToken != null)
			{
				this.Clear();
				return this.Decrypt(sToken);
			}
			return false;
		}

		internal string GetToken()
		{
			return this.Encrypt();
		}

		public bool AddParam(string sName, string sValue)
		{
			if (sName != null || sValue != null)
			{
				byte[] bytes = Encoding.ASCII.GetBytes(sValue);
				return this.AddParamImpl(sName, 1, bytes);
			}
			return false;
		}

		public bool AddParam(string sName, string sValue, bool bUnicode)
		{
			if (!bUnicode)
			{
				return this.AddParam(sName, sValue);
			}
			if (sName != null || sValue != null)
			{
				byte[] bytes = Encoding.Unicode.GetBytes(sValue);
				return this.AddParamImpl(sName, 2, bytes);
			}
			return false;
		}

		public bool AddParam(string sName, byte[] aValue, int nLen)
		{
			return (sName != null || aValue != null) && this.AddParamImpl(sName, 3, aValue, nLen);
		}

		public bool AddParam(string sName, byte[] aValue)
		{
			return this.AddParam(sName, aValue, 0);
		}

		public bool AddParam(string sName, sbyte nValue)
		{
			return sName != null && this.AddParamImpl(sName, 4, new byte[]
			{
				(byte)nValue
			});
		}

		public bool AddParam(string sName, byte nValue)
		{
			return sName != null && this.AddParamImpl(sName, 5, new byte[]
			{
				nValue
			});
		}

		public bool AddParam(string sName, short nValue)
		{
			return sName != null && this.AddParamImpl(sName, 6, new byte[]
			{
				(byte)(nValue >> 8 & 255),
				(byte)(nValue & 255)
			});
		}

		public bool AddParam(string sName, ushort nValue)
		{
			return sName != null && this.AddParamImpl(sName, 7, new byte[]
			{
				(byte)(nValue >> 8 & 255),
				(byte)(nValue & 255)
			});
		}

		public bool AddParam(string sName, int nValue)
		{
			return sName != null && this.AddParamImpl(sName, 8, new byte[]
			{
				(byte)(nValue >> 24 & 255),
				(byte)(nValue >> 16 & 255),
				(byte)(nValue >> 8 & 255),
				(byte)(nValue & 255)
			});
		}

		public bool AddParam(string sName, uint nValue)
		{
			return sName != null && this.AddParamImpl(sName, 9, new byte[]
			{
				(byte)(nValue >> 24 & 255u),
				(byte)(nValue >> 16 & 255u),
				(byte)(nValue >> 8 & 255u),
				(byte)(nValue & 255u)
			});
		}

		public bool AddParam(string sName, long nValue)
		{
			return sName != null && this.AddParamImpl(sName, 10, new byte[]
			{
				(byte)(nValue >> 56 & 255L),
				(byte)(nValue >> 48 & 255L),
				(byte)(nValue >> 40 & 255L),
				(byte)(nValue >> 32 & 255L),
				(byte)(nValue >> 24 & 255L),
				(byte)(nValue >> 16 & 255L),
				(byte)(nValue >> 8 & 255L),
				(byte)(nValue & 255L)
			});
		}

		public bool AddParam(string sName, ulong nValue)
		{
			return sName != null && this.AddParamImpl(sName, 11, new byte[]
			{
				(byte)(nValue >> 56 & 255UL),
				(byte)(nValue >> 48 & 255UL),
				(byte)(nValue >> 40 & 255UL),
				(byte)(nValue >> 32 & 255UL),
				(byte)(nValue >> 24 & 255UL),
				(byte)(nValue >> 16 & 255UL),
				(byte)(nValue >> 8 & 255UL),
				(byte)(nValue & 255UL)
			});
		}

		public object GetParamValue(string sName)
		{
			if (sName == null)
			{
				return null;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(sName);
			int num = this.GetParamPtr(bytes);
			if (num == -1)
			{
				return null;
			}
			int num2 = this.m_aBuffer[num] >> 4 & 15;
			int num3 = (int)(this.m_aBuffer[num] & 15) << 8 | (int)this.m_aBuffer[num + 1];
			num += bytes.Length + 3;
			num3 -= bytes.Length + 3;
			object result = null;
			switch (num2)
			{
			case 1:
				result = Encoding.Default.GetString(this.m_aBuffer, num, num3);
				break;
			case 2:
				result = Encoding.Unicode.GetString(this.m_aBuffer, num, num3);
				break;
			case 3:
			{
				byte[] array = new byte[num3];
				Array.Copy(this.m_aBuffer, num, array, 0, num3);
				result = array;
				break;
			}
			case 4:
			{
				sbyte b = (sbyte)this.m_aBuffer[num];
				result = b;
				break;
			}
			case 5:
			{
				byte b2 = this.m_aBuffer[num];
				result = b2;
				break;
			}
			case 6:
			{
				short num4 = (short)((int)this.m_aBuffer[num] << 8 | (int)this.m_aBuffer[num + 1]);
				result = num4;
				break;
			}
			case 7:
			{
				ushort num5 = (ushort)((int)this.m_aBuffer[num] << 8 | (int)this.m_aBuffer[num + 1]);
				result = num5;
				break;
			}
			case 8:
			{
				int num6 = (int)this.m_aBuffer[num] << 24 | (int)this.m_aBuffer[num + 1] << 16 | (int)this.m_aBuffer[num + 2] << 8 | (int)this.m_aBuffer[num + 3];
				result = num6;
				break;
			}
			case 9:
			{
				uint num7 = (uint)((int)this.m_aBuffer[num] << 24 | (int)this.m_aBuffer[num + 1] << 16 | (int)this.m_aBuffer[num + 2] << 8 | (int)this.m_aBuffer[num + 3]);
				result = num7;
				break;
			}
			case 10:
			{
				long num8 = (long)((int)this.m_aBuffer[num] << 24 | (int)this.m_aBuffer[num + 1] << 16 | (int)this.m_aBuffer[num + 2] << 8 | (int)this.m_aBuffer[num + 3] | (int)this.m_aBuffer[num + 4] << 24 | (int)this.m_aBuffer[num + 5] << 16 | (int)this.m_aBuffer[num + 6] << 8 | (int)this.m_aBuffer[num + 7]);
				result = num8;
				break;
			}
			case 11:
			{
				ulong num9 = (ulong)((long)((int)this.m_aBuffer[num] << 24 | (int)this.m_aBuffer[num + 1] << 16 | (int)this.m_aBuffer[num + 2] << 8 | (int)this.m_aBuffer[num + 3] | (int)this.m_aBuffer[num + 4] << 24 | (int)this.m_aBuffer[num + 5] << 16 | (int)this.m_aBuffer[num + 6] << 8 | (int)this.m_aBuffer[num + 7]));
				result = num9;
				break;
			}
			}
			return result;
		}

		public bool DelParam(string sName)
		{
			return this.DelParamImpl(Encoding.ASCII.GetBytes(sName));
		}

		private bool AddParamImpl(string isName, byte inType, byte[] iaData, int inDataLength)
		{
			if (isName != null && iaData != null)
			{
				byte[] bytes = Encoding.ASCII.GetBytes(isName);
				return this.AddParamImpl(bytes, inType, iaData, (inDataLength <= 0) ? iaData.Length : inDataLength);
			}
			return false;
		}

		private bool AddParamImpl(string isName, byte inType, byte[] iaData)
		{
			if (isName != null && iaData != null)
			{
				byte[] bytes = Encoding.ASCII.GetBytes(isName);
				return this.AddParamImpl(bytes, inType, iaData, iaData.Length);
			}
			return false;
		}

		private bool AddParamImpl(byte[] iaName, byte inType, byte[] iaData, int inDataLength)
		{
			if (iaName != null && iaData != null)
			{
				int num = iaName.Length + 3 + inDataLength;
				this.DelParamImpl(iaName);
				if (this.m_nBufferSize + num <= 2048)
				{
					this.m_aBuffer[this.m_nBufferSize] = (byte)(((int)inType << 4 & 240) | (num >> 8 & 15));
					this.m_aBuffer[this.m_nBufferSize + 1] = (byte)(num & 255);
					Array.Copy(iaName, 0, this.m_aBuffer, this.m_nBufferSize + 2, iaName.Length);
					this.m_aBuffer[this.m_nBufferSize + 2 + iaName.Length] = 0;
					Array.Copy(iaData, 0, this.m_aBuffer, this.m_nBufferSize + 3 + iaName.Length, inDataLength);
					this.m_nBufferSize += num;
					return true;
				}
			}
			return false;
		}

		private bool DelParamImpl(byte[] iaName)
		{
			if (iaName != null)
			{
				int paramPtr = this.GetParamPtr(iaName);
				if (paramPtr != -1)
				{
					int num = (int)(this.m_aBuffer[paramPtr] & 15) << 8 | (int)this.m_aBuffer[paramPtr + 1];
					if (this.m_nBufferSize > paramPtr + num)
					{
						Array.Copy(this.m_aBuffer, paramPtr + num, this.m_aBuffer, paramPtr, this.m_nBufferSize - paramPtr - num);
					}
					this.m_nBufferSize -= num;
					return true;
				}
			}
			return false;
		}

		private int GetParamPtr(byte[] iaName)
		{
			if (iaName != null)
			{
				int num;
				for (int i = 0; i < this.m_nBufferSize; i += num)
				{
					num = ((int)(this.m_aBuffer[i] & 15) << 8 | (int)this.m_aBuffer[i + 1]);
					int num2 = 0;
					while (num2 < iaName.Length && this.m_aBuffer[i + 2 + num2] != 0 && this.m_aBuffer[i + 2 + num2] == iaName[num2])
					{
						num2++;
					}
					if (num2 == iaName.Length && this.m_aBuffer[i + 2 + num2] == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		private string Encrypt()
		{
			byte[] array = new byte[4096];
			Random random = new Random((int)DateTime.Now.Ticks);
			int i = 0;
			int num = 0;
			Encoding.ASCII.GetString(this.m_aBuffer);
			int num2 = (this.m_nBufferSize + 1) / 2 * 2;
			this.m_nHashValue = ParamEncoder.GetHashValue(this.m_aBuffer, 0, num2);
			int num3 = random.Next(0, 65535);
			array[0] = (byte)(num3 >> 8 & 255);
			array[1] = (byte)(num3 & 255);
			array[2] = (byte)(this.m_nBufferSize >> 8 & 255);
			array[3] = (byte)(this.m_nBufferSize & 255);
			Array.Copy(this.m_aBuffer, 0, array, 4, num2);
			array[num2 + 4] = (byte)(this.m_nHashValue >> 56 & 255L);
			array[num2 + 5] = (byte)(this.m_nHashValue >> 48 & 255L);
			array[num2 + 6] = (byte)(this.m_nHashValue >> 40 & 255L);
			array[num2 + 7] = (byte)(this.m_nHashValue >> 32 & 255L);
			array[num2 + 8] = (byte)(this.m_nHashValue >> 24 & 255L);
			array[num2 + 9] = (byte)(this.m_nHashValue >> 16 & 255L);
			array[num2 + 10] = (byte)(this.m_nHashValue >> 8 & 255L);
			array[num2 + 11] = (byte)(this.m_nHashValue & 255L);
			array[num2 + 12] = 0;
			for (int j = 2; j < num2 + 12; j += 2)
			{
				int num4 = ((int)array[j - 2] << 8 | (int)array[j - 1]) + 17 & 65535;
				int num5 = (int)array[j] << 8 | (int)array[j + 1];
				num5 ^= (int)ScrambleTable.Convert16[num4];
				array[j] = (byte)(ScrambleTable.Convert16[num5] >> 8 & 255);
				array[j + 1] = (byte)(ScrambleTable.Convert16[num5] & 255);
			}
			array[0] = (byte)((int)array[0] ^ (this.m_uPrivateKey >> 8 & 255));
			array[1] = (byte)((ushort)array[1] ^ (this.m_uPrivateKey & 255));
			int num6 = num2 + 12 << 3;
			char[] array2 = new char[((num2 + 12) * 8 + 5) / 6];
			while (i < num6)
			{
				int num7 = i >> 3;
				int num8 = i & 7;
				int num9 = 6 - Math.Min(6, 8 - num8);
				int num10 = (int)array[num7] << num8 & 255;
				num10 >>= 2;
				if (num9 > 0)
				{
					num10 |= array[num7 + 1] >> 8 - num9;
				}
				if (num10 > 64)
				{
					return null;
				}
				array2[num] = ScrambleTable.ConvertText[num10];
				num++;
				i += 6;
			}
			return new string(array2);
		}

		private bool Decrypt(string sToken)
		{
			byte[] array = new byte[2048];
			this.Clear();
			int num = sToken.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				int num3 = num2 >> 3;
				int num4 = num2 & 7;
				int num5 = (int)ScrambleTable.ReverseText[(int)sToken[i]];
				int num6 = 6 - Math.Min(6, 8 - num4);
				byte[] array2 = array;
				int num7 = num3;
				array2[num7] |= (byte)(num5 << 2 >> num4);
				if (num6 > 0)
				{
					byte[] array3 = array;
					int num8 = num3 + 1;
					array3[num8] |= (byte)(num5 << 8 - num6 & 255);
				}
				num2 += 6;
			}
			num = num2 + 1 >> 3;
			int num9 = ((int)array[0] << 8 | (int)array[1]) ^ (int)this.m_uPrivateKey;
			for (int i = 2; i < num; i += 2)
			{
				int num10 = (int)array[i] << 8 | (int)array[i + 1];
				array[i] = (byte)(ScrambleTable.Reverse16[num10] >> 8 & 255);
				array[i + 1] = (byte)(ScrambleTable.Reverse16[num10] & 255);
				int num11 = (int)array[i] << 8 | (int)array[i + 1];
				array[i] = (byte)((num11 ^ (int)ScrambleTable.Convert16[num9 + 17 & 65535]) >> 8 & 255);
				array[i + 1] = (byte)((num11 ^ (int)ScrambleTable.Convert16[num9 + 17 & 65535]) & 255);
				num9 = num10;
			}
			num = ((int)array[2] << 8 | (int)array[3]);
			int num12 = (num + 1) / 2 * 2;
			this.m_nHashValue = (long)((ulong)array[num12 + 4]);
			this.m_nHashValue = (this.m_nHashValue << 8 | (long)((ulong)array[num12 + 5]));
			this.m_nHashValue = (this.m_nHashValue << 8 | (long)((ulong)array[num12 + 6]));
			this.m_nHashValue = (this.m_nHashValue << 8 | (long)((ulong)array[num12 + 7]));
			this.m_nHashValue = (this.m_nHashValue << 8 | (long)((ulong)array[num12 + 8]));
			this.m_nHashValue = (this.m_nHashValue << 8 | (long)((ulong)array[num12 + 9]));
			this.m_nHashValue = (this.m_nHashValue << 8 | (long)((ulong)array[num12 + 10]));
			this.m_nHashValue = (this.m_nHashValue << 8 | (long)((ulong)array[num12 + 11]));
			if (this.m_nHashValue == ParamEncoder.GetHashValue(array, 4, num12))
			{
				Array.Copy(array, 4, this.m_aBuffer, 0, num12);
				this.m_nBufferSize = num;
				return true;
			}
			return false;
		}

		public static long GetHashValue(byte[] iaBuffer, int inStartIndex, int inLength)
		{
			if (inStartIndex < iaBuffer.Length && inStartIndex + inLength < iaBuffer.Length)
			{
				long num = 0L;
				for (int i = inStartIndex; i < inStartIndex + inLength - 1; i += 2)
				{
					num ^= (long)((ulong)ScrambleTable.Convert16[(int)iaBuffer[i] << 8 | (int)iaBuffer[i + 1]]);
					num = (num << 48 | (num >> 16 & 281474976710655L));
				}
				return num;
			}
			return 0L;
		}

		private byte[] m_aBuffer = new byte[2048];

		private int m_nBufferSize;

		private long m_nHashValue;

		private ushort m_uPrivateKey;

		private enum Constant
		{
			MaxPlainBuffer = 2048,
			MaxEncryptedBuffer = 4096
		}

		private enum Type
		{
			AnsiString = 1,
			UnicodeString,
			Binary,
			SInt8,
			UInt8,
			SInt16,
			UInt16,
			SInt32,
			UInt32,
			SInt64,
			UInt64
		}
	}
}
