using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RepairItemMessage : IMessage
	{
		public ICollection<long> ItemIDs { get; set; }

		public bool AddAllEquippedItems { get; set; }

		public bool AddAllBrokenItems { get; set; }

		public bool AddAllRepairableItems { get; set; }

		public int Price
		{
			get
			{
				return this.price;
			}
			private set
			{
				this.price = value;
			}
		}

		public RepairItemMessage(ICollection<long> itemIDs)
		{
			this.ItemIDs = (itemIDs ?? ((ICollection<long>)new long[0]));
		}

		public override string ToString()
		{
			return string.Format("RepairItemMessage[ ]", new object[0]);
		}

		[NonSerialized]
		private int price;
	}
}
