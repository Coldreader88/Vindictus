using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class ReportGuildScoreEvent : Operation
	{
		public string Event { get; set; }

		public string StringValue { get; set; }

		public int IntValue { get; set; }

		public int Multiplier { get; set; }

		public ReportGuildScoreEvent()
		{
			this.Multiplier = 1;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
