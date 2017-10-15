using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class StartAutoFishing : Operation
	{
		public int SerialNumber { get; set; }

		public int Tag { get; set; }

		public string Argument { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
