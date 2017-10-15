using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class GatherObject : Operation
	{
		public string MID { get; set; }

		public int RequireExp { get; set; }

		public int IncreaseExp
		{
			get
			{
				return this.increaseExp;
			}
		}

		public GatherObject(string mID, int requireExp)
		{
			this.MID = mID;
			this.RequireExp = requireExp;
			this.increaseExp = 0;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GatherObject.Request(this);
		}

		[NonSerialized]
		private int increaseExp;

		private class Request : OperationProcessor<GatherObject>
		{
			public Request(GatherObject op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.increaseExp = (int)base.Feedback;
					base.Result = true;
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
