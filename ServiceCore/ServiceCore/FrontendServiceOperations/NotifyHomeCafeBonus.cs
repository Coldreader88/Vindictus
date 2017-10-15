using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class NotifyHomeCafeBonus : Operation
	{
		public int CafeType { get; set; }

		public NotifyHomeCafeBonus(int cafeType)
		{
			this.CafeType = cafeType;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
