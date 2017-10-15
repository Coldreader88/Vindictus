using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class StopStorytelling : Operation
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
