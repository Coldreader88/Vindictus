using System;
using System.Text;

namespace Nexon.CafeAuth.Packets
{
	public class Initialize
	{
		public GameSN GameSN { get; private set; }

		public byte DomainSN { get; private set; }

		public string DomainName { get; private set; }

		public Initialize(GameSN gameSN, byte domainSN, string domainName)
		{
			this.GameSN = gameSN;
			this.DomainSN = domainSN;
			this.DomainName = domainName;
		}

		private int CalculateStructureSize()
		{
			return this.InitializeType.CalculateStructureSize() + ((byte)this.GameSN).CalculateStructureSize() + this.DomainSN.CalculateStructureSize() + this.DomainName.CalculateStructureSize() + this.SynchronizeType.CalculateStructureSize() + this.SynchronizeCount.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.Initialize);
			result.Write(this.InitializeType);
			result.Write((byte)this.GameSN);
			result.Write(this.DomainSN);
			result.Write(this.DomainName);
			result.Write(this.SynchronizeType);
			result.Write(this.SynchronizeCount);
			return result;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{ Initialize InitializeType = [");
			stringBuilder.Append(this.InitializeType);
			stringBuilder.Append("], GameSN = [");
			stringBuilder.Append(this.GameSN);
			stringBuilder.Append("], DomainSN = [");
			stringBuilder.Append(this.DomainSN);
			stringBuilder.Append("], DomainName = [");
			stringBuilder.Append(this.DomainName);
			stringBuilder.Append("], SynchronizeType = [");
			stringBuilder.Append(this.SynchronizeType);
			stringBuilder.Append("], SynchronizeCount = [");
			stringBuilder.Append(this.SynchronizeCount);
			stringBuilder.Append("] }");
			return stringBuilder.ToString();
		}

		public readonly byte InitializeType = 1;

		public readonly byte SynchronizeType = 1;

		public readonly int SynchronizeCount = 256;
	}
}
