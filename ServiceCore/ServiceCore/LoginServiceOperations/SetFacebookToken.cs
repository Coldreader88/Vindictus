using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.LoginServiceOperations
{
	[Serializable]
	public sealed class SetFacebookToken : Operation
	{
		public string Token { get; set; }

		public SetFacebookToken(string token)
		{
			this.Token = token;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
