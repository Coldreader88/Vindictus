using System;
using System.ServiceModel.Channels;

namespace Nexon.Enterprise.ServiceFacade
{
	public class ProcessMessageEventArgs : EventArgs
	{
		public Message CurrentMessage { get; private set; }

		public ProcessMessageEventArgs(Message message)
		{
			this.CurrentMessage = message;
		}
	}
}
