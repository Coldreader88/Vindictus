using System;

namespace Nexon.Nisms.Packets
{
	public class PurchaseItemRefundResponse
	{
		public int UsageNo { get; private set; }

		public bool Result
		{
			get
			{
				return this.ResultCode == 1;
			}
		}

		public int ResultCode { get; private set; }

		internal PurchaseItemRefundResponse(ref Packet packet)
		{
			this.ResultCode = packet.ReadInt32();
		}
	}
}
