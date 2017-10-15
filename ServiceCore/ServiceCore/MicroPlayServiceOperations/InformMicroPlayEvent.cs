using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class InformMicroPlayEvent : Operation
	{
		public long HostCID { get; set; }

		public int Slot { get; set; }

		public string EventString { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
