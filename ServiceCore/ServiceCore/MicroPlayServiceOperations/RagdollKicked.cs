using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class RagdollKicked : Operation
	{
		public int Tag { get; set; }

		public string TargetEntityName { get; set; }

		public EvilCoreType EvilCoreType { get; set; }

		public bool IsRareCore { get; set; }

		public RagdollKicked(int tag, string targetEntityName, EvilCoreType type, bool isRareCore)
		{
			this.Tag = tag;
			this.TargetEntityName = targetEntityName;
			this.EvilCoreType = type;
			this.IsRareCore = isRareCore;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
