using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseConsumableMessage : IMessage
	{
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		public int Part
		{
			get
			{
				return this.part;
			}
		}

		public int Slot
		{
			get
			{
				return this.slot;
			}
		}

		public UseConsumableMessage(int tag, int part, int slot)
		{
			this.tag = tag;
			this.part = part;
			this.slot = slot;
		}

		public override string ToString()
		{
			return string.Format("UseConsumableMessage[ tag = {0} part = {1}({2}) ]", this.tag, this.part, this.slot);
		}

		private int tag;

		private int part;

		private int slot;
	}
}
