using System;
using System.Collections.Generic;
using System.Text;

namespace Nexon.Com.Net
{
	public class IPFilter
	{
		public IPFilter()
		{
			this.m_ipList = new List<string>();
			this.m_ipRangeList = new List<IPRange>();
		}

		public void AddIPs(string ips)
		{
			if (ips != null && ips != string.Empty)
			{
				foreach (string text in ips.Split(new char[]
				{
					';'
				}))
				{
					string text2 = text.Trim();
					if (text2 != string.Empty)
					{
						if (text2.IndexOf('~') > -1)
						{
							string[] array2 = text2.Split(new char[]
							{
								'~'
							})[0].Split(new char[]
							{
								'.'
							});
							string[] array3 = text2.Split(new char[]
							{
								'~'
							})[1].Split(new char[]
							{
								'.'
							});
							if (array2.Length == 4 && array3.Length == 4)
							{
								for (int j = Convert.ToInt32(array2[3]); j <= Convert.ToInt32(array3[3]); j++)
								{
									IPRange iprange = new IPRange(string.Format("{0}.{1}.{2}.{3}", new object[]
									{
										array2[0],
										array2[1],
										array2[2],
										j
									}));
									if (iprange != null)
									{
										this.m_ipRangeList.Add(iprange);
									}
								}
							}
						}
						else
						{
							IPRange iprange2 = new IPRange(text2);
							if (iprange2 != null)
							{
								this.m_ipRangeList.Add(iprange2);
							}
						}
					}
				}
			}
		}

		public bool CheckIP(string ip)
		{
			if (this.m_ipList.Contains(ip))
			{
				return true;
			}
			foreach (IPRange iprange in this.m_ipRangeList)
			{
				if (iprange.Contains(ip))
				{
					this.m_ipList.Add(ip);
					return true;
				}
			}
			return false;
		}

		public string TraceIPRange()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IPRange iprange in this.m_ipRangeList)
			{
				stringBuilder.AppendLine(iprange.Trace());
			}
			return stringBuilder.ToString();
		}

		public string TraceIP()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in this.m_ipList)
			{
				stringBuilder.AppendLine(value);
			}
			return stringBuilder.ToString();
		}

		private List<string> m_ipList;

		private List<IPRange> m_ipRangeList;
	}
}
