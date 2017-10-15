using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class MonsterDamageReport : Operation
	{
		public int Target { get; set; }

		public ICollection<MonsterTakeDamageInfo> TakeDamageList { get; set; }

		public DateTime Time { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new MonsterDamageReport.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(MonsterDamageReport op) : base(op)
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
