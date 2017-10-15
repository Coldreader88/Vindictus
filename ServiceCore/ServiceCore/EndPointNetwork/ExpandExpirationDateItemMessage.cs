using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ExpandExpirationDateItemMessage : IMessage
	{
		public ExpandExpirationDateItemMessageType MessageType
		{
			get
			{
				return (ExpandExpirationDateItemMessageType)this.messageType;
			}
			private set
			{
				this.messageType = (int)value;
			}
		}

		public long ItemID
		{
			get
			{
				return this.itemID;
			}
			private set
			{
				this.itemID = value;
			}
		}

		public string ExpanderName
		{
			get
			{
				return this.expanderName;
			}
			private set
			{
				this.expanderName = value;
			}
		}

		public int ExpanderPrice
		{
			get
			{
				return this.expanderPrice;
			}
			private set
			{
				this.expanderPrice = value;
			}
		}

		public bool IsCredit
		{
			get
			{
				return this.isCredit;
			}
			private set
			{
				this.isCredit = value;
			}
		}

		public override string ToString()
		{
			return string.Format("ExpandExpirationDateIemMessage[ MessageType = {0} ItemID = {1} ]", this.MessageType, this.ItemID);
		}

		private int messageType;

		private long itemID;

		private string expanderName;

		private int expanderPrice;

		private bool isCredit;
	}
}
