using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestMarbleProcessNodeMessage : IMessage
	{
		public int CurrentIndex { get; set; }
	}
}
