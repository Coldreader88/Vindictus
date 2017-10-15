using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class ArmorBroken : Operation
	{
		public long HostCID { get; set; }

		public int Owner { get; set; }

		public int CostumePart { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
