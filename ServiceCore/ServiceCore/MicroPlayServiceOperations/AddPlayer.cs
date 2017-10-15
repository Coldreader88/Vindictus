using System;
using System.Collections.Generic;
using System.Net;
using ServiceCore.EndPointNetwork;
using ServiceCore.PartyServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class AddPlayer : Operation
	{
		public int UID { get; set; }

		public long CID { get; set; }

		public int SlotNumber { get; set; }

		public long FrontendID { get; set; }

		public int Age { get; set; }

		public JoinType JType { get; set; }

		public bool IsTeaching { get; set; }

		public bool IsHost { get; set; }

		public bool IsCheat { get; set; }

		public IPEndPoint EndPoint
		{
			get
			{
				return this.endpoint;
			}
		}

		public int SN
		{
			get
			{
				return this.sn;
			}
		}

		public int Key
		{
			get
			{
				return this.key;
			}
		}

		public GameJoinMemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
		}

		public GameInfo HostInfo
		{
			get
			{
				return this.hostInfo;
			}
		}

		public bool IsHostCandidate
		{
			get
			{
				return this.isHostCandidate;
			}
		}

		public AddPlayer.FailReasonEnum FailReason
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

		public override OperationProcessor RequestProcessor()
		{
			return new AddPlayer.Request(this);
		}

		[NonSerialized]
		private IPEndPoint endpoint;

		[NonSerialized]
		private int sn;

		[NonSerialized]
		private int key;

		[NonSerialized]
		private GameJoinMemberInfo memberInfo;

		[NonSerialized]
		private GameInfo hostInfo;

		[NonSerialized]
		private bool isHostCandidate;

		[NonSerialized]
		private AddPlayer.FailReasonEnum failReason;

		public enum FailReasonEnum
		{
			Unknown,
			NoSuchMicroplay,
			SlotNotEmpty,
			P2PJoinFail,
			PlayerInfoFail,
			SlotNotEmpty2,
			CannotSendMemberInfo,
			NoHostInfo,
			FullMicroPlay,
			QuestNotAvailable,
			Over18Only,
			NotEnoughCoinForAssist,
			NotEnoughFishingTicket,
			DuplicateAssist,
			NoAssist,
			ItemRequired
		}

		private class Request : OperationProcessor<AddPlayer>
		{
			public Request(AddPlayer op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IPEndPoint)
				{
					base.Operation.endpoint = (base.Feedback as IPEndPoint);
					yield return null;
					base.Operation.sn = (int)base.Feedback;
					yield return null;
					base.Operation.key = (int)base.Feedback;
					yield return null;
					base.Operation.memberInfo = (base.Feedback as GameJoinMemberInfo);
					yield return null;
					if (base.Feedback is GameInfo)
					{
						base.Operation.hostInfo = (base.Feedback as GameInfo);
					}
					yield return null;
					base.Operation.isHostCandidate = (bool)base.Feedback;
				}
				else
				{
					base.Result = false;
					if (base.Feedback is AddPlayer.FailReasonEnum)
					{
						base.Operation.FailReason = (AddPlayer.FailReasonEnum)base.Feedback;
					}
					else
					{
						base.Operation.FailReason = AddPlayer.FailReasonEnum.Unknown;
					}
				}
				yield break;
			}
		}
	}
}
