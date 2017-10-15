using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using ServiceCore.PartyServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class RegisterPvpPlayer : Operation
	{
		public string PvpMode { get; set; }

		public ICollection<PvPPartyMemberInfo> IDList { get; set; }

		public long MyCID { get; set; }

		public PvpRegisterCheat Cheat { get; set; }

		public int ChannelID { get; set; }

		public long PartyID { get; set; }

		public RegisterPvpPlayer(string pvpMode, ICollection<PvPPartyMemberInfo> idList, long myCID, PvpRegisterCheat cheat, long partyID, int channelID)
		{
			this.PvpMode = pvpMode;
			this.IDList = idList;
			this.MyCID = myCID;
			this.Cheat = cheat;
			this.PartyID = partyID;
			this.ChannelID = channelID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RegisterPvpPlayer.Request(this);
		}

		[NonSerialized]
		public HeroesString ErrorMessage;

		[NonSerialized]
		public bool AllowEquipWhileWaiting;

		private class Request : OperationProcessor<RegisterPvpPlayer>
		{
			public Request(RegisterPvpPlayer op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is bool)
				{
					base.Result = true;
					base.Operation.ErrorMessage = null;
					base.Operation.AllowEquipWhileWaiting = (bool)base.Feedback;
				}
				else if (base.Feedback is HeroesString)
				{
					base.Result = false;
					base.Operation.ErrorMessage = (base.Feedback as HeroesString);
				}
				else
				{
					base.Result = false;
					base.Operation.ErrorMessage = new HeroesString("GameUI_Heroes_PvpJoinFail_Unknown");
				}
				yield break;
			}
		}
	}
}
