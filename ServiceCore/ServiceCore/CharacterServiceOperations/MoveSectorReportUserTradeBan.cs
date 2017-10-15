using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class MoveSectorReportUserTradeBan : Operation
	{
		public int Status { get; set; }

		public int? BanInterval { get; set; }

		public MoveSectorReportUserTradeBan(int status, int? banInterval)
		{
			this.Status = status;
			this.BanInterval = banInterval;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
