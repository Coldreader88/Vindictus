using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestRouletteBoardMessage : IMessage
	{
		public int Type { get; set; }

		public override string ToString()
		{
			return "";
		}
	}
}
