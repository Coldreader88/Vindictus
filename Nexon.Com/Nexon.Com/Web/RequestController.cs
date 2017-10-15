using System;
using System.Collections.Generic;

namespace Nexon.Com.Web
{
	public class RequestController
	{
		public RequestController(int maxAccessCountPerSecond, int secondCheckDuration) : this(maxAccessCountPerSecond, secondCheckDuration, 0)
		{
		}

		public RequestController(int maxAccessCountPerSecond, int secondCheckDuration, int blockMinutes)
		{
			this.n4MaxAccessCountPerSecond = maxAccessCountPerSecond;
			this.n4SecondCheckDuration = secondCheckDuration;
			this.n4BlockMinutes = blockMinutes;
		}

		public int n4MaxAccessCountPerSecond { get; private set; }

		public int n4SecondCheckDuration { get; private set; }

		public int n4BlockMinutes { get; private set; }

		public bool IsBlock(string strIP)
		{
			return this.n4BlockMinutes > 0 && this.blockInfo.Exists((BlockInfo pSelect) => pSelect.strIP == strIP && pSelect.dtBlockEndDate > DateTime.Now);
		}

		public bool AddRequest(string strIP)
		{
			string text = DateTime.Now.ToString("yyyyMMdd-HH");
			bool flag;
			if (this.accessList != null && this.accessList.strDate == text)
			{
				flag = this.accessList.IsDoS(this.n4MaxAccessCountPerSecond, this.n4SecondCheckDuration, strIP);
			}
			else
			{
				this.accessList = new AccessList(text);
				flag = this.accessList.IsDoS(this.n4MaxAccessCountPerSecond, this.n4SecondCheckDuration, strIP);
			}
			if (flag && this.n4BlockMinutes > 0)
			{
				this.blockInfo.Add(new BlockInfo(strIP, DateTime.Now.AddMinutes((double)this.n4BlockMinutes)));
			}
			return flag;
		}

		public bool IsDoS(string strIP)
		{
			return this.IsBlock(strIP) || this.AddRequest(strIP);
		}

		private AccessList accessList;

		private List<BlockInfo> blockInfo = new List<BlockInfo>();
	}
}
