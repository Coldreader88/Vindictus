using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestMarbleCastDiceMessage : IMessage
	{
		public int DiceID { get; set; }
	}
}
