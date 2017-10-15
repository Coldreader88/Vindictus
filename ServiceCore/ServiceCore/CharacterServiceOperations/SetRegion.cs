using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SetRegion : Operation
	{
		public int RegionCode { get; set; }

		public SetRegion(int regionCode)
		{
			this.RegionCode = regionCode;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
