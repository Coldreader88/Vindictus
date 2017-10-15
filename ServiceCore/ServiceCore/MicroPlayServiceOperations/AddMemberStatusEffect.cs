using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class AddMemberStatusEffect : Operation
	{
		public int Tag { get; set; }

		public string Type { get; set; }

		public int Level { get; set; }

		public int RemainTimeSec { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
