using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class RequestNewbieRecommendGuild : Operation
	{
		public RequestNewbieRecommendGuild(string name, long shipid)
		{
			this.UserName = name;
			this.ShipID = shipid;
		}

		public string UserName { get; set; }

		public long ShipID { get; set; }

		public List<string> Teachers { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new RequestNewbieRecommendGuild.Request(this);
		}

		private class Request : OperationProcessor<RequestNewbieRecommendGuild>
		{
			public Request(RequestNewbieRecommendGuild op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is List<string>)
				{
					base.Operation.Teachers = (List<string>)base.Feedback;
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
