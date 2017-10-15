using System;
using System.Text;

namespace Nexon.CafeAuth.Packets
{
	[Serializable]
	public sealed class MessageRequest
	{
		public long SessionNo { get; internal set; }

		public string NexonID { get; internal set; }

		public string CharacterName { get; internal set; }

		public Option Option { get; internal set; }

		public int Argument { get; internal set; }

		public int SessionCount { get; internal set; }

		public ExtendInformation ExtendInfos { get; internal set; }

		internal MessageRequest(ref Packet packet)
		{
			packet.ReadByte();
			this.SessionNo = packet.ReadInt64();
			this.NexonID = packet.ReadString();
			this.CharacterName = packet.ReadString();
			this.Option = (Option)packet.ReadByte();
			this.Argument = packet.ReadInt32();
			this.SessionCount = packet.ReadInt32();
			this.ExtendInfos = new ExtendInformation(ref packet);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{ MessageRequest exonID = ");
			stringBuilder.Append(this.NexonID);
			stringBuilder.Append(", CharacterName = ");
			stringBuilder.Append(this.CharacterName);
			stringBuilder.Append(", SessionNo = ");
			stringBuilder.Append(this.SessionNo);
			stringBuilder.Append(", Option = ");
			stringBuilder.Append(this.Option);
			stringBuilder.Append(", Argument = ");
			stringBuilder.Append(this.Argument);
			stringBuilder.Append(", SessionCount = ");
			stringBuilder.Append(this.SessionCount);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
