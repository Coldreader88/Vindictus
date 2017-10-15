using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class VocationTransform : Operation
	{
		public int TransformLevel { get; set; }

		public VocationTransform(int transformLevel)
		{
			this.TransformLevel = transformLevel;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
