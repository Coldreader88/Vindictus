using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ItemDropUserInfo
	{
		public long CID { get; private set; }

		public IDictionary<string, ItemDropEntityInfo> Entities { get; private set; }

		public ItemDropUserInfo(long cid, IDictionary<string, ItemDropEntityInfo> entities)
		{
			this.CID = cid;
			this.Entities = entities;
		}
	}
}
