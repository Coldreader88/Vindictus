using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HotSpringRequestInfoMessage : IMessage
	{
		public int Channel { get; set; }

		public int TownID { get; set; }
	}
}
