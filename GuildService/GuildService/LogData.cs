using System;

namespace GuildService
{
	public class LogData
	{
		public long GuildSN { get; set; }

		public long CID { get; set; }

		public OperationType Operation { get; set; }

		public short Event { get; set; }

		public string Arg1 { get; set; }

		public string Arg2 { get; set; }

		private void _SetLogData(long guildSN, long cid, OperationType operation, GuildLedgerEventType eventType, string arg1, string arg2)
		{
			this.GuildSN = guildSN;
			this.CID = cid;
			this.Operation = operation;
			this.Event = (short)eventType;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
		}

		public LogData(long guildSN, long cid, OperationType operation, GuildLedgerEventType eventType, string arg1, string arg2)
		{
			this._SetLogData(guildSN, cid, operation, eventType, arg1, arg2);
		}

		public LogData(long guildSN, long cid, OperationType operation, GuildLedgerEventType eventType, string arg1)
		{
			this._SetLogData(guildSN, cid, operation, eventType, arg1, "");
		}

		public LogData(long guildSN, long cid, OperationType operation, GuildLedgerEventType eventType)
		{
			this._SetLogData(guildSN, cid, operation, eventType, "", "");
		}
	}
}
