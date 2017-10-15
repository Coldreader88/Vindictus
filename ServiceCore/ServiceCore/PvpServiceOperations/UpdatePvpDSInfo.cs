using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class UpdatePvpDSInfo : Operation
	{
		public int DSID { get; set; }

		public DSInfo DSInfo { get; set; }

		public Dictionary<int, DSInfo> DSInfoDict { get; set; }

		public UpdatePvpDSInfo(int dsID, DSInfo dsInfo)
		{
			this.DSInfo = dsInfo;
			this.DSID = dsID;
		}

		public UpdatePvpDSInfo(Dictionary<int, DSInfo> dsInfoDict)
		{
			this.DSInfoDict = dsInfoDict;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
