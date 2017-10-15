using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class Identify : IMessage
	{
		public long ID { get; set; }

		public int Key { get; set; }
	}
}
