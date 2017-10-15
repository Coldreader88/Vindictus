using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class CreateHousingMessage : IMessage
	{
		public int OpenLevel { get; set; }

		public string Desc { get; set; }
	}
}
