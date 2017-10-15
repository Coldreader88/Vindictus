using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public class GuildChat : Operation
	{
		public long CID { get; set; }

		public string Message { get; set; }

		public GuildChat.ErrorCode GuildChatResult
		{
			get
			{
				return this.guildChatResult;
			}
			set
			{
				this.guildChatResult = value;
			}
		}

		public GuildChat(long CID, string message)
		{
			this.CID = CID;
			this.Message = message;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GuildChat.Request(this);
		}

		[NonSerialized]
		private GuildChat.ErrorCode guildChatResult;

		public enum ErrorCode
		{
			Unknown = -1,
			Success,
			NotEnoughPermission
		}

		private class Request : OperationProcessor<GuildChat>
		{
			public Request(GuildChat op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is GuildChat.ErrorCode)
				{
					base.Operation.GuildChatResult = (GuildChat.ErrorCode)base.Feedback;
				}
				else
				{
					base.Operation.GuildChatResult = GuildChat.ErrorCode.Unknown;
				}
				yield break;
			}
		}
	}
}
