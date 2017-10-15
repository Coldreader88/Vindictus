using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using ServiceCore.QuestOwnershipServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class RegisterShipInfo : Operation
	{
		public long CharacterID { get; set; }

		public string QuestID { get; set; }

		public string ShipName { get; set; }

		public int SwearID { get; set; }

		public bool IsHuntingQuest { get; set; }

		public ShipOptionInfo Option { get; set; }

		public bool UpdateShipList { get; set; }

		public object Reason
		{
			get
			{
				return this.failReason;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RegisterShipInfo.Request(this);
		}

		[NonSerialized]
		private object failReason = RegisterShipInfo.FailReason.Unknown;

		public enum FailReason : byte
		{
			TooManyPartyMembers,
			AlreadyRegistered,
			NotMaster,
			Unknown
		}

		private class Request : OperationProcessor<RegisterShipInfo>
		{
			public Request(RegisterShipInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is QuestConstraintResult || base.Feedback is RegisterShipInfo.FailReason)
				{
					base.Result = false;
					base.Operation.failReason = base.Feedback;
				}
				else
				{
					base.Result = (base.Feedback is OkMessage);
				}
				yield break;
			}
		}
	}
}
