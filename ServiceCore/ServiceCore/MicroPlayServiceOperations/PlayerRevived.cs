using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class PlayerRevived : Operation
	{
		public long HostCID { get; set; }

		public int CasterTag { get; set; }

		public int RevivedTag { get; set; }

		public string Method { get; set; }

		public PlayerRevived(long hostCID, int caster, int revived, string method)
		{
			this.HostCID = hostCID;
			this.CasterTag = caster;
			this.RevivedTag = revived;
			this.Method = method;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
