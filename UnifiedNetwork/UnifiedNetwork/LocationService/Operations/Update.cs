using System;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.LocationService.Operations
{
	[Serializable]
	internal sealed class Update : Operation
	{
		public int Type { get; set; }

		public ServiceInfo Info { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}

		public enum UpdateType
		{
			Up,
			Down
		}
	}
}
