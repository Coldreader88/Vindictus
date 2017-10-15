using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StartGameAckMessage : IMessage
	{
		public bool Succeed { get; set; }

		public override string ToString()
		{
			return string.Format("StartGameAckMessage[]", new object[0]);
		}
	}
}
