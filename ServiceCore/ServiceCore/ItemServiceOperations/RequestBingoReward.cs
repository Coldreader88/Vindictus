using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RequestBingoReward : Operation
	{
		public Dictionary<int, int> BingoNumberMapDic { get; set; }

		public int? BingoVer { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
