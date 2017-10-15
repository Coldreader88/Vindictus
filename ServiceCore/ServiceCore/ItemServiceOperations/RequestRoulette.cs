using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RequestRoulette : Operation
	{
		public RequestRoulette.RequestType Type { get; set; }

		public List<string> CheatArg { get; set; }

		public RequestRoulette()
		{
			this.Type = RequestRoulette.RequestType.INVALID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		public enum RequestType
		{
			INVALID = -1,
			LOAD,
			NORMAL_BOARD,
			PREMIUM_BOARD,
			PICK_SLOT,
			GET_PICKED_ITEM,
			CHEAT_BOARD = 99
		}
	}
}
