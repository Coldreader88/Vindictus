using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AddStatusEffect : IMessage
	{
		public int Tag { get; set; }

		public string Type { get; set; }

		public int Level { get; set; }

		public int TimeSec { get; set; }
	}
}
