using System;
using System.Text;

namespace Nexon.CafeAuth.Packets
{
	[Serializable]
	public sealed class InitializeResponse
	{
		public InitializeResult Result { get; internal set; }

		public byte DomainSN { get; internal set; }

		public string Message { get; internal set; }

		internal InitializeResponse(ref Packet packet)
		{
			packet.ReadByte();
			this.Result = (InitializeResult)packet.ReadByte();
			this.DomainSN = packet.ReadByte();
			this.Message = packet.ReadString();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{ InitializeResponse Result = [");
			stringBuilder.Append(this.Result.ToString());
			stringBuilder.Append("], DomainSN = [");
			stringBuilder.Append(this.DomainSN);
			stringBuilder.Append("], Message = [");
			stringBuilder.Append(this.Message);
			stringBuilder.Append("] }");
			return stringBuilder.ToString();
		}
	}
}
