using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MovePetSlotMessage : IMessage
	{
		public long PetID { get; set; }

		public int DestSlotNo { get; set; }

		public override string ToString()
		{
			return string.Format(" [ MovePetSlotMessage ] PetID : {0}, destSlot : {1} ", this.PetID, this.DestSlotNo);
		}
	}
}
