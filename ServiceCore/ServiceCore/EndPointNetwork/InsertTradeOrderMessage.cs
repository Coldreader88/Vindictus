using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class InsertTradeOrderMessage : IMessage
	{
		public long ItemID
		{
			get
			{
				return this.itemID;
			}
			set
			{
				this.itemID = value;
			}
		}

		public int Num
		{
			get
			{
				return this.num;
			}
			set
			{
				this.num = value;
			}
		}

		public int DurationMin
		{
			get
			{
				return this.durationMin;
			}
			set
			{
				this.durationMin = value;
			}
		}

		public int UnitPrice
		{
			get
			{
				return this.unitPrice;
			}
			set
			{
				this.unitPrice = value;
			}
		}

		public byte TradeType
		{
			get
			{
				return this.tradeType;
			}
			set
			{
				this.tradeType = value;
			}
		}

		private long itemID;

		private int num;

		private int durationMin;

		private int unitPrice;

		private byte tradeType;
	}
}
