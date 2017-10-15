using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.Housing;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class EnterHousing : Operation
	{
		public long CID { get; set; }

		public long HousingID { get; set; }

		public EnterHousingType EnterType { get; set; }

		public long TargetHousingPlayID { get; set; }

		public EnterHousing(long cid, long housingID, EnterHousingType enterType, long targetHousingPlayID)
		{
			this.CID = cid;
			this.HousingID = housingID;
			this.EnterType = enterType;
			this.TargetHousingPlayID = targetHousingPlayID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new EnterHousing.Request(this);
		}

		[NonSerialized]
		public long HousingPlayID;

		[NonSerialized]
		public bool IsHost;

		[NonSerialized]
		public EnterHousing.FailReasonEnum FailReason;

		public enum FailReasonEnum : byte
		{
			InvalidHousingID,
			PermissionFail,
			NoSuchPlay,
			OpenPlayFail,
			Unknown
		}

		private class Request : OperationProcessor<EnterHousing>
		{
			public Request(EnterHousing op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is long)
				{
					base.Result = true;
					base.Operation.HousingPlayID = (long)base.Feedback;
					yield return null;
					base.Operation.IsHost = (bool)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.FailReason = (EnterHousing.FailReasonEnum)base.Feedback;
				}
				yield break;
			}
		}
	}
}
