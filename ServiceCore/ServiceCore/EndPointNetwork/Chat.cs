using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class Chat : IMessage
	{
		public string Message { get; set; }
	}
}
