using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class FeverSystemOperation : Operation
	{
		public FeverSystemOperation.ProcessEnum ProcessType { get; set; }

		public FeverSystemOperation(FeverSystemOperation.ProcessEnum processType)
		{
			this.ProcessType = processType;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		public enum ProcessEnum
		{
			SET_CLIENT_DATA,
			USE
		}
	}
}
