using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class VocationTransformFinishedMessage : IMessage
	{
		public int SlotNum { get; set; }

		public int TotalDamage { get; set; }

		public override string ToString()
		{
			return string.Format("VocationTransformFinishedMessage [ SlotNum = {0} TotalDamage = {1} ]", this.SlotNum, this.TotalDamage);
		}
	}
}
