using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RarityCoreListMessage : IMessage
	{
		public List<RareCoreInfo> RareCores { get; private set; }

		public RarityCoreListMessage(List<RareCoreInfo> list)
		{
			this.RareCores = list;
		}
	}
}
