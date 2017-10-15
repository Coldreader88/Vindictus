using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RoulettePickSlotResultMessage : IMessage
	{
		public int PickedSlot { get; set; }

		public RoulettePickSlotResultMessage(int pickedSlot)
		{
			this.PickedSlot = pickedSlot;
		}

		public override string ToString()
		{
			return "";
		}
	}
}
