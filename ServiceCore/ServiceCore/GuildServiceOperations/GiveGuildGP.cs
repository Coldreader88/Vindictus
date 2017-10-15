using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.GuildService;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class GiveGuildGP : Operation
	{
		public GPGainType GainType { get; set; }

		public Dictionary<long, int> GiveDict { get; set; }

		public GiveGuildGP()
		{
			this.GiveDict = new Dictionary<long, int>();
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GiveGuildGP.Request(this);
		}

		[NonSerialized]
		public int LevelUpResult;

		private class Request : OperationProcessor<GiveGuildGP>
		{
			public Request(GiveGuildGP op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.LevelUpResult = (int)base.Feedback;
					yield return null;
				}
				yield break;
			}
		}
	}
}
