using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class _CheatItemDropTest : Operation
	{
		public long CID { get; set; }

		public long FID { get; set; }

		public int Level { get; set; }

		public string QuestID { get; set; }

		public int SwearID { get; set; }

		public int Difficulty { get; set; }

		public int Count { get; set; }

		public int EvilcoreCount { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
