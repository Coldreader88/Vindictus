using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class InsertTradeTirOrderMessage : IMessage
	{
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

		private int num;

		private int durationMin;

		private int unitPrice;
	}
}
