using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SelectPattern : Operation
	{
		public int PatternID { get; set; }

		public SelectPattern(int patternID)
		{
			this.PatternID = patternID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
