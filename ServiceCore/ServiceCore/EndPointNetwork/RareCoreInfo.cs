using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RareCoreInfo
	{
		public int PlayerTag { get; private set; }

		public string CoreEntityName { get; private set; }

		public int CoreType { get; private set; }

		public RareCoreInfo(int playerTag, string coreEntityName, int coreType)
		{
			this.PlayerTag = playerTag;
			this.CoreEntityName = coreEntityName;
			this.CoreType = coreType;
		}
	}
}
