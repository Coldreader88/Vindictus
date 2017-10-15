using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class RegisterDSParty : Operation
	{
		public string QuestID { get; set; }

		public List<DSPlayerInfo> PartyMembers { get; set; }

		public long MicroPlayID { get; set; }

		public long PartyID { get; set; }

		public bool IsGiantRaid { get; set; }

		public bool IsAdultMode { get; set; }

		public RegisterDSParty(string questID, List<DSPlayerInfo> members, long microPlayID, long partyID, bool isGiantRaid, bool isAdultMode)
		{
			this.QuestID = questID;
			this.PartyMembers = members;
			this.MicroPlayID = microPlayID;
			this.PartyID = partyID;
			this.IsGiantRaid = isGiantRaid;
			this.IsAdultMode = isAdultMode;
		}

		public RegisterDSParty(string questID, long cid, long fid, int level, long microPlayID, long partyID, bool isGiantRaid, bool isAdultMode)
		{
			this.QuestID = questID;
			this.PartyMembers = new List<DSPlayerInfo>
			{
				new DSPlayerInfo(cid, fid, level)
			};
			this.MicroPlayID = microPlayID;
			this.PartyID = partyID;
			this.IsGiantRaid = isGiantRaid;
			this.IsAdultMode = isAdultMode;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RegisterDSParty.Request(this);
		}

		[NonSerialized]
		public RegisterDSPartyResult FailReason;

		private class Request : OperationProcessor<RegisterDSParty>
		{
			public Request(RegisterDSParty op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is RegisterDSPartyResult)
				{
					base.Operation.FailReason = (RegisterDSPartyResult)base.Feedback;
					base.Result = (RegisterDSPartyResult.Success == (RegisterDSPartyResult)base.Feedback);
				}
				else
				{
					base.Result = false;
					base.Operation.FailReason = RegisterDSPartyResult.Unknown;
				}
				yield break;
			}
		}
	}
}
