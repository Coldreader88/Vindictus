using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryAP : Operation
	{
		public long NextBonusTimeTicks
		{
			get
			{
				return this.nextBonusTimeTicks;
			}
			set
			{
				this.nextBonusTimeTicks = value;
			}
		}

		public int AP
		{
			get
			{
				return this.ap;
			}
			set
			{
				this.ap = value;
			}
		}

		public int MaxAP
		{
			get
			{
				return this.maxap;
			}
			set
			{
				this.maxap = value;
			}
		}

		public int APBonusInterval
		{
			get
			{
				return this.apBonusInterval;
			}
			set
			{
				this.apBonusInterval = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryAP.Request(this);
		}

		[NonSerialized]
		private long nextBonusTimeTicks;

		[NonSerialized]
		private int ap;

		[NonSerialized]
		private int maxap;

		[NonSerialized]
		private int apBonusInterval;

		private class Request : OperationProcessor<QueryAP>
		{
			public Request(QueryAP op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is APInfo)
				{
					APInfo apinfo = base.Feedback as APInfo;
					base.Operation.AP = apinfo.AP;
					base.Operation.nextBonusTimeTicks = apinfo.NextBonusTimeTicks;
					base.Operation.MaxAP = apinfo.MaxAP;
					base.Operation.APBonusInterval = apinfo.APBonusInterval;
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
