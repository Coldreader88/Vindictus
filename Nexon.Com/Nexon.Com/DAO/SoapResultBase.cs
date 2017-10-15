using System;

namespace Nexon.Com.DAO
{
	public abstract class SoapResultBase
	{
		public SoapResultBase()
		{
		}

		public int SoapErrorCode { get; internal set; }

		public string SoapErrorMessage { get; internal set; }
	}
}
