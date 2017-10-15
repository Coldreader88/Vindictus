using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class HousingHostStarted : Operation
	{
		public GameInfo GameInfo { get; set; }

		public HousingHostStarted(GameInfo gameInfo)
		{
			this.GameInfo = gameInfo;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
