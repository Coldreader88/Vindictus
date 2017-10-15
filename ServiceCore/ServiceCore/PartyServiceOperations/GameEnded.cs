using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class GameEnded : Operation
	{
		public bool BreakParty { get; set; }

		public bool NoCountSuccess { get; set; }

		public int SuccessivePartyBonus
		{
			get
			{
				return this.successivePartyBonus;
			}
			set
			{
				this.successivePartyBonus = value;
			}
		}

		public int SuccessivePartyCount
		{
			get
			{
				return this.successivePartyCount;
			}
			set
			{
				this.successivePartyCount = value;
			}
		}

		public GameEnded()
		{
			this.BreakParty = false;
			this.NoCountSuccess = false;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GameEnded.Request(this);
		}

		[NonSerialized]
		private int successivePartyBonus;

		[NonSerialized]
		private int successivePartyCount;

		private class Request : OperationProcessor<GameEnded>
		{
			public Request(GameEnded op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.successivePartyBonus = (int)base.Feedback;
					yield return null;
					base.Operation.successivePartyCount = (int)base.Feedback;
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
