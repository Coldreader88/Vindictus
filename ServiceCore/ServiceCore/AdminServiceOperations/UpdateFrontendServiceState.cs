using System;
using System.Collections.Generic;

namespace ServiceCore.AdminServiceOperations
{
	[Serializable]
	public sealed class UpdateFrontendServiceState
	{
		public int UserCount
		{
			get
			{
				return this.userTotalCount;
			}
		}

		public IEnumerable<string> UserNames
		{
			get
			{
				return this.userNameList;
			}
		}

		public UpdateFrontendServiceState(int count, IEnumerable<string> list)
		{
			this.userTotalCount = count;
			this.userNameList = list;
		}

		private int userTotalCount;

		private IEnumerable<string> userNameList;
	}
}
