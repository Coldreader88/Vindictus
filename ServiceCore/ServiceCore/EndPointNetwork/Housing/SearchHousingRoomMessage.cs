using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class SearchHousingRoomMessage : IMessage
	{
		public int Option { get; set; }

		public string Keyword { get; set; }
	}
}
