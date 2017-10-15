using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RagdollKickedMessage : IMessage
	{
		public int Tag { get; set; }

		public string TargetEntityName { get; set; }

		public EvilCoreType EvilCoreType { get; set; }

		public bool IsRareCore { get; set; }

		public override string ToString()
		{
			return string.Format("RagdollKicked[ {0} {1} {2} {3} ]", new object[]
			{
				this.Tag,
				this.TargetEntityName,
				this.EvilCoreType,
				this.IsRareCore
			});
		}
	}
}
