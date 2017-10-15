using System;

namespace Nexon.Com.DAO
{
	public class SPLogicalException : Exception
	{
		public SPLogicalException(int errorcode, string errormessage, string SPDump) : base(errormessage)
		{
			this.SPErrorCode = errorcode;
			this.SPErrorMessage = errormessage;
		}

		public int SPErrorCode { get; set; }

		public string SPErrorMessage { get; set; }

		public string SPDump { get; set; }
	}
}
