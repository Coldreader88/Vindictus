using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class EnterHousingMessage : IMessage
	{
		public string CharacterName { get; set; }

		public int HousingIndex { get; set; }

		public EnterHousingType EnterType { get; set; }

		public long HousingPlayID { get; set; }
	}
}
