using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MicroPlayEventMessage : IMessage
	{
		public int Slot { get; set; }

		public string EventString { get; set; }

		public MicroPlayEventMessage(int slot, string eventString)
		{
			this.Slot = slot;
			this.EventString = eventString;
		}

		public override string ToString()
		{
			return string.Format("MicroPlayEventMessage[ Slot = {0} EventString = {1} ]", this.Slot, this.EventString);
		}
	}
}
