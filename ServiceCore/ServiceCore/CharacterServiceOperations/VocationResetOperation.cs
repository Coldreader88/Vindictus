using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class VocationResetOperation : Operation
	{
		public VocationEnum VocationClass { get; set; }

		public VocationResetOperation(VocationEnum vocationClass)
		{
			this.VocationClass = vocationClass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
