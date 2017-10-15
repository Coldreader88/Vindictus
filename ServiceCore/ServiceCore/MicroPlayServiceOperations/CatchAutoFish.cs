using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class CatchAutoFish : Operation
	{
		public int SerialNumber { get; set; }

		public int Tag { get; set; }

		public int Time { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
