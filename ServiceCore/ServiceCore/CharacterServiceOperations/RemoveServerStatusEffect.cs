using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class RemoveServerStatusEffect : Operation
	{
		public string Type { get; set; }

		public string IDTag { get; set; }

		public RemoveServerStatusEffect(string type, string idTag)
		{
			this.Type = type;
			this.IDTag = idTag;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
