using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MarbleProcessNodeResultMessage : IMessage
	{
		public int Type { get; set; }

		public bool IsChance { get; set; }

		public MarbleProcessNodeResultMessage(MarbleNodeProcessType type, bool isChance)
		{
			this.Type = (int)type;
			this.IsChance = isChance;
		}
	}
}
