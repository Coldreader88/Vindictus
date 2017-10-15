using System;

namespace UnifiedNetwork.Cooperation
{
	public interface IResultReceiver<MessageType>
	{
		MessageType ResultMessage { set; }
	}
}
