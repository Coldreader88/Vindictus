using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RequestMarble : Operation
	{
		public RequestMarble.RequestType Type { get; set; }

		public int CurrentIndex { get; set; }

		public int DiceID { get; set; }

		public RequestMarble(RequestMarble.RequestType type)
		{
			this.Type = type;
			this.DiceID = -1;
			this.CurrentIndex = -1;
		}

		public RequestMarble(RequestMarble.RequestType type, int currentIndex, int diceID)
		{
			this.Type = type;
			this.CurrentIndex = currentIndex;
			this.DiceID = diceID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		public enum RequestType
		{
			INVALID = -1,
			LOAD,
			INFO,
			CAST_DICE,
			PROCESS_NODE,
			PROCESS_CHANCEROAD,
			PROCESS_CHANDEDICE
		}
	}
}
