using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ReadyMessage : IMessage
	{
		public byte Ready
		{
			get
			{
				return this.ready;
			}
		}

		public ReadyMessage(byte ready)
		{
			this.ready = ready;
		}

		public override string ToString()
		{
			return string.Format("ReadyMessage[ ready = {0} ]", this.ready);
		}

		private byte ready;
	}
}
