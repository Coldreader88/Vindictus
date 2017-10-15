using System;

namespace Nexon.CafeAuthOld.Packets
{
	internal class Initialize
	{
		private GameSN GameSN { get; set; }

		private byte DomainSN { get; set; }

		private string DomainName { get; set; }

		public Initialize(GameSN gameSN, byte domainSN, string domainName)
		{
			this.GameSN = gameSN;
			this.DomainSN = domainSN;
			this.DomainName = domainName;
		}

		private int CalculateStructureSize()
		{
			return ((byte)this.GameSN).CalculateStructureSize() + this.DomainSN.CalculateStructureSize() + this.DomainName.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.Initialize);
			result.Write((byte)this.GameSN);
			result.Write(this.DomainSN);
			result.Write(this.DomainName);
			return result;
		}
	}
}
