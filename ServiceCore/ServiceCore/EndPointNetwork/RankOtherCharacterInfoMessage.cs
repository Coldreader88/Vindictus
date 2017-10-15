using System;
using System.Collections.Generic;
using ServiceCore.RankServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RankOtherCharacterInfoMessage : IMessage
	{
		public int Level { get; set; }

		public int BaseCharacter { get; set; }

		public string CharacterName { get; set; }

		public IList<RankResultInfo> RankResult { get; set; }

		public IList<RankResultInfo> RequesterRankResult { get; set; }

		public int PeriodType { get; set; }

		public RankOtherCharacterInfoMessage(int Level, int BaseCharacter, string CharacterName, IList<RankResultInfo> RankResult, IList<RankResultInfo> RequesterRankResult, int PeriodType)
		{
			this.Level = Level;
			this.BaseCharacter = BaseCharacter;
			this.CharacterName = CharacterName;
			this.RankResult = RankResult;
			this.RequesterRankResult = RequesterRankResult;
			this.PeriodType = PeriodType;
		}

		public override string ToString()
		{
			return string.Format("RankOtherCharacterInfoMessage[{0}], {1}, {2}, {3}, [{4}], {5}", new object[]
			{
				this.RankResult.Count,
				this.Level,
				this.BaseCharacter,
				this.CharacterName,
				this.RequesterRankResult.Count,
				this.PeriodType
			});
		}
	}
}
