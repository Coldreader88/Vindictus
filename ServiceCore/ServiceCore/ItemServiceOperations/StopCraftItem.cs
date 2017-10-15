using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class StopCraftItem : Operation
	{
		public bool TargetState
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private bool state;
	}
}
