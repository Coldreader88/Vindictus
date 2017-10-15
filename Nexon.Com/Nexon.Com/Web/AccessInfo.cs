using System;
using Nexon.Com.Log;

namespace Nexon.Com.Web
{
	internal sealed class AccessInfo
	{
		internal AccessInfo(string strIP)
		{
			this.strIP = strIP;
			this.dtLastAccessDate = DateTime.Now;
			this.n4AccessCount = 1;
		}

		internal bool IsAccessable(int maxAccessCountPerSecond, int secondCheckDuration)
		{
			if (this.dtLastAccessDate.AddSeconds((double)secondCheckDuration) > DateTime.Now)
			{
				if (this.n4AccessCount <= maxAccessCountPerSecond)
				{
					this.n4AccessCount++;
				}
			}
			else
			{
				this.dtLastAccessDate = DateTime.Now;
				this.n4AccessCount = 1;
			}
			if (this.n4AccessCount >= maxAccessCountPerSecond)
			{
				if (this.n4AccessCount == maxAccessCountPerSecond)
				{
					int num;
					DateTime dateTime;
					ErrorLog.CreateLightErrorLog(ServiceCode.platform, 1011500, null, "DoS attack detected", string.Format("IP={0}, Count={1} Per {3} Second, Date={2}", new object[]
					{
						this.strIP,
						this.n4AccessCount,
						this.dtLastAccessDate.ToString("yyyyMMdd hhmmss"),
						secondCheckDuration
					}), out num, out dateTime);
				}
				return false;
			}
			return true;
		}

		private string strIP = string.Empty;

		private DateTime dtLastAccessDate;

		internal int n4AccessCount;
	}
}
