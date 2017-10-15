using System;

namespace Nexon.Nisms.Packets
{
	public class CheckBalanceResponse
	{
		public Result Result { get; private set; }

		public int Balance { get; private set; }

		internal CheckBalanceResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
			this.Balance = packet.ReadInt32();
		}
	}
}
