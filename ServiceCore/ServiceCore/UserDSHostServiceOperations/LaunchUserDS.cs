using System;
using System.Collections.Generic;
using System.Net;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.UserDSHostServiceOperations
{
	[Serializable]
	public sealed class LaunchUserDS : Operation
	{
		public string QuestID { get; set; }

		public long MicroPlayID { get; set; }

		public long PartyID { get; set; }

		public long FrontendID { get; set; }

		public bool IsAdultMode { get; set; }

		public bool IsPracticeMode { get; set; }

		public IPAddress HostIP { get; set; }

		public long UserDSRealHostCID { get; set; }

		public string ServerAddress
		{
			get
			{
				return this.serverAddress;
			}
			set
			{
				this.serverAddress = value;
			}
		}

		public long UserDSEntityID
		{
			get
			{
				return this.userDSEntityID;
			}
			set
			{
				this.userDSEntityID = value;
			}
		}

		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
			}
		}

		public LaunchUserDS(string questID, long microPlayID, long partyID, long frontendID, bool isAdultMode, bool isPracticeMode, IPAddress hostIP, long userDSRealHostCID)
		{
			this.QuestID = questID;
			this.MicroPlayID = microPlayID;
			this.PartyID = partyID;
			this.FrontendID = frontendID;
			this.IsAdultMode = isAdultMode;
			this.IsPracticeMode = isPracticeMode;
			this.HostIP = hostIP;
			this.UserDSRealHostCID = userDSRealHostCID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new LaunchUserDS.Request(this);
		}

		[NonSerialized]
		public string serverAddress;

		[NonSerialized]
		public long userDSEntityID;

		[NonSerialized]
		public int port;

		private class Request : OperationProcessor<LaunchUserDS>
		{
			public Request(LaunchUserDS op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is string)
				{
					base.Operation.ServerAddress = (string)base.Feedback;
					yield return null;
				}
				if (base.Feedback is int)
				{
					base.Operation.Port = (int)base.Feedback;
					yield return null;
				}
				if (base.Feedback is long)
				{
					base.Operation.UserDSEntityID = (long)base.Feedback;
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
