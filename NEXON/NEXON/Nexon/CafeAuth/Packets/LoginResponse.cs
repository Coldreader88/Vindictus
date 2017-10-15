using System;
using System.Text;

namespace Nexon.CafeAuth.Packets
{
	[Serializable]
	public sealed class LoginResponse
	{
		public long SessionNo { get; internal set; }

		public string NexonID { get; internal set; }

		public AuthorizeResult Result { get; internal set; }

		public char AuthorizeType { get; internal set; }

		public char ChargeType { get; internal set; }

		public Option Option { get; internal set; }

		public int Argument { get; internal set; }

		public bool IsCafe { get; internal set; }

		public ExtendInformation ExtendInfos { get; internal set; }

		internal LoginResponse(ref Packet packet)
		{
			packet.ReadByte();
			this.SessionNo = packet.ReadInt64();
			this.NexonID = packet.ReadString();
			this.Result = (AuthorizeResult)packet.ReadByte();
			this.AuthorizeType = (char)packet.ReadByte();
			this.ChargeType = (char)packet.ReadByte();
			this.Option = (Option)packet.ReadByte();
			this.Argument = packet.ReadInt32();
			this.IsCafe = packet.ReadBoolean();
			this.ExtendInfos = new ExtendInformation(ref packet);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{ LoginResponse NexonID = ");
			stringBuilder.Append(this.NexonID);
			stringBuilder.Append(", SessionNo = ");
			stringBuilder.Append(this.SessionNo);
			stringBuilder.Append(", Result = ");
			stringBuilder.Append(this.Result);
			stringBuilder.Append(", Option = ");
			stringBuilder.Append(this.Option);
			stringBuilder.Append(", Argument = ");
			stringBuilder.Append(this.Argument);
			stringBuilder.Append(", IsCafe = ");
			stringBuilder.Append(this.IsCafe);
			stringBuilder.Append(", ExtendInfos = ");
			stringBuilder.Append(this.ExtendInfos);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
