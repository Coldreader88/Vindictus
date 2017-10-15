using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class _CheatCommandMessage : IMessage
	{
		public string Service { get; set; }

		public string Command { get; set; }

		public bool IsEntityOp { get; set; }

		public override string ToString()
		{
			return string.Format("_CheatCommandMessage [ {0} {1} ]", this.Service, this.Command);
		}
	}
}
