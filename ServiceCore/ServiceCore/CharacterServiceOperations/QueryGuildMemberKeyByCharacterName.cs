using System;
using System.Collections.Generic;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryGuildMemberKeyByCharacterName : Operation
	{
		public string CharacterName { get; private set; }

		public GuildMemberKey GuildMemberKeyInfo
		{
			get
			{
				return this.guildMemberKeyInfo;
			}
			private set
			{
				this.guildMemberKeyInfo = value;
			}
		}

		public QueryGuildMemberKeyByCharacterName(string CharacterName)
		{
			this.CharacterName = CharacterName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryGuildMemberKeyByCharacterName.Request(this);
		}

		[NonSerialized]
		private GuildMemberKey guildMemberKeyInfo;

		private class Request : OperationProcessor<QueryGuildMemberKeyByCharacterName>
		{
			public Request(QueryGuildMemberKeyByCharacterName op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is GuildMemberKey)
				{
					base.Operation.GuildMemberKeyInfo = (GuildMemberKey)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
