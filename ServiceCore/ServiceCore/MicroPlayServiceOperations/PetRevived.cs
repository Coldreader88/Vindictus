using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class PetRevived : Operation
	{
		public long HostCID { get; set; }

		public int CasterTag { get; set; }

		public int RevivedTag { get; set; }

		public long PetID { get; set; }

		public string Method { get; set; }

		public PetRevived(long hostCID, int caster, int revived, long petID, string method)
		{
			this.HostCID = hostCID;
			this.CasterTag = caster;
			this.RevivedTag = revived;
			this.PetID = petID;
			this.Method = method;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
