using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SharingResponseMessage : IMessage
	{
		public bool Accepted { get; set; }
	}
}
