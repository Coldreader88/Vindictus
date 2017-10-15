using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestRoulettePickSlotMessage : IMessage
	{
		public override string ToString()
		{
			return "";
		}
	}
}
