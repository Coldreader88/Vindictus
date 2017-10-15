using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

namespace Nexon.Enterprise.ServiceFacade
{
	public sealed class ClassSerializer
	{
		public string Path
		{
			get
			{
				return System.IO.Path.GetDirectoryName(this._path);
			}
		}

		internal string Key
		{
			get
			{
				return this._key;
			}
		}

		internal string CheckSumFilePath
		{
			get
			{
				return System.IO.Path.Combine(this.Path, "checksum.xml");
			}
		}

		public ClassSerializer(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException();
			}
			this._key = key;
			this._path = System.IO.Path.Combine(string.IsNullOrEmpty(ConfigurationManager.AppSettings["Path"]) ? "C:\\Program Files\\Common Files\\Nexon\\LiveUpdate" : ConfigurationManager.AppSettings["Path"], key + "\\");
		}

		public Stream GetLiveUpdateStream()
		{
			return new FileStream(System.IO.Path.Combine(this.Path, string.Format("{0}.zip", this.Key)), FileMode.Open, FileAccess.Read);
		}

		public bool CheckLiveUpdate(string hashCode)
		{
			bool result;
			using (FileStream fileStream = File.OpenRead(this.CheckSumFilePath))
			{
				result = hashCode.Equals(BitConverter.ToString(this.md5.ComputeHash(fileStream)));
			}
			return result;
		}

		private const string CHECKSUM_FILE = "checksum.xml";

		private MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

		private string _path;

		private readonly string _key;
	}
}
