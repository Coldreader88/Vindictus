using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class LaunchPvpDS : Operation
	{
		public long PvpID { get; set; }

		public int DSID { get; set; }

		public string PvpBSP { get; set; }

		public Dictionary<string, string> Config { get; set; }

		public PvpGameInfo GameInfo { get; set; }

		public LaunchPvpDS(long pvpID, int dsid, string pvpBSP, Dictionary<string, string> config, PvpGameInfo gameInfo)
		{
			this.PvpID = pvpID;
			this.DSID = dsid;
			this.PvpBSP = pvpBSP;
			this.Config = config;
			this.GameInfo = gameInfo;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new LaunchPvpDS.Request(this);
		}

		[NonSerialized]
		public long DSEntityID;

		private class Request : OperationProcessor<LaunchPvpDS>
		{
			public Request(LaunchPvpDS op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is long)
				{
					base.Operation.DSEntityID = (long)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
