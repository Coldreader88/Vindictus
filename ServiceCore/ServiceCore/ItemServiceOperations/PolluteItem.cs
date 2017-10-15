using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class PolluteItem : Operation
	{
		public float Ratio { get; set; }

		public Dictionary<string, float> ItemDurabilityPollutionRatios { get; set; }

		public Dictionary<string, float> ItemMaxDurabilityPollutionRatios { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new PolluteItem.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(PolluteItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					Log<PolluteItem>.Logger.DebugFormat("아이템이 지저분해졌습니다", new object[0]);
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
