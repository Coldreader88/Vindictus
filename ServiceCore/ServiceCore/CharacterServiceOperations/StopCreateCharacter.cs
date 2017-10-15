using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class StopCreateCharacter : Operation
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
