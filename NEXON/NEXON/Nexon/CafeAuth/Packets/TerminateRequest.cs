using System;
using System.Text;

namespace Nexon.CafeAuth.Packets
{
	[Serializable]
	public sealed class TerminateRequest
	{
		public long SessionNo { get; internal set; }

		public string NexonID { get; internal set; }

		public string CharacterName { get; internal set; }

		public Option Option { get; internal set; }

		public ExtendInformation ExtendInfos { get; internal set; }

		internal TerminateRequest(ref Packet packet)
		{
			packet.ReadByte();
			this.SessionNo = packet.ReadInt64();
			this.NexonID = packet.ReadString();
			this.CharacterName = packet.ReadString();
			this.Option = (Option)packet.ReadByte();
			this.ExtendInfos = new ExtendInformation(ref packet);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{ TerminateRequest NexonID = ");
			stringBuilder.Append(this.NexonID);
			stringBuilder.Append(", CharacterName = ");
			stringBuilder.Append(this.CharacterName);
			stringBuilder.Append(", SessionNo = ");
			stringBuilder.Append(this.SessionNo);
			stringBuilder.Append(", Option = ");
			stringBuilder.Append(this.Option);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
