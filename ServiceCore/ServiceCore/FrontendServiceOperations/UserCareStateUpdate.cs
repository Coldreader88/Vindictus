using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class UserCareStateUpdate : Operation
	{
		public int UserCareType { get; set; }

		public int UserCareNextState { get; set; }

		public UserCareStateUpdate(int type, int nextState)
		{
			this.UserCareType = type;
			this.UserCareNextState = nextState;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
