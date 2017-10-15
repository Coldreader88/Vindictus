using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.UserDSHostServiceOperations
{
	[Serializable]
	public sealed class UserDSHostTransferStart : Operation
	{
		public long UserDSRealHostCID { get; set; }

		public UserDSHostTransferStart(long userDSRealHostCID)
		{
			this.UserDSRealHostCID = userDSRealHostCID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
