using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class UpdateDSShipInfo : Operation
	{
		public int DSID { get; set; }

		public long PartyID { get; set; }

		public UpdateDSShipInfo.CommandEnum Command { get; set; }

		public string Arg { get; set; }

		public int ServiceID { get; set; }

		public UpdateDSShipInfo(int dsid, long pid, UpdateDSShipInfo.CommandEnum command, string arg)
		{
			this.ServiceID = 0;
			this.DSID = dsid;
			this.PartyID = pid;
			this.Command = command;
			this.Arg = arg;
		}

		public UpdateDSShipInfo(int serviceID, int dsid, long pid, UpdateDSShipInfo.CommandEnum command, string arg)
		{
			this.ServiceID = serviceID;
			this.DSID = dsid;
			this.PartyID = pid;
			this.Command = command;
			this.Arg = arg;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		public enum CommandEnum
		{
			LaunchComplete,
			StartGame,
			BlockEntering,
			DSFinished,
			PVPFinished,
			DSClosed,
			PVPClosed
		}
	}
}
