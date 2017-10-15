using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SharingStartMessage : IMessage
	{
		public long ItemID { get; set; }

		public List<long> TargetsCID { get; set; }
	}
}
