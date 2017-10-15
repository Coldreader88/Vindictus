using System;
using System.Text;
using System.Web;
using System.Web.Caching;
using Nexon.Com.Log;

namespace Nexon.Com.Encryption
{
	public class RSAWrapper
	{
		public static void Init()
		{
			RSACore.Init();
		}

		public static void Release()
		{
			RSACore.Release();
		}

		public string GetEncryptExponent()
		{
			return RSACore.GetEncryptExponent();
		}

		public string GetModulus()
		{
			return RSACore.GetModulus();
		}

		public string[] Decrypt(string data, bool checkHash)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string[] result;
			try
			{
				try
				{
					StringBuilder stringBuilder = new StringBuilder(256);
					string[] array = data.Split(new char[]
					{
						' '
					});
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(RSACore.Decrypt(array[i]));
					}
					text = stringBuilder.ToString();
				}
				catch (Exception ex)
				{
					throw new Exception(string.Format("decrypt failed ({0})", ex.Message));
				}
				string[] array2 = text.Split(new char[]
				{
					'\\'
				});
				string[] array3;
				if (checkHash)
				{
					if (array2.Length < 2)
					{
						throw new Exception("hash key invalid");
					}
					text2 = array2[0];
					array3 = new string[array2.Length - 1];
					if (this.CheckHashKey(text2))
					{
						for (int j = 0; j < array2.Length - 1; j++)
						{
							array3[j] = this.Convert(array2[j + 1]);
						}
					}
					else
					{
						for (int k = 0; k < array2.Length - 1; k++)
						{
							array3[k] = string.Empty;
						}
					}
				}
				else
				{
					array3 = new string[array2.Length];
					for (int l = 0; l < array2.Length; l++)
					{
						array3[l] = this.Convert(array2[l]);
					}
				}
				result = array3;
			}
			catch (Exception ex2)
			{
				int num;
				DateTime dateTime;
				ErrorLog.CreateErrorLog(ServiceCode.framework, 70000, null, string.Format("RSAWrapper : {0}", ex2.Message), string.Format("Message={0} & ClientIP={1} & EncData={2} & HashKey={3} & StackTrace={4}", new object[]
				{
					ex2.Message,
					(HttpContext.Current != null) ? HttpContext.Current.Request.UserHostAddress : string.Empty,
					data,
					text2,
					ex2.StackTrace
				}), out num, out dateTime);
				throw ex2;
			}
			return result;
		}

		public string Convert(string data)
		{
			return RSACore.ConvertUTF8(data);
		}

		public virtual string CreateHashKey()
		{
			string arg = "Nexon.Com.Encryption.RSAWrapper";
			DateTime now = DateTime.Now;
			DateTime dateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute / 10 * 10, 0);
			string text = HttpRuntime.Cache[string.Format("{0}:{1}", arg, dateTime.ToString("yyyyMMddHHmmss"))] as string;
			if (text == null)
			{
				string text2 = dateTime.AddMinutes(-10.0).ToString("yyyyMMddHHmmss");
				string text3 = dateTime.ToString("yyyyMMddHHmmss");
				string text4 = dateTime.AddMinutes(10.0).ToString("yyyyMMddHHmmss");
				byte[] bytes = Encoding.Default.GetBytes(text2);
				byte[] bytes2 = Encoding.Default.GetBytes(text3);
				byte[] bytes3 = Encoding.Default.GetBytes(text4);
				string value = RSACore.ComputeHashString(bytes);
				string text5 = RSACore.ComputeHashString(bytes2);
				string value2 = RSACore.ComputeHashString(bytes3);
				HttpRuntime.Cache.Insert(string.Format("{0}:{1}", arg, text2), value, null, now.AddMinutes(10.0), Cache.NoSlidingExpiration);
				HttpRuntime.Cache.Insert(string.Format("{0}:{1}", arg, text3), text5, null, now.AddMinutes(10.0), Cache.NoSlidingExpiration);
				HttpRuntime.Cache.Insert(string.Format("{0}:{1}", arg, text4), value2, null, now.AddMinutes(10.0), Cache.NoSlidingExpiration);
				return text5;
			}
			return text;
		}

		public virtual bool CheckHashKey(string hash)
		{
			string arg = "Nexon.Com.Encryption.RSAWrapper";
			DateTime now = DateTime.Now;
			DateTime dateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute / 10 * 10, 0);
			string text = dateTime.AddMinutes(-10.0).ToString("yyyyMMddHHmmss");
			string text2 = dateTime.ToString("yyyyMMddHHmmss");
			string text3 = dateTime.AddMinutes(10.0).ToString("yyyyMMddHHmmss");
			string text4 = HttpRuntime.Cache[string.Format("{0}:{1}", arg, text2)] as string;
			if (text4 != null && text4 == hash)
			{
				return true;
			}
			text4 = (HttpRuntime.Cache[string.Format("{0}:{1}", arg, text)] as string);
			if (text4 != null && text4 == hash)
			{
				return true;
			}
			text4 = (HttpRuntime.Cache[string.Format("{0}:{1}", arg, text3)] as string);
			if (text4 != null && text4 == hash)
			{
				return true;
			}
			byte[] bytes = Encoding.Default.GetBytes(text);
			byte[] bytes2 = Encoding.Default.GetBytes(text2);
			byte[] bytes3 = Encoding.Default.GetBytes(text3);
			string text5 = RSACore.ComputeHashString(bytes);
			string text6 = RSACore.ComputeHashString(bytes2);
			string text7 = RSACore.ComputeHashString(bytes3);
			HttpRuntime.Cache.Insert(string.Format("{0}:{1}", arg, text), text5, null, now.AddMinutes(10.0), Cache.NoSlidingExpiration);
			HttpRuntime.Cache.Insert(string.Format("{0}:{1}", arg, text2), text6, null, now.AddMinutes(10.0), Cache.NoSlidingExpiration);
			HttpRuntime.Cache.Insert(string.Format("{0}:{1}", arg, text3), text7, null, now.AddMinutes(10.0), Cache.NoSlidingExpiration);
			return hash == text6 || hash == text5 || hash == text7;
		}
	}
}
