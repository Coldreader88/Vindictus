using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class PetKilled : Operation
	{
		public long HostCID
		{
			get
			{
				return this.hostCID;
			}
		}

		public long Tag
		{
			get
			{
				return this.tag;
			}
		}

		public long PetID
		{
			get
			{
				return this.petID;
			}
		}

		public PetKilled(long host, long tag, long petid)
		{
			this.hostCID = host;
			this.tag = tag;
			this.petID = petid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new PetKilled.Request(this);
		}

		private long hostCID;

		private long tag;

		private long petID;

		private class Request : OperationProcessor
		{
			public Request(PetKilled op) : base(op)
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
