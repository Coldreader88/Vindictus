using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class QueryMemberState : Operation
	{
		public int Slot { get; set; }

		public ReadyState MemberState
		{
			get
			{
				return this.memberState;
			}
		}

		public PartyInfoState PartyState
		{
			get
			{
				return this.partyState;
			}
		}

		public string QuestID
		{
			get
			{
				return this.questID;
			}
		}

		public bool IsInGameJoinAllowed
		{
			get
			{
				return this.isInGameJoinAllowed;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryMemberState.Request(this);
		}

		[NonSerialized]
		private ReadyState memberState;

		[NonSerialized]
		private PartyInfoState partyState;

		[NonSerialized]
		private string questID;

		[NonSerialized]
		private bool isInGameJoinAllowed;

		private class Request : OperationProcessor<QueryMemberState>
		{
			public Request(QueryMemberState op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is ReadyState)
				{
					base.Operation.memberState = (ReadyState)base.Feedback;
					yield return null;
					base.Operation.partyState = (PartyInfoState)base.Feedback;
					yield return null;
					base.Operation.questID = (string)base.Feedback;
					yield return null;
					base.Operation.isInGameJoinAllowed = (bool)base.Feedback;
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
