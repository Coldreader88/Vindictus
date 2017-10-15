using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ManufactureLevelUpMessage : IMessage
	{
		public string MID { get; set; }

		public int Grade { get; set; }
	}
}
