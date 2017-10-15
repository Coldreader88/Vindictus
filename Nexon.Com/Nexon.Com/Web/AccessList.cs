using System;
using System.Collections;

namespace Nexon.Com.Web
{
	internal sealed class AccessList
	{
		internal string strDate { get; private set; }

		internal AccessList(string strDate)
		{
			this.strDate = strDate;
			this.htAccessInfo = new Hashtable();
		}

		internal bool IsDoS(int maxAccessCountPerSecond, int secondCheckDuration, string strIP)
		{
			if (this.htAccessInfo.Contains(strIP))
			{
				AccessInfo accessInfo = this.htAccessInfo[strIP] as AccessInfo;
				return !accessInfo.IsAccessable(maxAccessCountPerSecond, secondCheckDuration);
			}
			AccessInfo value = new AccessInfo(strIP);
			this.htAccessInfo.Add(strIP, value);
			return false;
		}

		private Hashtable htAccessInfo;
	}
}
