using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class AddUserProduct : Operation
	{
		public int NexonSN { get; private set; }

		public string ProductName { get; private set; }

		public string Argument { get; private set; }

		public DateTime? ExpireTime { get; private set; }

		public AddUserProduct(int nexonSN, string productName, string argument, DateTime? expireTime)
		{
			this.NexonSN = nexonSN;
			this.ProductName = productName;
			this.Argument = argument;
			this.ExpireTime = expireTime;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
