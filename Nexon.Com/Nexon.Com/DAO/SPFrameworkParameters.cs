using System;

namespace Nexon.Com.DAO
{
	public class SPFrameworkParameters
	{
		public SPFrameworkParameters(int DBIndex, int SPReturnValue, int SPErrorCode, string SPErrorMessage)
		{
			this.DBIndex = DBIndex;
			this.SPReturnValue = SPReturnValue;
			this.SPErrorCode = SPErrorCode;
			this.SPErrorMessage = SPErrorMessage;
		}

		public int DBIndex { get; private set; }

		public int SPReturnValue { get; private set; }

		public int SPErrorCode { get; private set; }

		public string SPErrorMessage { get; private set; }
	}
}
