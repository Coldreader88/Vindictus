using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class ReportGuildMemberChanged : Operation
	{
		public string CharacterName { get; set; }

		public int Level { get; set; }

		public ReportGuildMemberChanged(string name, int level)
		{
			this.CharacterName = name;
			this.Level = level;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
