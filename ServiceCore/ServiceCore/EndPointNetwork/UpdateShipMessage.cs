using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateShipMessage : IMessage
	{
		private ShipInfo Info
		{
			get
			{
				return this.info;
			}
			set
			{
				this.info = value;
			}
		}

		public UpdateShipMessage(ShipInfo info)
		{
			this.info = info;
		}

		public UpdateShipMessage()
		{
		}

		public override string ToString()
		{
			return string.Format("UpdateShipMessage[ info = {0} ]", this.info);
		}

		private ShipInfo info;
	}
}
