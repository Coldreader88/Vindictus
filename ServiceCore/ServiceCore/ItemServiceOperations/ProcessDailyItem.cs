using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class ProcessDailyItem : Operation
	{
		public int CafeLevel { get; set; }

		public int CafeType { get; set; }

		public bool IsDailyItemRequired { get; set; }

		public UserInfo UserInfo { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
