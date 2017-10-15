using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class RankCharacterNameUpdate : Operation
	{
		public RankCharacterNameUpdate(long cid, string characterName)
		{
			this.Cid = cid;
			this.CharacterName = characterName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		public long Cid;

		public string CharacterName;
	}
}
