using System;

namespace Nexon.Com.Web
{
	internal sealed class BlockInfo
	{
		internal BlockInfo(string strIP, DateTime dtBlockEndDate)
		{
			this.strIP = strIP;
			this.dtBlockEndDate = dtBlockEndDate;
		}

		internal string strIP;

		internal DateTime dtBlockEndDate;
	}
}
