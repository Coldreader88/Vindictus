using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Nexon.Com.Log;

namespace Nexon.Com.Encryption
{
	internal class RSACrypto
	{
		public RSAParameters ExportParameters(bool includePrivateParameters)
		{
			return this._sp.ExportParameters(includePrivateParameters);
		}

		public void InitCrypto(string file)
		{
			StreamReader streamReader = null;
			try
			{
				this._sp = new RSACryptoServiceProvider(new CspParameters
				{
					Flags = CspProviderFlags.UseMachineKeyStore
				});
				streamReader = new StreamReader(file);
				string xmlString = streamReader.ReadToEnd();
				this._sp.FromXmlString(xmlString);
			}
			catch (Exception ex)
			{
				if (ex is IOException)
				{
					int num;
					DateTime dateTime;
					ErrorLog.CreateErrorLog(ServiceCode.framework, 70000, null, "RSAWrapper : Invalid Key File Info", string.Format("{0} --> {1}", ex.Message, ex.StackTrace), out num, out dateTime);
				}
				else
				{
					int num;
					DateTime dateTime;
					ErrorLog.CreateErrorLog(ServiceCode.framework, 70000, null, string.Format("RSAWrapper : {0}", ex.Message), string.Format("{0} --> {1}", ex.Message, ex.StackTrace), out num, out dateTime);
				}
				throw;
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
					streamReader = null;
				}
			}
		}

		public void ReleaseCrypto()
		{
			if (this._sp != null)
			{
				this._sp.Clear();
				this._sp = null;
			}
		}

		public byte[] Encrypt(string data)
		{
			ASCIIEncoding asciiencoding = new ASCIIEncoding();
			asciiencoding.GetByteCount(data);
			byte[] bytes = asciiencoding.GetBytes(data);
			return this._sp.Encrypt(bytes, false);
		}

		public byte[] Decrypt(byte[] data)
		{
			return this._sp.Decrypt(data, false);
		}

		private RSACryptoServiceProvider _sp;
	}
}
