using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TriggerEventMessage : IMessage
	{
		public string EventName { get; set; }

		public string Arg { get; set; }

		public override string ToString()
		{
			return string.Format("{0}[ {1}, {2} ]", base.ToString(), this.EventName, this.Arg);
		}
	}
}
