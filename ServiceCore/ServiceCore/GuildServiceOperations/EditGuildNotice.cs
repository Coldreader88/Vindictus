using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class EditGuildNotice : Operation
	{
		public GuildMemberKey Key { get; set; }

		public string Text { get; set; }

		public EditGuildNotice(GuildMemberKey key, string text)
		{
			this.Key = key;
			this.Text = text;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
