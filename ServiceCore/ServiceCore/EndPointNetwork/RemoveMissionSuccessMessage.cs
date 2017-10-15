using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RemoveMissionSuccessMessage : IMessage
	{
		public long ID { get; set; }
	}
}
