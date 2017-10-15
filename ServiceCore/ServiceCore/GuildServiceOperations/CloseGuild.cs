using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class CloseGuild : Operation
	{
		public string ErrorType
		{
			get
			{
				return this.errorType;
			}
			set
			{
				this.errorType = value;
			}
		}

		public CloseGuild(GuildMemberKey key)
		{
			this.Key = key;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CloseGuild.Request(this);
		}

		public GuildMemberKey Key;

		[NonSerialized]
		private string errorType;

		private class Request : OperationProcessor<CloseGuild>
		{
			public Request(CloseGuild op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
					base.Operation.ErrorType = null;
				}
				else if (base.Feedback is string)
				{
					base.Result = false;
					base.Operation.ErrorType = (string)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.ErrorType = "";
				}
				yield break;
			}
		}
	}
}
