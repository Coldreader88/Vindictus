using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class UpdateGuildStorageSettings : Operation
	{
		public long RequestingCID { get; set; }

		public int GoldLimit { get; set; }

		public long AccessLimtiTag { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
