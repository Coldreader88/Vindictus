using System;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NotifyLook : IMessage
	{
		public long ID { get; set; }

		public CharacterSummary Look { get; set; }

		public override string ToString()
		{
			return string.Format("NotifyLook {0} : {1}", this.ID, this.Look);
		}
	}
}
