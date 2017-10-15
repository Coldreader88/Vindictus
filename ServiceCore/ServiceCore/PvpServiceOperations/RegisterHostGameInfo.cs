using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class RegisterHostGameInfo : Operation
	{
		public long HostCID { get; set; }

		public GameInfo GameInfo { get; set; }

		public RegisterHostGameInfo(long HostCID, GameInfo GameInfo)
		{
			this.HostCID = HostCID;
			this.GameInfo = GameInfo;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
