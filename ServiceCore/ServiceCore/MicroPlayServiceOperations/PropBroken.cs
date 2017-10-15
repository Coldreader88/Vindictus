using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class PropBroken : Operation
	{
		public long HostCID { get; set; }

		public int BrokenProp { get; set; }

		public string EntityName { get; set; }

		public int Attacker { get; set; }

		public PropBroken(long cid, int prop, string ename, int attacker)
		{
			this.HostCID = cid;
			this.BrokenProp = prop;
			this.EntityName = ename;
			this.Attacker = attacker;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
