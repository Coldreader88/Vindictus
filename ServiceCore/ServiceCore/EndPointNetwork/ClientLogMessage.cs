using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ClientLogMessage : IMessage
	{
		public int LogType { get; set; }

		public string Key { get; set; }

		public string Value { get; set; }

		public override string ToString()
		{
			return string.Format("ClientLogMessage [{0} : {1} : {2}]", (ClientLogMessage.LogTypes)this.LogType, this.Key, this.Value);
		}

		public enum LogTypes
		{
			Character,
			MicroPlay
		}
	}
}
