using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class UseConsumable : Operation
	{
		public long HostCID
		{
			get
			{
				return this.host;
			}
		}

		public long UserTag
		{
			get
			{
				return (long)this.tag;
			}
		}

		public int UsedPart
		{
			get
			{
				return this.part;
			}
		}

		public int InnerSlot
		{
			get
			{
				return this.slot;
			}
		}

		public UseConsumable(long host, int tag, int part, int slot)
		{
			this.host = host;
			this.tag = tag;
			this.part = part;
			this.slot = slot;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private long host;

		private int tag;

		private int part;

		private int slot;
	}
}
