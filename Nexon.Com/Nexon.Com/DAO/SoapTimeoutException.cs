using System;

namespace Nexon.Com.DAO
{
	public class SoapTimeoutException : Exception
	{
		public SoapTimeoutException(int retryCount, int durationTime) : base(string.Format("SoapTimeout [{0:00000}]", durationTime), null)
		{
			this.SoapRetryCount = retryCount;
			this.SoapDurationTime = durationTime;
		}

		public int SoapRetryCount { get; set; }

		public int SoapDurationTime { get; set; }
	}
}
