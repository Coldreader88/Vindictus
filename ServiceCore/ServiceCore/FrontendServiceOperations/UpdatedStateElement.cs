using System;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class UpdatedStateElement
	{
		public string Name { get; set; }

		public string Address { get; set; }

		public int State { get; set; }

		public int Time { get; set; }
	}
}
