using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class FrameRateMessage : IMessage
	{
		public int FrameRate { get; set; }

		public override string ToString()
		{
			return string.Format("FrameRateMessage[{0}fps]", this.FrameRate);
		}
	}
}
