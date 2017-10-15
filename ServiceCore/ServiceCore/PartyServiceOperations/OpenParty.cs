using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using ServiceCore.QuestOwnershipServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class OpenParty : Operation
	{
		public string Title { get; set; }

		public QuestDigest QuestDigest { get; set; }

		public ShipOptionInfo Option { get; set; }

		public bool IsInTownWithShipInfo { get; set; }

		public Dictionary<long, InvitedPartyMember> ReserveInfo { get; set; }

		public long PartyID
		{
			get
			{
				return this.pid;
			}
		}

		public OpenParty(string title, QuestDigest digest, ShipOptionInfo option, bool isInTownWithShipInfo, Dictionary<long, InvitedPartyMember> reserveInfo)
		{
			this.Title = title;
			this.QuestDigest = digest;
			this.Option = option;
			this.IsInTownWithShipInfo = isInTownWithShipInfo;
			this.ReserveInfo = reserveInfo;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OpenParty.Request(this);
		}

		[NonSerialized]
		private long pid;

		private class Request : OperationProcessor<OpenParty>
		{
			public Request(OpenParty op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is long)
				{
					base.Operation.pid = (long)base.Feedback;
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
