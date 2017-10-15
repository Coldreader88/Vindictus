using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class VocationTransformMessage : IMessage
	{
		public int SlotNum { get; set; }

		public int TransformLevel { get; set; }

		public override string ToString()
		{
			return string.Format("VocationTransformMessage [ SlotNum {0} TransformLevel {1} ]", this.SlotNum, this.TransformLevel);
		}
	}
}
