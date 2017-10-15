using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class ChangeGuildIDAndName : Operation
	{
		public ChangeGuildIDAndName.ResultCodeEnum ResultCode
		{
			get
			{
				return this.resultCode;
			}
			set
			{
				this.resultCode = value;
			}
		}

		public ChangeGuildIDAndName(GuildMemberKey memberKey, int guildSN, string guildID, string guildName)
		{
			this.MemberKey = memberKey;
			this.GuildSN = guildSN;
			this.GuildID = guildID;
			this.GuildName = guildName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new ChangeGuildIDAndName.Request(this);
		}

		public GuildMemberKey MemberKey;

		public int GuildSN;

		public string GuildID;

		public string GuildName;

		[NonSerialized]
		private ChangeGuildIDAndName.ResultCodeEnum resultCode;

		public enum ResultCodeEnum
		{
			Unknown,
			Success,
			Fail_InvalidGuildID,
			Fail_InvalidGuildName,
			Fail_GuildInfoUpdateFailed
		}

		private class Request : OperationProcessor<ChangeGuildIDAndName>
		{
			public Request(ChangeGuildIDAndName op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is ChangeGuildIDAndName.ResultCodeEnum)
				{
					base.Operation.ResultCode = (ChangeGuildIDAndName.ResultCodeEnum)base.Feedback;
					if (base.Operation.ResultCode == ChangeGuildIDAndName.ResultCodeEnum.Success)
					{
						base.Result = true;
					}
					else
					{
						base.Result = false;
					}
				}
				else
				{
					base.Operation.ResultCode = ChangeGuildIDAndName.ResultCodeEnum.Unknown;
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
