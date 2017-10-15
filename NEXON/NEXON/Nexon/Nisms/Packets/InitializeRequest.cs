using System;

namespace Nexon.Nisms.Packets
{
	internal class InitializeRequest
	{
		public string ServiceCode { get; private set; }

		public byte ServerNo { get; private set; }

		internal InitializeRequest(string serviceCode, byte serverNo)
		{
			this.ServiceCode = serviceCode;
			this.ServerNo = serverNo;
		}

		private int CalculateStructureSize()
		{
			return this.ServiceCode.CalculateStructureSize() + this.ServerNo.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.Initialize);
			result.Write(this.ServiceCode);
			result.Write(this.ServerNo);
			return result;
		}
	}
}
