using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateWhisperFilterMessage : IMessage
	{
		public int OperationType { get; set; }

		public string TargetID { get; set; }

		public int Level { get; set; }
	}
}
