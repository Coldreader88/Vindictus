using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuickSlotInfoMessage : IMessage
	{
		public QuickSlotInfoMessage(QuickSlotInfo info)
		{
			this.info = info;
		}

		public override string ToString()
		{
			return string.Format("QuickSlotInfoMessage[ info = {0} ]", this.info);
		}

		private QuickSlotInfo info;
	}
}
