using System;
using System.Collections.Generic;
using ServiceCore.QuestOwnershipServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class JoinParty : Operation
	{
		public long CharacterID { get; set; }

		public long FrontendID { get; set; }

		public int NexonSN { get; set; }

		public JoinType JType { get; set; }

		public bool PushMicroPlayInfo { get; set; }

		public int Slot
		{
			get
			{
				return this.slot;
			}
		}

		public long PlayID
		{
			get
			{
				return this.playID;
			}
		}

		public bool IsEntranceProcessSkipped
		{
			get
			{
				return this.skipProcess;
			}
		}

		public object Reason
		{
			get
			{
				return this.failReason;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new JoinParty.Request(this);
		}

		[NonSerialized]
		private int slot;

		[NonSerialized]
		private long playID;

		[NonSerialized]
		private bool skipProcess;

		[NonSerialized]
		private object failReason = JoinParty.FailReason.NoSuchParty;

		public enum FailReason : byte
		{
			NoSuchParty,
			PartyIsFull,
			NotInvited,
			LevelNotMatch,
			LevelMaxNumExceed,
			ShipAlreadyLaunched,
			QuestNotAvailable,
			InternalError,
			NotInGame,
			Loading,
			MicroPlayRestarting,
			DuplicateAssist,
			Unknown
		}

		private class Request : OperationProcessor<JoinParty>
		{
			public Request(JoinParty op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is QuestConstraintResult || base.Feedback is JoinParty.FailReason)
				{
					base.Result = false;
					base.Operation.failReason = base.Feedback;
				}
				else if (base.Feedback is int)
				{
					base.Operation.slot = (int)base.Feedback;
					yield return null;
					base.Operation.playID = (long)base.Feedback;
					yield return null;
					base.Operation.skipProcess = (bool)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.failReason = (JoinParty.FailReason)base.Feedback;
				}
				yield break;
			}
		}
	}
}
