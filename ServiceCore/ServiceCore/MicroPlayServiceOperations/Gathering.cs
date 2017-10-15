using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class Gathering : Operation
	{
		public string EntityName { get; set; }

		public int PlayerTag { get; set; }

		public Gathering(string entityName, int playerTag)
		{
			this.EntityName = entityName;
			this.PlayerTag = playerTag;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
