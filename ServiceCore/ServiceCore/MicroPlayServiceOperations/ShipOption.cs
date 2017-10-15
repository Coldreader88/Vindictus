using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class ShipOption : Operation
	{
		public ShipOptionInfo Option { get; set; }

		public ShipOption(ShipOptionInfo Option)
		{
			this.Option = Option;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
