using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class PolluteItemEndBattle : Operation
	{
		public Dictionary<string, float> ItemDurabilityPollutionRatios { get; set; }

		public Dictionary<string, float> ItemMaxDurabilityPollutionRatios { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new PolluteItemEndBattle.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(PolluteItemEndBattle op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					Log<PolluteItemEndBattle>.Logger.DebugFormat("아이템이 지저분해졌습니다", new object[0]);
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
