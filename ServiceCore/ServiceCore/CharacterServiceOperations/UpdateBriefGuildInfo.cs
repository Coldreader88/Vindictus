using System;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class UpdateBriefGuildInfo : Operation
	{
		public BriefGuildInfo BriefGuildInfo { get; set; }

		public bool IsRankTitleAnnounce { get; set; }

		public UpdateBriefGuildInfo(BriefGuildInfo info, bool isRankTitleAnnounce)
		{
			this.BriefGuildInfo = info;
			this.IsRankTitleAnnounce = isRankTitleAnnounce;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
