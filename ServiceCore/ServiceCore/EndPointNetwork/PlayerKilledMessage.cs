using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PlayerKilledMessage : IMessage
	{
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		public PlayerKilledMessage(int tag)
		{
			this.tag = tag;
		}

		public override string ToString()
		{
			return string.Format("PlayerKilledMessage[ tag = {0} ]", this.tag);
		}

		private int tag;
	}
}
