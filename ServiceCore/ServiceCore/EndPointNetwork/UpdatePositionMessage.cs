using System;

namespace ServiceCore.EndPointNetwork
{
	public sealed class UpdatePositionMessage : IP2PMessage
	{
		public float X { get; set; }

		public float Y { get; set; }

		public float Z { get; set; }

		public int Yaw { get; set; }

		public uint Action { get; set; }
	}
}
