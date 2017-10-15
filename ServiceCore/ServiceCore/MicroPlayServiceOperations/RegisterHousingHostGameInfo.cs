using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class RegisterHousingHostGameInfo : Operation
	{
		public long HostCID { get; set; }

		public GameInfo GameInfo { get; set; }

		public RegisterHousingHostGameInfo(long hostCID, GameInfo gameInfo)
		{
			this.HostCID = hostCID;
			this.GameInfo = gameInfo;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
