using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public struct ActionSync
	{
		public Vector3D Position { get; set; }

		public Vector3D Velocity { get; set; }

		public float Yaw { get; set; }

		public short Sequence { get; set; }

		public short ActionStateIndex { get; set; }

		public float StartTime { get; set; }

		public int State { get; set; }
	}
}
