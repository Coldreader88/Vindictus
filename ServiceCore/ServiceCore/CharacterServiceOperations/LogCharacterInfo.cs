using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class LogCharacterInfo : Operation
	{
		public string Key { get; set; }

		public string Value { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
