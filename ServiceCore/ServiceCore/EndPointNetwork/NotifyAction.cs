using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NotifyAction : IMessage
	{
		public long ID { get; set; }

		public ActionSync Action { get; set; }

		public override string ToString()
		{
			return string.Format("NotifyAction {0} : {1}", this.ID, this.Action);
		}
	}
}
