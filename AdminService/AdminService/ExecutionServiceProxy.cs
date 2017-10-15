using System;
using System.Collections.Generic;
using System.Net;

namespace AdminService
{
	internal class ExecutionServiceProxy
	{
		public IPEndPoint ExecutionServiceInfo
		{
			get
			{
				return this.executionServiceInfo;
			}
		}

		public IEnumerable<string> Services
		{
			get
			{
				return this.services;
			}
		}

		public IEnumerable<string> AppDomains
		{
			get
			{
				return this.appdomains;
			}
		}

		public ExecutionServiceProxy(IPEndPoint eServiceInfo, IEnumerable<string> sList, IEnumerable<string> aList)
		{
			this.executionServiceInfo = eServiceInfo;
			this.services = sList;
			this.appdomains = aList;
		}

		private IPEndPoint executionServiceInfo;

		private IEnumerable<string> services;

		private IEnumerable<string> appdomains;
	}
}
