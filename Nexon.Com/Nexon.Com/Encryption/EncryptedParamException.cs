using System;

namespace Nexon.Com.Encryption
{
	public class EncryptedParamException : Exception
	{
		public EncryptedParamException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public EncryptedParamException(string message) : base(message, null)
		{
		}
	}
}
