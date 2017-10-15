using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DyeAmpleCompleteMessage : IMessage
	{
		public int Color { get; set; }

		public DyeAmpleCompleteMessage(int color)
		{
			this.Color = color;
		}

		public override string ToString()
		{
			return string.Format("DyeAmpleCompleteMessage [ Color = {0} ]", this.Color);
		}
	}
}
