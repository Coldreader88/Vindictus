using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestGoddessProtectionMessage : IMessage
	{
		public string ItemClass { get; set; }
	}
}
