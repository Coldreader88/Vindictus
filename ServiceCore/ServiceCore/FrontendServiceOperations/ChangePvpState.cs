using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class ChangePvpState : Operation
	{
		public ChangePvpState.PvpStateEnum NextState { get; set; }

		public string PvpMode { get; set; }

		public long PvpID { get; set; }

		public string PvpUserCountKey { get; set; }

		public ChangePvpState(ChangePvpState.PvpStateEnum nextState, string pvpMode, long pvpID, string pvpUserCountKey)
		{
			this.NextState = nextState;
			this.PvpMode = pvpMode;
			this.PvpID = pvpID;
			this.PvpUserCountKey = pvpUserCountKey;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		public enum PvpStateEnum
		{
			Waiting,
			Hosting,
			Member,
			Initial,
			GameFinished
		}
	}
}
