using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class MoveToHousingHost : Operation
	{
		public bool IsToHousingHost { get; set; }

		public MoveToHousingHost(bool isToHousingHost)
		{
			this.IsToHousingHost = isToHousingHost;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
