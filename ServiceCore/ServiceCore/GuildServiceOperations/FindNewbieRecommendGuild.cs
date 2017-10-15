using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.GuildService;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class FindNewbieRecommendGuild : Operation
	{
		public InGameGuildInfo Info
		{
			get
			{
				return this.info;
			}
			set
			{
				this.info = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new FindNewbieRecommendGuild.Request(this);
		}

		[NonSerialized]
		private InGameGuildInfo info;

		private class Request : OperationProcessor<FindNewbieRecommendGuild>
		{
			public Request(FindNewbieRecommendGuild op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is InGameGuildInfo)
				{
					base.Operation.Info = (InGameGuildInfo)base.Feedback;
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
