using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AdditionalMicroPlayContentsMessage : IMessage
	{
		public string HostCommandString { get; set; }
	}
}
