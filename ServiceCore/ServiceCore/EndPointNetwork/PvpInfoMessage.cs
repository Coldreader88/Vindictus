using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpInfoMessage : IMessage
	{
		public IList<PvpResultInfo> PvpResultList { get; private set; }

		public PvpInfoMessage(IList<PvpResultInfo> PvpResultList)
		{
			this.PvpResultList = PvpResultList;
		}

		public override string ToString()
		{
			return string.Format("PvpInfoMessage[{0}]", this.PvpResultList.Count);
		}
	}
}
