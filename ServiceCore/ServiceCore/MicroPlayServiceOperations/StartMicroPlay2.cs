using System;
using ServiceCore.QuestOwnershipServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class StartMicroPlay2 : Operation
	{
		public QuestDigest QuestDigest { get; set; }

		public bool IsCheat { get; set; }

		public bool IsDS { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
