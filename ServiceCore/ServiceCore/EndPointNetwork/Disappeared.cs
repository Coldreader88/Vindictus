using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class Disappeared : IMessage
	{
		public long ID { get; set; }

		public override string ToString()
		{
			return string.Format("Disappeared {0}", this.ID);
		}
	}
}
