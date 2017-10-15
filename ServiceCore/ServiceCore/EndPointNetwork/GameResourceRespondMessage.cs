using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GameResourceRespondMessage : IMessage
	{
		public string ResourceRespond { get; set; }
	}
}
