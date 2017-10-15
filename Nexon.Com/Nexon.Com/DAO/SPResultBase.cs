using System;

namespace Nexon.Com.DAO
{
	public abstract class SPResultBase
	{
		public SPResultBase()
		{
		}

		public int SPReturnValue { get; set; }

		public int SPErrorCode { get; set; }

		public string SPErrorMessage { get; set; }
	}
}
