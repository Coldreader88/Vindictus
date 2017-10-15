using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class CashshopMileage : Operation
	{
		public long CID { get; set; }

		public int Mileage { get; set; }

		public CashshopMileage.ProcessEnum ProcessType { get; set; }

		public int ResultMileage
		{
			get
			{
				return this.resultMileage;
			}
			set
			{
				this.resultMileage = value;
			}
		}

		public CashshopMileage(long cid, int mileage, CashshopMileage.ProcessEnum processType)
		{
			this.CID = cid;
			this.Mileage = mileage;
			this.ProcessType = processType;
		}

		public CashshopMileage(CashshopMileage.ProcessEnum processType)
		{
			this.CID = 0L;
			this.Mileage = 0;
			this.ProcessType = processType;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CashshopMileage.Request(this);
		}

		[NonSerialized]
		private int resultMileage;

		public enum ProcessEnum
		{
			ADD,
			ADD_GIFT_USER,
			EXCHANGE,
			SET_CLIENT_DATA
		}

		private class Request : OperationProcessor<CashshopMileage>
		{
			public Request(CashshopMileage op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Result = (bool)base.Feedback;
				yield return null;
				base.Operation.ResultMileage = (int)base.Feedback;
				yield break;
			}
		}
	}
}
