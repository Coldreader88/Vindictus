using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class UpdateEquipAllowed : Operation
	{
		public bool IsEquipAllowed { get; set; }

		public UpdateEquipAllowed(bool isEquipAllowed)
		{
			this.IsEquipAllowed = isEquipAllowed;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
