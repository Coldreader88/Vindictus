using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RewardItem : Operation
	{
		public Dictionary<string, int> Rewards { get; set; }

		public Dictionary<string, int> GivenItems
		{
			get
			{
				return this.givenItems;
			}
			set
			{
				this.givenItems = value;
			}
		}

		public RewardItem(Dictionary<string, int> rewards)
		{
			this.Rewards = rewards;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RewardItem.Request(this);
		}

		[NonSerialized]
		private Dictionary<string, int> givenItems;

		private class Request : OperationProcessor<RewardItem>
		{
			public Request(RewardItem op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is Dictionary<string, int>)
				{
					base.Operation.GivenItems = (base.Feedback as Dictionary<string, int>);
					base.Result = true;
				}
				else
				{
					base.Operation.GivenItems = new Dictionary<string, int>();
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
