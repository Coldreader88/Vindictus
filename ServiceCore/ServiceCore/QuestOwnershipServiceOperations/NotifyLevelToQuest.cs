using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class NotifyLevelToQuest : Operation
	{
		public int CharacterLevel { get; set; }

		public int? CafeType { get; set; }

		public bool? HasVIPBonusEffect { get; set; }

		public NotifyLevelToQuest(int CharacterLevel, int? cafeType, bool? hasHomeCafe, bool? hasVIPBonusEffect)
		{
			this.CharacterLevel = CharacterLevel;
			this.CafeType = cafeType;
			this.HasVIPBonusEffect = hasVIPBonusEffect;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
