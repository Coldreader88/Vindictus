using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ItemDropQuestInfo
	{
		public IDictionary<long, ItemDropUserInfo> Users { get; private set; }

		public ItemDropQuestInfo(IDictionary<long, ItemDropUserInfo> users)
		{
			this.Users = users;
		}
	}
}
