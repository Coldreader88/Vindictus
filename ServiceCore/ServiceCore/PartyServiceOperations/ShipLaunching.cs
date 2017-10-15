using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using ServiceCore.QuestOwnershipServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class ShipLaunching : Operation
	{
		public long MasterCID { get; set; }

		public long HostCID { get; set; }

		public string ShipName { get; set; }

		public bool IsGiantRaid { get; set; }

		public QuestDigest QuestDigest { get; set; }

		public ShipOptionInfo Option { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new ShipLaunching.Request(this);
		}

		[NonSerialized]
		public ShipLaunching.FailReasonEnum FailReason;

		[NonSerialized]
		public Dictionary<long, int> PlayCountDict;

		[NonSerialized]
		public int SuccessiveCount;

		public enum FailReasonEnum
		{
			NoSuchParty,
			HostNotInParty,
			EmergencyStop,
			TooManyPartyMembers,
			UnknownError
		}

		private class Request : OperationProcessor<ShipLaunching>
		{
			public Request(ShipLaunching op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is Dictionary<long, int>)
				{
					base.Result = true;
					base.Operation.PlayCountDict = (base.Feedback as Dictionary<long, int>);
					yield return null;
					base.Operation.SuccessiveCount = (int)base.Feedback;
				}
				else if (base.Feedback is ShipLaunching.FailReasonEnum)
				{
					base.Result = false;
					base.Operation.FailReason = (ShipLaunching.FailReasonEnum)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.FailReason = ShipLaunching.FailReasonEnum.UnknownError;
				}
				yield break;
			}
		}
	}
}
