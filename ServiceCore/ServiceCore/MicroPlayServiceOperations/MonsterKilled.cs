using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class MonsterKilled : Operation
	{
		public long MasterCID { get; set; }

		public int Attacker { get; set; }

		public int Target { get; set; }

		public string Action { get; set; }

		public bool HasEvilCore { get; set; }

		public int Damage { get; set; }

		public int PositionX { get; set; }

		public int PositionY { get; set; }

		public int Distance { get; set; }

		public int ActorIndex { get; set; }

		public bool IgnoreCheatCheck { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new MonsterKilled.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(MonsterKilled op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Result = (base.Feedback is OkMessage);
				yield break;
			}
		}
	}
}
