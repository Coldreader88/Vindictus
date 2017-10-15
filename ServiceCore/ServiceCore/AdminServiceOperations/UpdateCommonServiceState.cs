using System;
using System.Collections.Generic;

namespace ServiceCore.AdminServiceOperations
{
	[Serializable]
	public sealed class UpdateCommonServiceState
	{
		public string Name
		{
			get
			{
				return this.serviceClass;
			}
		}

		public IEnumerable<string> Info
		{
			get
			{
				return this.infomation;
			}
		}

		public UpdateCommonServiceState(string name, IEnumerable<string> info)
		{
			this.serviceClass = name;
			this.infomation = info;
		}

		private string serviceClass;

		private IEnumerable<string> infomation;
	}
}
