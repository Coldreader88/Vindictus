using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class DSCheatToggle : Operation
	{
		public DSCheatToggle(int on)
		{
			this.On = on;
		}

		public int On { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
