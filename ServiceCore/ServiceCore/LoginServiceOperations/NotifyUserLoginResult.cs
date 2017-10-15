using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.LoginServiceOperations
{
	[Serializable]
	public sealed class NotifyUserLoginResult : Operation
	{
		public string UserLoginResult { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
