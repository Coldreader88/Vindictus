using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class PurchaseGuildStorage : Operation
	{
		public long PuchasedCID { get; set; }

		public int ProductNo { get; set; }

		public bool IsCredit { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
