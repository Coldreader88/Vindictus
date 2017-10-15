using System;

namespace Nexon.Com.Encryption
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class EncryptedParamAttribute : Attribute
	{
		public EncryptedParamAttribute(string key)
		{
			this.Key = key;
		}

		public string Key;

		public bool Optional;

		public bool Unicode = true;
	}
}
