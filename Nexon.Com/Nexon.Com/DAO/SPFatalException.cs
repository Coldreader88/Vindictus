using System;

namespace Nexon.Com.DAO
{
	public class SPFatalException : Exception
	{
		public SPFatalException(int errorcode, string errormessage, string SPDump) : base(errormessage)
		{
			this.SPErrorCode = errorcode;
			this.SPErrorMessage = errormessage;
		}

		public SPFatalException(int DBIndex, int errorcode, string errormessage, string SPDump) : base(errormessage)
		{
			this.SPErrorCode = errorcode;
			this.SPErrorMessage = errormessage;
		}

		public int DBIndex { get; set; }

		public int SPErrorCode { get; set; }

		public string SPErrorMessage { get; set; }

		public string SPDump { get; set; }
	}
}
