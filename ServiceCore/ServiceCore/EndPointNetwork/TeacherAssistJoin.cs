using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TeacherAssistJoin : IMessage
	{
		public long ShipID { get; set; }
	}
}
