using System;
using System.Net;

namespace Nexon.Nisms.Packets
{
	internal class PurchaseItemRefundRequest
	{
		public IPAddress RemoteIP { get; set; }

		public string RequestID { get; set; }

		public string GameID { get; set; }

		public int UsageNo { get; set; }

		public int ProductNo { get; set; }

		public short Quantity { get; set; }

		private int CalculateStructureSize()
		{
			return this.RemoteIP.CalculateStructureSize() + this.RequestID.CalculateStructureSize() + this.GameID.CalculateStructureSize() + this.UsageNo.CalculateStructureSize() + this.ProductNo.CalculateStructureSize() + this.Quantity.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.PurchaseItemRefund);
			result.Write(this.RemoteIP);
			result.Write(this.RequestID);
			result.Write(this.GameID);
			result.Write(this.UsageNo);
			result.Write(this.ProductNo);
			result.Write(this.Quantity);
			return result;
		}
	}
}
