using System;

namespace Nexon.Nisms.Packets
{
	public class CheckBalanceEXResponse
	{
		public Result Result { get; private set; }

		public int Balance { get; private set; }

		public int NoRefundBalance { get; private set; }

		internal CheckBalanceEXResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
			this.Balance = packet.ReadInt32();
			this.NoRefundBalance = packet.ReadInt32();
		}
	}
}
