using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class GameStarting : Operation
	{
		public GameStarting.FailReasonEnum FailReason
		{
			get
			{
				return this.failReason;
			}
			set
			{
				this.failReason = value;
			}
		}

		public int MinPartyCount { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new GameStarting.Request(this);
		}

		[NonSerialized]
		private GameStarting.FailReasonEnum failReason;

		public enum FailReasonEnum
		{
			NoSuchParty,
			PartyNotInShip,
			NotAllReady,
			EmergencyStop,
			NotEnoughPartyCount,
			Unknown
		}

		private class Request : OperationProcessor<GameStarting>
		{
			public Request(GameStarting op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.FailReason = GameStarting.FailReasonEnum.Unknown;
				if (!(base.Feedback is GameStarting.FailReasonEnum))
				{
					base.Result = (base.Feedback is OkMessage);
				}
				else
				{
					base.Result = false;
					base.Operation.FailReason = (GameStarting.FailReasonEnum)base.Feedback;
				}
				yield break;
			}
		}
	}
}
