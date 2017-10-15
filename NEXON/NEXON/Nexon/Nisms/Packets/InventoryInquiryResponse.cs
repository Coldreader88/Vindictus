using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexon.Nisms.Packets
{
	public class InventoryInquiryResponse
	{
		public Result Result { get; private set; }

		public int TotalCount { get; private set; }

		public ICollection<Order> Order { get; private set; }

		public ICollection<Package> Package { get; private set; }

		public ICollection<Lottery> Lottery { get; private set; }

		internal InventoryInquiryResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
			this.TotalCount = packet.ReadInt32();
			int num = packet.ReadInt32();
			this.Order = new List<Order>(num);
			foreach (int num2 in Enumerable.Range(0, num))
			{
				Order item = new Order(ref packet);
				this.Order.Add(item);
			}
			int num3 = packet.ReadInt32();
			this.Package = new List<Package>(num3);
			foreach (int num4 in Enumerable.Range(0, num3))
			{
				Package item2 = new Package(ref packet);
				this.Package.Add(item2);
			}
			int num5 = packet.ReadInt32();
			this.Lottery = new List<Lottery>(num5);
			foreach (int num6 in Enumerable.Range(0, num5))
			{
				Lottery item3 = new Lottery(ref packet);
				this.Lottery.Add(item3);
			}
		}
	}
}
