using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DestroyMicroPlayContentsMessage : IMessage
	{
		public string EntityID { get; set; }
	}
}
